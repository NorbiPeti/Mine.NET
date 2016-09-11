using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P9DLLAutoPatches
    {
        public static void DoIt()
        {
            Console.WriteLine("\n\nApplying CraftMine.NET auto-patches...");
            foreach (var file in from file in Directory.EnumerateFiles("CraftMine.NET") select file)
            {
                File.WriteAllLines(file, File.ReadLines(file)
                    .Where(line => !line.StartsWith("using IKVM") && !line.StartsWith("using java"))
                    .Select(line => Regex.Match(line, @"(?:(class \w+)\s*\:\s*java\.(?:\w|\.)+^)|(.+)").Groups.Cast<Group>().Last().Value)
                    ); //TODO: Copy decompiled files to another folder so it doesn't have to be done every time
            }
            PAFinished.DoIt();
        }
    }
}
