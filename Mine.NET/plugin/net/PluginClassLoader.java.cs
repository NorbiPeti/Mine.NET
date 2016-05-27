using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Mine.NET.plugin.net
{
    /**
     * A ClassLoader for plugins, to allow shared classes across multiple plugins
     */
    internal sealed class PluginClassLoader
    {
        private readonly NetPluginLoader loader;
        private readonly Dictionary<String, Type> classes = new Dictionary<String, Type>();
        private readonly DirectoryInfo dataFolder;
        private readonly FileInfo file;
        internal readonly NetPlugin plugin;
        private NetPlugin pluginInit;
        private InvalidOperationException pluginState;
        private readonly Assembly asm;

        internal PluginClassLoader(NetPluginLoader loader, Type parent, DirectoryInfo dataFolder, FileInfo file, Assembly asm)
        { //TODO: Code analysis
            if (loader == null) throw new ArgumentNullException("Loader cannot be null");

            this.loader = loader;
            this.dataFolder = dataFolder;
            this.file = file;
            this.asm = asm;
        }

        internal void initialize(NetPlugin javaPlugin)
        {
            if (javaPlugin == null) throw new ArgumentNullException("Initializing plugin cannot be null");
            //if(javaPlugin.getClass().getClassLoader() == this) throw new ArgumentException("Cannot initialize plugin outside of this class loader");
            if (this.plugin != null || this.pluginInit != null)
            {
                throw new ArgumentException("Plugin already initialized!", pluginState);
            }

            pluginState = new InvalidOperationException("Initial initialization");
            this.pluginInit = javaPlugin;

            javaPlugin.init(loader, loader.server, dataFolder, file, this);
        }

        internal Stream getResource(string filename)
        {
            return asm.GetManifestResourceStream(filename);
        }
    }
}
