using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P9DLLAutoPatches
    {
        public static void DoIt()
        {
            Console.WriteLine("\n\nCloning CraftMine.NET patches...");
            PAFinished.DoIt();
        }
    }
}
