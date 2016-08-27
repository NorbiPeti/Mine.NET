using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P6CBCompile
    {
        public static void DoIt()
        {
            if (!File.Exists(Path.Combine("CraftBukkit", "target", "craftbukkit-" + Program.Version + "-R0.1-SNAPSHOT.jar")))
            {
                Console.WriteLine("Compiling CraftBukkit...");
                Program.RunMaven("clean install", "CraftBukkit");
            }
            P7DLLConvert.DoIt();
        }
    }
}
