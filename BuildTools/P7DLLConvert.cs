using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P7DLLConvert
    {
        public static void DoIt()
        {
            Console.WriteLine("Converting JAR to .NET DLL...");
            Process.Start("IKVM_BIN" + Path.DirectorySeparatorChar + "ikvmc", "-target:library " + Path.Combine("CraftBukkit", "target", "craftbukkit-" + Program.Version + ".jar")).WaitForExit();
            P8DLLDecompile.DoIt();
        }
    }
}
