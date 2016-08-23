using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P1Clone
    {
        public static void DoIt()
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
            if (!System.IO.File.Exists("work" + Path.DirectorySeparatorChar + "minecraft_server." + Program.Version + ".jar"))
            {
                Console.WriteLine("Downloading vanilla server...");
                var client = new WebClient();
                client.DownloadFileAsync(new Uri("https://s3.amazonaws.com/Minecraft.Download/versions/" + Program.Version + "/minecraft_server." + Program.Version + ".jar"), "work" + Path.DirectorySeparatorChar + "minecraft_server." + Program.Version + ".jar");
                client.DownloadProgressChanged += ServerDownloadProgressChanged;
                client.DownloadFileCompleted += ServerDownloadFinished;
            }
            else
                P2Maven.DoIt();
        }

        private static void ServerDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rProgress: " + e.ProgressPercentage + " (" + e.BytesReceived + "/" + e.TotalBytesToReceive + ")");
        }

        private static void ServerDownloadFinished(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            P2Maven.DoIt();
        }

        private static bool CloneProgress(string progress)
        {
            Console.Write(progress);
            return true;
        }
    }
}
