using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Newtonsoft.Json.Linq;
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
            if (!Directory.Exists("Bukkit"))
            {
                Console.WriteLine("Cloning Bukkit...");
                Console.WriteLine(Repository.Clone("https://hub.spigotmc.org/stash/scm/spigot/bukkit.git", "Bukkit", options));
                Console.WriteLine();
            }
            if (!Directory.Exists("CraftBukkit"))
            {
                Console.WriteLine("Cloning CraftBukkit...");
                Console.WriteLine(Repository.Clone("https://hub.spigotmc.org/stash/scm/spigot/craftbukkit.git", "CraftBukkit", options));
                Console.WriteLine();
            }
            if (!Directory.Exists("Spigot"))
            {
                Console.WriteLine("Cloning Spigot...");
                Console.WriteLine(Repository.Clone("https://hub.spigotmc.org/stash/scm/spigot/spigot.git", "Spigot", options));
                Console.WriteLine();
            }
            if (!Directory.Exists("BuildData"))
            {
                Console.WriteLine("Cloning BuildData...");
                Console.WriteLine(Repository.Clone("https://hub.spigotmc.org/stash/scm/spigot/builddata.git", "BuildData", options));
                Console.WriteLine();
            }
            if (!File.Exists("work" + Path.DirectorySeparatorChar + "minecraft_server." + Version + ".jar"))
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
            Console.WriteLine();
            JObject obj = JObject.Parse(new WebClient().DownloadString("https://hub.spigotmc.org/versions/" + Version + ".json"));
            Pull("BuildData", obj);
            Pull("Bukkit", obj);
            Pull("CraftBukkit", obj);
            Pull("Spigot", obj);

            var info = JObject.Parse(File.ReadAllText("BuildData" + Path.DirectorySeparatorChar + "info.json"));
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
            Console.WriteLine("\nExtracting server files...");
            Directory.CreateDirectory(Path.Combine("work", "decompile - " + id, "classes"));
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


            Console.WriteLine("Applying CraftBukkit Patches");
            DirectoryInfo nmsDir = new DirectoryInfo(Path.Combine("CraftBukkit", "src", "main", "java", "net"));
            DirectoryInfo patchDir = new DirectoryInfo(Path.Combine("CraftBukkit", "nms-patches"));
            foreach (FileInfo file in patchDir.EnumerateFiles())
            {
                String targetFile = Path.Combine("net", "minecraft", "server", file.Name.Replace(".patch", ".java"));

                FileInfo clean = new FileInfo(Path.Combine("work", "decompile - " + id, targetFile));
                FileInfo t = new FileInfo(Path.Combine(nmsDir.Parent.FullName, targetFile));
                t.Directory.Create();

                Console.WriteLine("Patching with " + file.Name);

                List<String> readFile = File.ReadAllLines(file.FullName).ToList();

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

                var craftrepo = new Repository("CraftBukkit");
                //craftrepo.ApplyPatch();
                /*Patch parsedPatch = parseUnifiedDiff(readFile);
                List <?> modifiedLines = DiffUtils.patch(Files.readLines(clean, Charsets.UTF_8), parsedPatch);

                BufferedWriter bw = new BufferedWriter(new FileWriter(t));
                for (String line : (List<String>)modifiedLines)
                {
                    bw.write(line);
                    bw.newLine();
                }
                bw.close();*/ //TODO
            }
            DirectoryInfo tmpNms = new DirectoryInfo(Path.Combine("CraftBukkit", "tmp-nms"));   
            FileUtils.copyDirectory(nmsDir, tmpNms);

            craftBukkitGit.branchDelete().setBranchNames("patched").setForce(true).call();
            craftBukkitGit.checkout().setCreateBranch(true).setForce(true).setName("patched").call();
            craftBukkitGit.add().addFilepattern("src/main/java/net/").call();
            craftBukkitGit.commit().setMessage("CraftBukkit $ " + new Date()).call();
            craftBukkitGit.checkout().setName(buildInfo.getRefs().getCraftBukkit()).call();

            FileUtils.moveDirectory(tmpNms, nmsDir);

            File spigotApi = new File(spigot, "Bukkit");
            if (!spigotApi.exists())
            {
                clone("file://" + bukkit.getAbsolutePath(), spigotApi);
            }
            File spigotServer = new File(spigot, "CraftBukkit");
            if (!spigotServer.exists())
            {
                clone("file://" + craftBukkit.getAbsolutePath(), spigotServer);
            }

            // Git spigotApiGit = Git.open( spigotApi );
            // Git spigotServerGit = Git.open( spigotServer );
            if (!skipCompile)
            {
                System.out.println("Compiling Bukkit");
                runProcess(bukkit, "sh", mvn, "clean", "install");
                if (generateDocs)
                {
                    runProcess(bukkit, "sh", mvn, "javadoc:jar");
                }
                if (generateSource)
                {
                    runProcess(bukkit, "sh", mvn, "source:jar");
                }

                System.out.println("Compiling CraftBukkit");
                runProcess(craftBukkit, "sh", mvn, "clean", "install");
            }

            try
            {
                runProcess(spigot, "bash", "applyPatches.sh");
                System.out.println("*** Spigot patches applied!");

                if (!skipCompile)
                {
                    System.out.println("Compiling Spigot & Spigot-API");
                    runProcess(spigot, "sh", mvn, "clean", "install");
                }
            }
            catch (Exception ex)
            {
                System.err.println("Error compiling Spigot. Please check the wiki for FAQs.");
                System.err.println("If this does not resolve your issue then please pastebin the entire BuildTools.log.txt file when seeking support.");
                ex.printStackTrace();
                System.exit(1);
            }

            for (int i = 0; i < 35; i++)
            {
                System.out.println(" ");
            }

            if (!skipCompile)
            {
                System.out.println("Success! Everything compiled successfully. Copying final .jar files now.");
                copyJar("CraftBukkit/target", "craftbukkit", "craftbukkit-" + versionInfo.getMinecraftVersion() + ".jar");
                copyJar("Spigot/Spigot-Server/target", "spigot", "spigot-" + versionInfo.getMinecraftVersion() + ".jar");
            }

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
    }
}
