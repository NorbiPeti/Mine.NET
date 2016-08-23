using LibGit2Sharp;
using Newtonsoft.Json.Linq;
using SharpDiff.FileStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P5CBPatches
    {
        public static void DoIt(string id)
        {
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
            craftrepo.Checkout((string)P2Maven.obj["refs"]["CraftBukkit"]);
            tmpNms.MoveTo(nmsDir.FullName);
            P6CBCompile.DoIt();
        }
    }
}
