using JustDecompile.EngineInfrastructure;
using JustDecompile.Tools.MSBuildProjectBuilder;
using Mono.Cecil;
using Mono.Cecil.AssemblyResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustDecompiler.External;
using Telerik.JustDecompiler.External.Interfaces;
using Telerik.JustDecompiler.Languages;
using Telerik.JustDecompiler.Languages.CSharp;

namespace BuildTools
{
    public static class P8DLLDecompile
    {
        public static void DoIt()
        {
            Console.WriteLine("Decompiling DLL...");
            AssemblyDefinition assembly = Telerik.JustDecompiler.Decompiler.Utilities.GetAssembly("craftbukkit-" + Program.Version + ".jar");
            TargetPlatform targetPlatform = assembly.MainModule.AssemblyResolver.GetTargetPlatform(assembly.MainModule.FilePath);
            var preferences = new DecompilationPreferences();
            preferences.RenameInvalidMembers = true;
            preferences.RenameInvalidNamespaces = true;
            preferences.WriteLargeNumbersInHex = false;
            MSBuildProjectBuilder builder;
            if (targetPlatform == TargetPlatform.WinRT)
            {
                builder = new WinRTProjectBuilder(assembly.MainModule.FilePath, "CraftMine.NET", LanguageFactory.GetLanguage(CSharpVersion.V6), preferences, null, NoCacheAssemblyInfoService.Instance, VisualStudioVersion.VS2015);
            }
            else
            {
                IFrameworkResolver frameworkResolver = new ConsoleFrameworkResolver(FrameworkVersion.v4_6);
                builder = new MSBuildProjectBuilder(assembly.MainModule.FilePath, "CraftMine.NET", LanguageFactory.GetLanguage(CSharpVersion.V6), frameworkResolver, preferences, null, NoCacheAssemblyInfoService.Instance, VisualStudioVersion.VS2015);
            }
            builder.ProjectFileCreated += OnProjectFileCreated;
            builder.ProjectGenerationFailure += OnProjectGenerationFailure;
            builder.ResourceWritingFailure += OnResourceWritingFailure;
            builder.ExceptionThrown += OnExceptionThrown;
        }

        private static void OnExceptionThrown(object sender, Exception e)
        {
            Console.WriteLine("Error while decompiling DLL!\n" + e);
        }

        private static void OnResourceWritingFailure(object sender, string resourceName, Exception ex)
        {
            Console.WriteLine("Error while writing resource!\nResource name: " + resourceName + "\nError: " + ex);
        }

        private static void OnProjectGenerationFailure(object sender, Exception ex)
        {
            Console.WriteLine("Error while generating project!\n" + ex);
        }

        private static void OnProjectFileCreated(object sender, ProjectFileCreated e)
        {
            if (e.HasErrors)
                Console.WriteLine("One or more error occured while creating project file!");
            else
            {
                Console.WriteLine("Project file \"" + e.Name + "\" created!");
                P9DLLPatches.DoIt();
            }
        }
    }
}
