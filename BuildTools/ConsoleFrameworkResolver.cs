using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustDecompiler.External;
using Telerik.JustDecompiler.External.Interfaces;

namespace BuildTools
{
    class ConsoleFrameworkResolver : IFrameworkResolver
    {
        private readonly FrameworkVersion frameworkVersion;

        public ConsoleFrameworkResolver(FrameworkVersion frameworkVersion)
        {
            this.frameworkVersion = frameworkVersion;
        }

        public FrameworkVersion GetDefaultFallbackFramework4Version()
        {
            return this.frameworkVersion;
        }
    }
}
