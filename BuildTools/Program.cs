using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Newtonsoft.Json.Linq;
using SharpDiff.FileStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    static class Program //Based on Spigot BuildTools
    {
        public const string Version = "1.9";
        static void Main(string[] args)
        {
            Console.WriteLine("Started BuildTools\n");
            try
            {
                DoIt();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fatal error during install!\n" + e);
                Console.ReadLine();
            }
            Console.ReadLine();
        }

        private static void DoIt()
        {
            Directory.CreateDirectory("work");
            var options = new CloneOptions();
            options.OnProgress += new ProgressHandler(CloneProgress);
            if (!Directory.Exists("CraftBukkit"))
            {
                Console.WriteLine("Cloning CraftBukkit...");
                Console.WriteLine(Repository.Clone("https://hub.spigotmc.org/stash/scm/spigot/craftbukkit.git", "CraftBukkit", options));
                Console.WriteLine();
            }
            if (!Directory.Exists("BuildData"))
            {
                Console.WriteLine("Cloning BuildData...");
                Console.WriteLine(Repository.Clone("https://hub.spigotmc.org/stash/scm/spigot/builddata.git", "BuildData", options));
                Console.WriteLine();
            }
            if (!System.IO.File.Exists("work" + Path.DirectorySeparatorChar + "minecraft_server." + Version + ".jar"))
            {
                Console.WriteLine("Downloading vanilla server...");
                var client = new WebClient();
                client.DownloadFileAsync(new Uri("https://s3.amazonaws.com/Minecraft.Download/versions/" + Version + "/minecraft_server." + Version + ".jar"), "work" + Path.DirectorySeparatorChar + "minecraft_server." + Version + ".jar");
                client.DownloadProgressChanged += ServerDownloadProgressChanged;
                client.DownloadFileCompleted += ServerDownloadFinished;
            }
            else
                ContinueDoingIt();
        }

        private static void ServerDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rProgress: " + e.ProgressPercentage + " (" + e.BytesReceived + "/" + e.TotalBytesToReceive + ")");
        }

        private static void ServerDownloadFinished(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ContinueDoingIt();
        }

        private static bool CloneProgress(string progress)
        {
            Console.Write(progress);
            return true;
        }

        private static void ContinueDoingIt()
        {
            DirectoryInfo maven;
            String m2Home = Environment.GetEnvironmentVariable("M2_HOME");
            if (m2Home == null || !(maven = new DirectoryInfo(m2Home)).Exists)
            {
                maven = new DirectoryInfo("apache-maven-3.2.5");

                if (!maven.Exists)
                {
                    Console.WriteLine("Maven does not exist, downloading. Please wait.");

                    FileInfo mvnTemp = new FileInfo("mvn.zip");

                    var client = new WebClient();
                    client.DownloadFile("https://static.spigotmc.org/maven/apache-maven-3.2.5-bin.zip", mvnTemp.FullName);
                    Console.WriteLine("Unzipping Maven...");
                    maven.Create();
                    var zipfile = ZipFile.OpenRead(mvnTemp.FullName);
                    foreach (var entry in zipfile.Entries)
                    {
                        if (entry.FullName.EndsWith("/"))
                        {
                            Directory.CreateDirectory(entry.FullName);
                            continue;
                        }
                        Console.WriteLine("Extracting " + entry.FullName);
                        entry.ExtractToFile(Path.Combine(maven.Parent.FullName, entry.FullName), true);
                    }
                    zipfile.Dispose();
                    mvnTemp.Delete();
                }
            }
            string java_home = GetJavaInstallationPath();
            Environment.SetEnvironmentVariable("JAVA_HOME", java_home);
            Console.WriteLine();
            JObject obj = JObject.Parse(new WebClient().DownloadString("https://hub.spigotmc.org/versions/" + Version + ".json"));
            Pull("BuildData", obj);
            Pull("CraftBukkit", obj);

            var info = JObject.Parse(System.IO.File.ReadAllText("BuildData" + Path.DirectorySeparatorChar + "info.json"));
            var buildrepo = new Repository("BuildData");
            string id = null;
            foreach (Commit commit in buildrepo.Commits)
            {
                if (TreeContainsFile(commit.Tree, (string)info["accessTransforms"]) || TreeContainsFile(commit.Tree, (string)info["classMappings"]) || TreeContainsFile(commit.Tree, (string)info["memberMappings"]) || TreeContainsFile(commit.Tree, (string)info["packageMappings"]))
                {
                    id = commit.Id.Sha.Substring(24);
                    break;
                }
            }

            var pi = new ProcessStartInfo("java");
            Process p = null;
            FileInfo finalMappedJar = new FileInfo("work" + Path.DirectorySeparatorChar + "mapped." + id + ".jar");
            pi.UseShellExecute = false;
            pi.RedirectStandardOutput = true;
            if (!finalMappedJar.Exists)
            {
                Console.WriteLine("Final mapped jar: " + finalMappedJar + " does not exist, creating!");

                FileInfo clMappedJar = new FileInfo("work" + Path.DirectorySeparatorChar + "mapped." + id + "-cl.jar");
                FileInfo mMappedJar = new FileInfo("work" + Path.DirectorySeparatorChar + "mapped." + id + "-m.jar");

                Console.WriteLine("1");
                pi.Arguments = "-jar BuildData/bin/SpecialSource-2.jar map -i work" + Path.DirectorySeparatorChar + "minecraft_server." + Version + ".jar -m BuildData/mappings/" + (string)info["classMappings"] + " -o \"" + clMappedJar.FullName + "\"";
                p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());

                Console.WriteLine("2");
                pi.Arguments = "-jar BuildData/bin/SpecialSource-2.jar map -i \"" + clMappedJar.FullName +
                            "\" -m BuildData/mappings/" + (string)info["memberMappings"] + " -o \"" + mMappedJar.FullName + "\"";
                Console.WriteLine(pi.Arguments);
                p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());

                Console.WriteLine("3");
                pi.Arguments = "-jar BuildData/bin/SpecialSource.jar --kill-lvt -i \"" + mMappedJar.FullName + "\" --access-transformer BuildData/mappings/" + (string)info["accessTransforms"] +
                        " -m BuildData/mappings/" + (string)info["packageMappings"] + " -o \"" + finalMappedJar.FullName + "\"";
                p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());
            }

            RunMaven("install:install-file -Dfile=\"" + finalMappedJar.FullName + "\" -Dpackaging=jar -DgroupId=org.spigotmc -DartifactId=minecraft-server -Dversion=1.9-SNAPSHOT");

            Console.WriteLine("\nExtracting server files...");
            var decompiledi = new DirectoryInfo(Path.Combine("work", "decompile - " + id, "classes"));
            if (!decompiledi.Exists)
            {
                decompiledi.Create();
                var zip = ZipFile.OpenRead(finalMappedJar.FullName);
                foreach (var entry in zip.Entries.Where(e => e.FullName.StartsWith("net/minecraft/server")))
                {
                    if (entry.FullName.EndsWith("/"))
                        continue;
                    Console.WriteLine("Extracting " + entry.FullName);
                    new FileInfo(Path.Combine("work", "decompile - " + id, "classes", entry.FullName)).Directory.Create();
                    entry.ExtractToFile(Path.Combine("work", "decompile - " + id, "classes", entry.FullName), true);
                }
                Console.WriteLine("\nDecompiling...");
                pi.Arguments = "-jar BuildData/bin/fernflower.jar -dgs=1 -hdc=0 -rbr=0 -asc=1 -udv=0 \"work" + Path.DirectorySeparatorChar + "decompile - " + id + Path.DirectorySeparatorChar + "classes\" \"work" + Path.DirectorySeparatorChar + "decompile - " + id + "\"";
                p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());
            }

            Console.WriteLine("Applying CraftBukkit Patches");
            DirectoryInfo nmsDir = new DirectoryInfo(Path.Combine("CraftBukkit", "src", "main", "java", "net"));
            DirectoryInfo patchDir = new DirectoryInfo(Path.Combine("CraftBukkit", "nms-patches"));
            var craftrepo = new Repository("CraftBukkit");
            foreach (FileInfo file in patchDir.EnumerateFiles())
            {
                String targetFile = Path.Combine("net", "minecraft", "server", file.Name.Replace(".patch", ".java"));

                FileInfo clean = new FileInfo(Path.Combine("work", "decompile - " + id, targetFile));

                Console.WriteLine("Patching with " + file.Name);

                List<String> readFile = System.IO.File.ReadAllLines(file.FullName).ToList();

                // Manually append prelude if it is not found in the first few lines.
                bool preludeFound = false;
                for (int i = 0; i < Math.Min(3, readFile.Count); i++)
                {
                    if (readFile[i].StartsWith("+++"))
                    {
                        preludeFound = true;
                        break;
                    }
                }
                if (!preludeFound)
                {
                    readFile.Insert(0, "+++");
                }

                var diffs = ParseDiff.Diff.Parse(readFile.Aggregate((a, b) => a + "\r\n" + b));
                foreach (var diff in diffs)
                {
                    if (new FileInfo(Path.Combine("work", "decompile - " + id, diff.From)).FullName != clean.FullName)
                        continue;
                    var patch = new SharpDiff.Patch(new SharpDiff.FileStructure.Diff(new UnifiedDiffHeader(diff.Add, diff.Deleted), diff.Chunks.Select(c => new Chunk(new ChunkRange(new ChangeRange(c.OldStart, c.OldLines), new ChangeRange(c.NewStart, c.NewLines)), c.Changes.Select(ch => ch.Add ? new AdditionSnippet(new ILine[] { new AdditionLine(ch.Content.Substring(1)) }) : ch.Delete ? new SubtractionSnippet(new ILine[] { new SubtractionLine(ch.Content.Substring(1)) }) : (ISnippet)new ContextSnippet(new ILine[] { new ContextLine(ch.Content) })))))); //NewStart + 1 <-- only on certain files...
                    if (diff.From != diff.To) //TODO: Fix patch applying
                        System.IO.File.Delete(Path.Combine("work", "decompile - " + id, diff.From));
                    var path = Path.Combine(nmsDir.Parent.FullName, diff.To);
                    new FileInfo(path).Directory.Create();
                    System.IO.File.WriteAllText(path, patch.ApplyTo(clean.FullName));
                }
            }
            DirectoryInfo tmpNms = new DirectoryInfo(Path.Combine("CraftBukkit", "tmp-nms"));
            nmsDir.CopyTo(tmpNms.FullName, true);

            Console.WriteLine("\nCheckout, commit, checkout?");
            craftrepo.Branches.Remove("patched");
            //craftrepo.Checkout(branch, new CheckoutOptions { CheckoutModifiers = CheckoutModifiers.Force });
            Branch trackedBranch = craftrepo.Branches["origin/master"];
            Branch branch = craftrepo.CreateBranch("patched", trackedBranch.Tip);
            Branch updatedBranch = craftrepo.Branches.Update(branch,
                b => b.TrackedBranch = trackedBranch.CanonicalName);
            craftrepo.Stage("src/main/java/net/");
            var signature = new Signature("Mine.NET", "mine.net@norbipeti.github.io", DateTimeOffset.Now);
            craftrepo.Commit("CraftBukkit $ " + DateTime.Now, signature, signature);
            craftrepo.Checkout((string)obj["refs"]["CraftBukkit"]);

            tmpNms.MoveTo(nmsDir.FullName);

            Console.WriteLine("Compiling CraftBukkit...");
            RunMaven("clean install", "CraftBukkit"); //TODO: Error: Method has the same closure as...

            Console.WriteLine("Copying CraftBukkit to CraftMine.NET...");
            Directory.CreateDirectory("CraftMine.NET");
            new DirectoryInfo(Path.Combine("CraftBukkit", "src")).CopyTo("CraftMine.NET", true);

            Console.WriteLine("\n\nCloning CraftMine.NET patches...");

            Console.WriteLine("\n\n\n\n\t\tFINISHED");
        }

        private static void Pull(string name, JObject obj)
        {
            Console.WriteLine("\nPulling " + name);
            var repo = new Repository(name);
            repo.Reset(ResetMode.Hard, "origin/master");
            repo.Fetch("origin");
            Console.WriteLine("Fetched updates!");
            var reff = (string)obj["refs"][name];
            repo.Reset(ResetMode.Hard, reff);
            Console.WriteLine("Checked out: " + reff);
        }

        /// <summary>
        /// Checks a GIT tree to see if a file exists
        /// </summary>
        /// <param name="tree">The GIT tree</param>
        /// <param name="filename">The file name</param>
        /// <returns>true if file exists</returns>
        private static bool TreeContainsFile(Tree tree, string filename)
        {
            if (tree.Any(x => Path.GetFileName(x.Path) == filename))
            {
                return true;
            }
            else
            {
                foreach (Tree branch in tree.Where(x => x.TargetType == TreeEntryTargetType.Tree).Select(x => x.Target as Tree))
                {
                    if (TreeContainsFile(branch, filename))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        private static void RunMaven(string args, string wd = null)
        {
            string path = "";
            if (wd != null)
            {
                string[] spl = wd.Split(new char[] { '\\', '/' });
                for (int i = 0; i < spl.Length; i++)
                    path += ".."+Path.DirectorySeparatorChar;
            }
            string cmdargs = "/C \"" + (wd != null ? "cd " + wd + " && " : "") + Path.Combine(path, "apache-maven-3.2.5", "bin", "mvn") + " " + args + "\"";
            ProcessStartInfo psi = new ProcessStartInfo("cmd", cmdargs);
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            Process process = Process.Start(psi);
            while (!process.StandardOutput.EndOfStream)
                Console.WriteLine(process.StandardOutput.ReadLine());
            if (process.ExitCode > 0)
                throw new Exception("Process exited with code " + process.ExitCode);
        }

        private static string GetJavaInstallationPath()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
            using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey))
            {
                string currentVersion = rk.GetValue("CurrentVersion").ToString();
                using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
                {
                    return key.GetValue("JavaHome").ToString();
                }
            }
        }
    }
}
