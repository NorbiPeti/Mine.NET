using Mine.NET.configuration.serialization;
using Mine.NET.Event;
using Mine.NET.Event.server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Mine.NET.plugin.net
{
    /**
     * Represents a Java plugin loader, allowing plugins in the form of .jar
     */
    public sealed class NetPluginLoader : PluginLoader
    {
        internal readonly Server server;
        //private readonly Regex[] fileFilters = new Regex[] { new Regex("\\.dll$") };
        //private readonly Dictionary<String, Type> classes = new Dictionary<String, Type>();
        //private readonly Dictionary<String, PluginClassLoader> loaders = new Dictionary<String, PluginClassLoader>();

        internal NetPluginLoader(Server server)
        {
            this.server = server;
        }

        public Plugin loadPlugin(FileInfo file, Assembly asm, Plugin plugin)
        { //TODO: Remove this?
            if (file == null) throw new ArgumentNullException("File cannot be null");

            if (!file.Exists)
            {
                throw new InvalidPluginException(new FileNotFoundException(file.FullName + " does not exist"));
            }

            DirectoryInfo parentFile = file.Directory;
            DirectoryInfo dataFolder = new DirectoryInfo(Path.Combine(parentFile.FullName, Path.GetFileNameWithoutExtension(file.Name)));
            
            //TODO: 1. Load plugin 2. Find description 3. Load and enable dependencies 4. Enable plugin

            /*foreach (String pluginName in plugin.Depends)
            {
                if (loaders == null)
                {
                    throw new UnknownDependencyException(pluginName);
                }
                PluginClassLoader current = loaders[pluginName];

                if (current == null)
                {
                    throw new UnknownDependencyException(pluginName);
                }
            }*/

            return plugin;
        }

        /*public PluginDescriptionFile getPluginDescription(FileInfo file)
        {
            if (file == null) throw new ArgumentNullException("FileInfo cannot be null");

            JarFile jar = null;
            InputStream stream = null;

            try
            {
                jar = new JarFile(file);

                JarEntry entry = jar.getJarEntry("plugin.json"); //TODO

                if (entry == null)
                {
                    throw new InvalidDescriptionException(new FileNotFoundException("Jar does not contain plugin.yml"));
                }

                stream = jar.getInputStream(entry);

                return new PluginDescriptionFile(stream);

            }
            catch (IOException ex)
            {
                throw new InvalidDescriptionException(ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidDescriptionException(ex);
            }
            finally
            {
                if (jar != null)
                {
                    try
                    {
                        jar.close();
                    }
                    catch (IOException e)
                    {
                    }
                }
                if (stream != null)
                {
                    try
                    {
                        stream.close();
                    }
                    catch (IOException e)
                    {
                    }
                }
            }
        }*/

        /*public Regex[] getPluginFileFilters()
        {
            return fileFilters; //TODO: Clone?
        }*/

        /*Type getClassByName(String name) {
            Type cachedClass = classes[name];

            if (cachedClass != null) {
                return cachedClass;
            } else {
                foreach (String current  in  loaders.keySet()) {
                    PluginClassLoader loader = loaders[current];

                    try {
                        cachedClass = loader.findClass(name, false);
                    } catch (ClassNotFoundException cnfe) {}
                    if (cachedClass != null) {
                        return cachedClass;
                    }
                }
            }
            return null;
        }*/

        /*void setClass(String name, Type clazz)
        {
            if (!classes.ContainsKey(name))
            {
                classes.Add(name, clazz);

                if (typeof(ConfigurationSerializable).IsAssignableFrom(clazz))
                {
                    ConfigurationSerialization.registerClass(serializable);
                }
            }
        }
        //Find: "(\w+)\.class" - Replace: "typeof($1)"
        private void removeClass(String name)
        {
            Type clazz = classes[name];

            try
            {
                if ((clazz != null) && (typeof(ConfigurationSerializable).IsAssignableFrom(clazz)))
                {
                    ConfigurationSerialization.unregisterClass(clazz);
                }
            }
            catch (NullReferenceException ex)
            {
                // Boggle!
                // (Native methods throwing NPEs is not fun when you can't stop it before-hand)
            }
            classes.Remove(name);
        }*/

        /*[Obsolete("Use event handlers")] //TODO: Use event handlers
        public Dictionary<T, HashSet<RegisteredListener>> createRegisteredListeners<T>(Listener listener, Plugin plugin) where T : Event.Event
        {
            throw new NotImplementedException();
        }*/

        public void enablePlugin(Plugin plugin)
        {
            if (!(plugin is NetPlugin)) throw new ArgumentException("Plugin is not associated with this PluginLoader");

            if (!plugin.Enabled)
            {
                plugin.Logger.Info("Enabling " + plugin.FullName);

                NetPlugin jPlugin = (NetPlugin)plugin;

                String pluginName = jPlugin.Name;

                /*if (!loaders.ContainsKey(pluginName))
                {
                    loaders.Add(pluginName, (PluginClassLoader)jPlugin.getClassLoader());
                }*/

                try
                {
                    jPlugin.setEnabled(true);
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred while enabling " + plugin.FullName + " (Is it up to date?)", ex);
                }

                // Perhaps abort here, rather than continue going, but as it stands,
                // an abort is not possible the way it's currently written //TODO
                server.CallEvent(new PluginEnableEventArgs(plugin));
            }
        }

        public void disablePlugin(Plugin plugin)
        {
            if (!(plugin is NetPlugin)) throw new ArgumentException("Plugin is not associated with this PluginLoader");

            if (plugin.Enabled)
            {
                String message = String.Format("Disabling %s", plugin.FullName);
                plugin.Logger.Info(message);

                server.CallEvent(new PluginDisableEventArgs(plugin));

                NetPlugin jPlugin = (NetPlugin)plugin;
                //PluginClassLoader cloader = jPlugin.getClassLoader();

                try
                {
                    jPlugin.setEnabled(false);
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred while disabling " + plugin.FullName + " (Is it up to date?)", ex);
                }

                /*loaders.Remove(jPlugin.Name);

                if (cloader is PluginClassLoader)
                {
                    PluginClassLoader loader = (PluginClassLoader)cloader;*/
                    /*HashSet<String> names = loader.getClasses();

                    foreach (String name in names)
                    {
                        removeClass(name);
                    }*/
                //}
            }
        }
    }
}
