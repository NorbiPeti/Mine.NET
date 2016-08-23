using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Newtonsoft.Json.Linq;
using SharpDiff.FileStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    static class Program //Based on Spigot BuildTools
    {
        public const string Version = "1.9";
        static void Main(string[] args)
        {
            Console.WriteLine("Started BuildTools\n");
            try
            {
                P7DLLConvert.DoIt();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fatal error during install!\n" + e);
            }
            Console.ReadLine();
        }

        public static void RunMaven(string args, string wd = null)
        {
            string path = "";
            if (wd != null)
            {
                string[] spl = wd.Split(new char[] { '\\', '/' });
                for (int i = 0; i < spl.Length; i++)
                    path += ".." + Path.DirectorySeparatorChar;
            }
            string cmdargs = "/C \"" + (wd != null ? "cd " + wd + " && " : "") + Path.Combine(path, "apache-maven-3.2.5", "bin", "mvn") + " " + args + "\"";
            ProcessStartInfo psi = new ProcessStartInfo("cmd", cmdargs);
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            Process process = Process.Start(psi);
            while (!process.StandardOutput.EndOfStream)
                Console.WriteLine(process.StandardOutput.ReadLine());
            if (process.ExitCode > 0)
                throw new Exception("Process exited with code " + process.ExitCode);
        }
    }
}
