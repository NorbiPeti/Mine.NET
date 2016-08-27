using NGit.Api;
using NGit.Patch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BuildTools
{
    public static class P5CBPatches
    {
        public static void DoIt(string id)
        {
            if (!File.Exists(Path.Combine("CraftBukkit", "target", "craftbukkit-" + Program.Version + "-R0.1-SNAPSHOT.jar")))
            {
                Console.WriteLine("Applying CraftBukkit Patches");
                DirectoryInfo nmsDir = new DirectoryInfo(Path.Combine("CraftBukkit", "src", "main", "java", "net"));
                DirectoryInfo patchDir = new DirectoryInfo(Path.Combine("CraftBukkit", "nms-patches"));
                //var craftrepo = new Repository("CraftBukkit");
                var craftrepo = Git.Open("CraftBukkit");
                var deletefiles = new List<string>(Directory.EnumerateFiles(Path.Combine(nmsDir.FullName, "minecraft", "server")));
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
                            readFile[i] = readFile[i].Replace("net/minecraft/server", "src/main/java/net/minecraft/server");
                            break;
                        }
                        else if (readFile[i].StartsWith("---"))
                            readFile[i] = readFile[i].Replace("net/minecraft/server", "src/main/java/net/minecraft/server");
                    }
                    if (!preludeFound)
                    {
                        readFile.Insert(0, "+++");
                    }
                    craftrepo.Apply().SetPatch(new MemoryStream(Encoding.UTF8.GetBytes(readFile.Aggregate((a, b) => a + "\n" + b)))).Call();
                    deletefiles.Remove(Path.Combine(nmsDir.Parent.FullName, targetFile));
                }
                Console.WriteLine("Deleting unnecessary files...");
                deletefiles.ForEach(file => File.Delete(file));

                /*DirectoryInfo tmpNms = new DirectoryInfo(Path.Combine("CraftBukkit", "tmp-nms"));
                nmsDir.CopyTo(tmpNms.FullName, true);
                Console.WriteLine("\nCommitting patched branch...");
                craftrepo.BranchDelete().SetBranchNames("patched").SetForce(true).Call();
                craftrepo.Checkout().SetCreateBranch(true).SetForce(true).SetName("patched").Call();
                craftrepo.Add().AddFilepattern("src/main/java/net/").Call();
                craftrepo.Commit().SetMessage("CraftBukkit $ " + DateTime.Now).Call();
                craftrepo.Checkout().SetName("master").Call();
                tmpNms.MoveTo(nmsDir.FullName);*/
                Console.WriteLine("Copying modified CraftBukkit files...");
                new DirectoryInfo("CB_Custom").CopyTo("CraftBukkit", true);
            }
            P6CBCompile.DoIt();
        }
    }
}
