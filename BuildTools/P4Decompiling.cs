using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P4Decompiling
    {
        public static void DoIt(FileInfo finalMappedJar, string id)
        {
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
                var pi = new ProcessStartInfo("java");
                pi.Arguments = "-jar BuildData/bin/fernflower.jar -dgs=1 -hdc=0 -rbr=0 -asc=1 -udv=0 \"work" + Path.DirectorySeparatorChar + "decompile - " + id + Path.DirectorySeparatorChar + "classes\" \"work" + Path.DirectorySeparatorChar + "decompile - " + id + "\"";
                Process p = Process.Start(pi);
                while (!p.StandardOutput.EndOfStream)
                    Console.WriteLine(p.StandardOutput.ReadLine());
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception(p.ExitCode.ToString());
            }
            P5CBPatches.DoIt(id);
        }
    }
}
