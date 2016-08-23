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
    public static class P3Mapping
    {
        public static void DoIt()
        {
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
                pi.Arguments = "-jar BuildData/bin/SpecialSource-2.jar map -i work" + Path.DirectorySeparatorChar + "minecraft_server." + Program.Version + ".jar -m BuildData/mappings/" + (string)info["classMappings"] + " -o \"" + clMappedJar.FullName + "\"";
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

            Program.RunMaven("install:install-file -Dfile=\"" + finalMappedJar.FullName + "\" -Dpackaging=jar -DgroupId=org.spigotmc -DartifactId=minecraft-server -Dversion=1.9-SNAPSHOT");
            P4Decompiling.DoIt(finalMappedJar, id);
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
