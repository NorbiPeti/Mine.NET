using Newtonsoft.Json.Linq;
using NGit.Api;
using NGit.Revwalk;
using NGit.Revwalk.Depthwalk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P3Mapping
    {
        public static void DoIt()
        {
            var info = JObject.Parse(File.ReadAllText("BuildData" + Path.DirectorySeparatorChar + "info.json"));
            var buildrepo = P1Clone.GetBuildDataGit();
            IEnumerable<RevCommit> mappings = buildrepo.Log()
        .AddPath("mappings/" + (string)info["accessTransforms"])
        .AddPath("mappings/" + (string)info["classMappings"])
        .AddPath("mappings/" + (string)info["memberMappings"])
        .AddPath("mappings/" + (string)info["packageMappings"])
        .SetMaxCount(1).Call();

            //MD5 mappingsHash = MD5.Create();
            string hashstr = "";
            foreach (RevCommit rev in mappings)
            {
                hashstr += rev.Name;
            }
            //String mappingsVersion = Encoding.UTF8.GetString(mappingsHash.ComputeHash(Encoding.UTF8.GetBytes(hashstr))).Substring(24); // Last 8 chars
            string id = hashstr.Substring(24); // Last 8 chars
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

                Console.WriteLine("Mapping 1");
                pi.Arguments = "-jar BuildData/bin/SpecialSource-2.jar map -i work" + Path.DirectorySeparatorChar + "minecraft_server." + Program.Version + ".jar -m BuildData/mappings/" + (string)info["classMappings"] + " -o \"" + clMappedJar.FullName + "\"";
                p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());

                Console.WriteLine("Mapping 2");
                pi.Arguments = "-jar BuildData/bin/SpecialSource-2.jar map -i \"" + clMappedJar.FullName +
                            "\" -m BuildData/mappings/" + (string)info["memberMappings"] + " -o \"" + mMappedJar.FullName + "\"";
                p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());

                Console.WriteLine("Mapping 3");
                pi.Arguments = "-jar BuildData/bin/SpecialSource.jar --kill-lvt -i \"" + mMappedJar.FullName + "\" --access-transformer BuildData/mappings/" + (string)info["accessTransforms"] +
                        " -m BuildData/mappings/" + (string)info["packageMappings"] + " -o \"" + finalMappedJar.FullName + "\"";
                p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());
            }

            Program.RunMaven("install:install-file -Dfile=\"" + finalMappedJar.FullName + "\" -Dpackaging=jar -DgroupId=org.spigotmc -DartifactId=minecraft-server -Dversion=" + Program.Version + "-SNAPSHOT");
            P4Decompiling.DoIt(finalMappedJar, id);
        }
    }
}
