using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P2Maven
    {
        public static JObject obj;
        
        public static void DoIt()
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
            obj = JObject.Parse(new WebClient().DownloadString("https://hub.spigotmc.org/versions/" + Program.Version + ".json"));
            Pull("BuildData", obj);
            Pull("CraftBukkit", obj);
            P3Mapping.DoIt();
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
    }
}
