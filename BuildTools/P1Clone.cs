using NGit.Api;
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
        private static Git CraftBukkitGit = null;
        private static Git BuildDataGit = null;

        public static Git GetCraftBukkitGit()
        {
            return P1Clone.CraftBukkitGit ?? Git.Open("CraftBukkit");
        }

        public static Git GetBuildDataGit()
        {
            return P1Clone.BuildDataGit ?? Git.Open("BuildData");
        }

        public static void DoIt()
        {
            Directory.CreateDirectory("work");
            if (!Directory.Exists("CraftBukkit"))
            {
                Console.WriteLine("Cloning CraftBukkit...");
                CraftBukkitGit = Clone("https://hub.spigotmc.org/stash/scm/spigot/craftbukkit.git", "CraftBukkit");
                Console.WriteLine();
            }
            if (!Directory.Exists("BuildData"))
            {
                Console.WriteLine("Cloning BuildData...");
                BuildDataGit=Clone("https://hub.spigotmc.org/stash/scm/spigot/builddata.git", "BuildData");
                Console.WriteLine();
            }
            if (!File.Exists("work" + Path.DirectorySeparatorChar + "minecraft_server." + Program.Version + ".jar"))
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

        private static Git Clone(string url, string target)
        {
            return Git.CloneRepository().SetURI(url).SetDirectory("CraftBukkit").Call();
        }

        private static void ServerDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rProgress: " + e.ProgressPercentage + "% (" + e.BytesReceived + "/" + e.TotalBytesToReceive + ")");
        }

        private static void ServerDownloadFinished(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            P2Maven.DoIt();
        }
    }
}
