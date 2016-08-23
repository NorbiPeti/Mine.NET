using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P6CBCompile
    {
        public static void DoIt()
        {
            Console.WriteLine("Compiling CraftBukkit...");
            Program.RunMaven("clean install", "CraftBukkit");
            P7DLLConvert.DoIt();
        }
    }
}
