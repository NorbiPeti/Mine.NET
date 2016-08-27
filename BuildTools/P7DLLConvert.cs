using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuildTools
{
    public static class P7DLLConvert
    {
        private static Process process = null;
        public static void DoIt()
        {
            Console.WriteLine("Converting JAR to .NET DLL...");
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.WorkingDirectory = Environment.CurrentDirectory;
            psi.FileName = "IKVM_BIN" + Path.DirectorySeparatorChar + "ikvmc";
            psi.Arguments = "-target:library " + Path.Combine("CraftBukkit", "target", "craftbukkit-" + Program.Version + "-R0.1-SNAPSHOT.jar");
            process = Process.Start(psi);
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
            if (process.ExitCode > 0)
                throw new Exception("IKVM exited with code " + process.ExitCode);
            else
                P8DLLDecompile.DoIt();
        }

        private static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
