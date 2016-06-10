using Mine.NET.command;
using Mine.NET.permissions;
using Mine.NET.plugin;
using Mine.NET.plugin.net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Mine.NET.plugin
{

    /**
     * Handles all plugin management from the Server
     */
    public sealed class SimplePluginManager : PluginManager
    {
        private readonly Server server;
        //private readonly Dictionary<Regex, PluginLoader> fileAssociations = new Dictionary<Regex, PluginLoader>();
        private readonly List<Plugin> plugins = new List<Plugin>();
        private readonly Dictionary<String, Plugin> lookupNames = new Dictionary<String, Plugin>();
        private static DirectoryInfo updateDirectory = null;
        private readonly SimpleCommandMap commandMap;
        private readonly Dictionary<String, Permission> permissions = new Dictionary<String, Permission>();
        private readonly Dictionary<bool, HashSet<Permission>> defaultPerms = new Dictionary<bool, HashSet<Permission>>();
        private readonly Dictionary<String, Dictionary<Permissible, bool>> permSubs = new Dictionary<String, Dictionary<Permissible, bool>>();
        private readonly Dictionary<bool, Dictionary<Permissible, bool>> defSubs = new Dictionary<bool, Dictionary<Permissible, bool>>();
        private bool usetimings = false;
        private NetPluginLoader loader = null;

        public SimplePluginManager(Server instance, SimpleCommandMap commandMap)
        {
            server = instance;
            this.commandMap = commandMap;

            defaultPerms.Add(true, new HashSet<Permission>());
            defaultPerms.Add(false, new HashSet<Permission>());

            this.loader = new NetPluginLoader(server);
        }

        /**
         * Loads the plugins contained within the specified directory
         *
         * @param directory Directory to check for plugins
         * @return A list of all plugins loaded
         */
        public Plugin[] loadPlugins(DirectoryInfo directory)
        {
            if (directory == null) throw new ArgumentNullException("Directory cannot be null");
            //if(directory.isDirectory()) throw new ArgumentException("Directory must be a directory"); - lol

            List<Plugin> result = new List<Plugin>();

            if (!(server.getUpdateFolder().Equals("")))
            {
                updateDirectory = new DirectoryInfo(Path.Combine(directory.FullName, server.getUpdateFolder()));
            }

            Dictionary<String, FileInfo> plugins = new Dictionary<String, FileInfo>();
            HashSet<String> loadedPlugins = new HashSet<String>();
            Dictionary<String, List<String>> dependencies = new Dictionary<String, List<String>>();
            Dictionary<String, List<String>> softDependencies = new Dictionary<String, List<String>>();
            Dictionary<string, Assembly> pluginasm = new Dictionary<string, Assembly>();

            // This is where it figures out all possible plugins
            foreach (FileInfo file in directory.EnumerateFiles())
            {
                if (file.Extension != "dll")
                    continue;

                Assembly asm = null;
                Plugin plugin = null;
                try
                {
                    asm = Assembly.LoadFile(file.FullName);

                    Type plugintype = null;
                    foreach (Type type in asm.GetTypes())
                    {
                        if (typeof(NetPlugin).IsAssignableFrom(type))
                        {
                            if (plugintype == null)
                                plugintype = type;
                            else
                                throw new InvalidPluginException("More than one types found that extend NetPlugin! '" + plugintype + "' and '" + type + "'");
                        }
                    }

                    if (plugintype == null)
                        throw new InvalidPluginException("Cannot find main class!");

                    var datafolder = new DirectoryInfo(file.Name); //TODO
                    plugin = (NetPlugin)Activator.CreateInstance(plugintype, loader, server, datafolder, file, asm);
                }
                catch (MethodAccessException ex)
                {
                    throw new InvalidPluginException("No public constructor", ex);
                }
                catch (TargetInvocationException ex)
                {
                    throw new InvalidPluginException("Abnormal plugin type", ex);
                }
                catch (Exception ex)
                {
                    throw new InvalidPluginException(ex);
                }

                try
                {
                    String name = plugin.Name;
                    if (name.Equals("bukkit", StringComparison.InvariantCultureIgnoreCase) || name.Equals("minecraft", StringComparison.InvariantCultureIgnoreCase) || name.Equals("mojang", StringComparison.InvariantCultureIgnoreCase))
                    {
                        server.getLogger().Severe("Could not load '" + file.Name + "' in folder '" + directory.Name + "': Restricted Name");
                        continue;
                    }
                    else if (plugin.Name.IndexOf(' ') != -1)
                    {
                        server.getLogger().Warning(String.Format(
                            "Plugin `%s' uses the space-char (0x20) in its name `%s' - this is discouraged",
                            plugin.FullName,
                            plugin.Name
                            )); //TODO: ? - Also, rawname?
                    }
                }
                catch (InvalidDescriptionException ex)
                {
                    server.getLogger().Severe("Could not load '" + file.Name + "' in folder '" + directory.Name + "'", ex);
                    continue;
                }

                /*FileInfo replacedFile = plugins.Add(plugin.Name, file);
                if (replacedFile != null) {
                    server.getLogger().severe(String.format(
                        "Ambiguous plugin name `%s' for files `%s' and `%s' in `%s'",
                        plugin.Name,
                        file.getPath(),
                        replacedFile.getPath(),
                        directory.getPath()
                        ));
                }*/ //TODO

                Collection<String> softDependencySet = new Collection<string>(plugin.SoftDepends);
                if (softDependencySet != null && softDependencySet.Count != 0)
                {
                    if (softDependencies.ContainsKey(plugin.Name))
                    {
                        // Duplicates do not matter, they will be removed together if applicable
                        foreach (var item in softDependencySet)
                            softDependencies[plugin.Name].Add(item);
                    }
                    else
                    {
                        softDependencies.Add(plugin.Name, new List<String>(softDependencySet));
                    }
                }

                Collection<String> dependencySet = new Collection<string>(plugin.Depends);
                if (dependencySet != null && dependencySet.Count != 0)
                {
                    dependencies.Add(plugin.Name, new List<String>(dependencySet));
                }

                Collection<String> loadBeforeSet = new Collection<string>(plugin.LoadBefore);
                if (loadBeforeSet != null && loadBeforeSet.Count != 0)
                {
                    foreach (String loadBeforeTarget in loadBeforeSet)
                    {
                        if (softDependencies.ContainsKey(loadBeforeTarget))
                        {
                            softDependencies[loadBeforeTarget].Add(plugin.Name);
                        }
                        else
                        {
                            // softDependencies is never iterated, so 'ghost' plugins aren't an issue
                            List<String> shortSoftDependency = new List<String>();
                            shortSoftDependency.Add(plugin.Name);
                            softDependencies.Add(loadBeforeTarget, shortSoftDependency);
                        }
                    }
                }
            }

            while (plugins.Count != 0)
            {
                bool missingDependency = true;
                //IEnumerator<String> pluginIterator = plugins.Keys.GetEnumerator();

                //while (pluginIterator.MoveNext()) {
                plugins = plugins.Where(kv =>
                  {
                      bool keepplugin = true;
                      String plugin = kv.Key;

                      if (dependencies.ContainsKey(plugin))
                      {
                          bool dependencystop = false;
                          dependencies = dependencies.Where(dkv =>
                            {
                                if (dependencystop) //break
                                    return true;

                                bool keepdependency = true;
                                String dependency = dkv.Key;

                                // Dependency loaded
                                if (loadedPlugins.Contains(dependency))
                                {
                                    keepdependency = false;

                                    // We have a dependency not found
                                }
                                else if (!plugins.ContainsKey(dependency))
                                {
                                    missingDependency = false;
                                    FileInfo file = plugins[plugin];
                                    //pluginIterator.remove();

                                    softDependencies.Remove(plugin);
                                    dependencies.Remove(plugin);

                                    server.getLogger().Severe(
                                        "Could not load '" + file.Name + "' in folder '" + directory.Name + "'",
                                        new UnknownDependencyException(dependency));
                                    //break;
                                    dependencystop = true;
                                }
                                return keepdependency;
                            }).ToDictionary(k => k.Key, v => v.Value);

                          if (dependencies.ContainsKey(plugin) && dependencies[plugin].Count == 0)
                          {
                              dependencies.Remove(plugin);
                          }
                      }
                      if (softDependencies.ContainsKey(plugin))
                      {
                          softDependencies[plugin].RemoveAll(d => !plugins.ContainsKey(d));

                          if (softDependencies[plugin].Count == 0)
                          {
                              softDependencies.Remove(plugin);
                          }
                      }
                      if (!(dependencies.ContainsKey(plugin) || softDependencies.ContainsKey(plugin)) && plugins.ContainsKey(plugin))
                      {
                          // We're clear to load, no more soft or hard dependencies left
                          FileInfo file = plugins[plugin];
                          keepplugin = false;
                          missingDependency = false;

                          try
                          {
                              Assembly asm = null; //TODO
                              result.Add(loadPlugin(file, asm));
                              loadedPlugins.Add(plugin);
                              //continue;
                          }
                          catch (InvalidPluginException ex)
                          {
                              server.getLogger().Severe("Could not load '" + file.Name + "' in folder '" + directory.Name + "'", ex);
                          }
                      }
                      return keepplugin;
                  }).ToDictionary(k => k.Key, v => v.Value);

                if (missingDependency)
                {
                    // We now iterate over plugins until something loads
                    // This loop will ignore soft dependencies
                    bool pluginstop = false;
                    plugins.Where(kv =>
                    {
                        if (pluginstop) //break
                            return true;

                        String plugin = kv.Key;

                        if (!dependencies.ContainsKey(plugin))
                        {
                            softDependencies.Remove(plugin);
                            missingDependency = false;
                            FileInfo file = plugins[plugin];
                            //pluginIterator.remove();

                            try
                            {
                                Assembly asm = null;
                                result.Add(loadPlugin(file, asm));
                                loadedPlugins.Add(plugin);
                                //break;
                                pluginstop = true;
                            }
                            catch (InvalidPluginException ex)
                            {
                                server.getLogger().Severe("Could not load '" + file.Name + "' in folder '" + directory.Name + "'", ex);
                            }
                            return false; //Remove
                        }
                        return true;
                    });
                    // We have no plugins left without a depend
                    if (missingDependency)
                    { //TODO: Lock?
                        softDependencies.Clear();
                        dependencies.Clear();

                        /*while (failedPluginIterator.hasNext()) {
                            FileInfo file = failedPluginIterator.next();
                            failedPluginIterator.remove();
                            server.getLogger().Severe("Could not load '" + file.Name + "' in folder '" + directory.Name + "': circular dependency detected");
                        }*/

                        plugins.Values.Where(p =>
                        {
                            server.getLogger().Severe("Could not load '" + p.Name + "' in folder '" + directory.Name + "': circular dependency detected");
                            return false;
                        });
                    }
                }
            }

            return result.ToArray();
        }

        /**
         * Loads the plugin in the specified file
         * <p>
         * FileInfo must be valid according to the current enabled Plugin interfaces
         *
         * @param file FileInfo containing the plugin to load
         * @return The Plugin loaded, or null if it was invalid
         * @throws InvalidPluginException Thrown when the specified file is not a
         *     valid plugin
         * @throws UnknownDependencyException If a required dependency could not
         *     be found
         */
        public Plugin loadPlugin(FileInfo file, Assembly asm)
        {
            if (file == null) throw new ArgumentNullException("FileInfo cannot be null");

            checkUpdate(file);

            Plugin result = null;

            if (file.Extension == "dll")
            {
                result = loader.loadPlugin(file, asm, result); //TODO: Fix plugin system
            }

            if (result != null)
            {
                plugins.Add(result);
                lookupNames.Add(result.Name, result);
            }

            return result;
        }

        private void checkUpdate(FileInfo file)
        {
            if (updateDirectory == null)
            {
                return;
            }

            FileInfo updateFile = new FileInfo(Path.Combine(updateDirectory.FullName, file.Name));
            try
            {
                updateFile.CopyTo(file.FullName);
                updateFile.Delete(); //If copying succeeded
            }
            catch
            {
            }
        }

        /**
         * Checks if the given plugin is loaded and returns it when applicable
         * <p>
         * Please note that the name of the plugin is case-sensitive
         *
         * @param name Name of the plugin to check
         * @return Plugin if it exists, otherwise null
         */
        public Plugin getPlugin(String name)
        {
            return lookupNames[name.Replace(' ', '_')];
        }

        public Plugin[] getPlugins()
        {
            return plugins.ToArray();
        }

        /**
         * Checks if the given plugin is enabled or not
         * <p>
         * Please note that the name of the plugin is case-sensitive.
         *
         * @param name Name of the plugin to check
         * @return true if the plugin is enabled, otherwise false
         */
        public bool isPluginEnabled(String name)
        {
            Plugin plugin = getPlugin(name);

            return isPluginEnabled(plugin);
        }

        /**
         * Checks if the given plugin is enabled or not
         *
         * @param plugin Plugin to check
         * @return true if the plugin is enabled, otherwise false
         */
        public bool isPluginEnabled(Plugin plugin)
        {
            if ((plugin != null) && (plugins.Contains(plugin)))
            {
                return plugin.Enabled;
            }
            else
            {
                return false;
            }
        }

        public void enablePlugin(Plugin plugin)
        {
            if (!plugin.Enabled)
            {
                Command[] pluginCommands = plugin.Commands;

                if (pluginCommands.Length != 0)
                {
                    commandMap.registerAll(plugin.Name, pluginCommands);
                }

                try
                {
                    plugin.getPluginLoader().enablePlugin(plugin);
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred (in the plugin loader) while enabling " + plugin.FullName + " (Is it up to date?)", ex);
                }

                //HandlerList.bakeAll(); //TODO: ?
            }
        }

        public void disablePlugins()
        {
            Plugin[] plugins = getPlugins();
            for (int i = plugins.Length - 1; i >= 0; i--)
            {
                disablePlugin(plugins[i]);
            }
        }

        public void disablePlugin(Plugin plugin)
        {
            if (plugin.Enabled)
            {
                try
                {
                    plugin.getPluginLoader().disablePlugin(plugin);
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred (in the plugin loader) while disabling " + plugin.FullName + " (Is it up to date?)", ex);
                }

                try
                {
                    server.getScheduler().cancelTasks(plugin);
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred (in the plugin loader) while cancelling tasks for " + plugin.FullName + " (Is it up to date?)", ex);
                }

                try
                {
                    server.getServicesManager().unregisterAll(plugin);
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred (in the plugin loader) while unregistering services for " + plugin.FullName + " (Is it up to date?)", ex);
                }

                try
                {
                    //HandlerList.unregisterAll(plugin); //TODO: Use ClearEventInvocations (Utilities.cs) for all events (maybe use reflection to get all events)
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred (in the plugin loader) while unregistering events for " + plugin.FullName + " (Is it up to date?)", ex);
                }

                try
                {
                    server.getMessenger().unregisterIncomingPluginChannel(plugin);
                    server.getMessenger().unregisterOutgoingPluginChannel(plugin);
                }
                catch (Exception ex)
                {
                    server.getLogger().Severe("Error occurred (in the plugin loader) while unregistering plugin channels for " + plugin.FullName + " (Is it up to date?)", ex);
                }
            }
        }

        public void clearPlugins()
        {
            lock (this)
            {
                disablePlugins();
                plugins.Clear();
                lookupNames.Clear();
                //HandlerList.unregisterAll(); //TODO
                //fileAssociations.clear();
                permissions.Clear();
                defaultPerms[true].Clear();
                defaultPerms[false].Clear();
            }
        }

        public Permission getPermission(String name)
        {
            return permissions[name.ToLower()];
        }

        public void addPermission(Permission perm)
        {
            String name = perm.getName().ToLower();

            if (permissions.ContainsKey(name))
            {
                throw new ArgumentException("The permission " + name + " is already defined!");
            }

            permissions.Add(name, perm);
            calculatePermissionDefault(perm);
        }

        public HashSet<Permission> getDefaultPermissions(bool op)
        {
            return new HashSet<Permission>(defaultPerms[op]);
        }

        public void removePermission(Permission perm)
        {
            removePermission(perm.getName());
        }

        public void removePermission(String name)
        {
            permissions.Remove(name.ToLower());
        }

        public void recalculatePermissionDefaults(Permission perm)
        {
            if (perm != null && permissions.ContainsKey(perm.getName().ToLower()))
            {
                defaultPerms[true].Remove(perm);
                defaultPerms[false].Remove(perm);

                calculatePermissionDefault(perm);
            }
        }

        private void calculatePermissionDefault(Permission perm)
        {
            if ((perm.getDefault() == PermissionDefaults.OP) || (perm.getDefault() == PermissionDefaults.TRUE))
            {
                defaultPerms[true].Add(perm);
                dirtyPermissibles(true);
            }
            if ((perm.getDefault() == PermissionDefaults.NOT_OP) || (perm.getDefault() == PermissionDefaults.TRUE))
            {
                defaultPerms[false].Add(perm);
                dirtyPermissibles(false);
            }
        }

        private void dirtyPermissibles(bool op)
        {
            HashSet<Permissible> permissibles = getDefaultPermSubscriptions(op);

            foreach (Permissible p in permissibles)
            {
                p.recalculatePermissions();
            }
        }

        public void subscribeToPermission(String permission, Permissible permissible)
        {
            String name = permission.ToLower();
            Dictionary<Permissible, bool> map = permSubs[name];

            if (map == null)
            {
                map = new Dictionary<Permissible, bool>();
                permSubs.Add(name, map);
            }

            map.Add(permissible, true);
        }

        public void unsubscribeFromPermission(String permission, Permissible permissible)
        {
            String name = permission.ToLower();
            Dictionary<Permissible, bool> map = permSubs[name];

            if (map != null)
            {
                map.Remove(permissible);

                if (map.Count == 0)
                {
                    permSubs.Remove(name);
                }
            }
        }

        public HashSet<Permissible> getPermissionSubscriptions(String permission)
        {
            String name = permission.ToLower();
            Dictionary<Permissible, bool> map = permSubs[name];

            if (map == null)
            {
                return new HashSet<Permissible>();
            }
            else
            {
                return new HashSet<Permissible>(map.Keys);
            }
        }

        public void subscribeToDefaultPerms(bool op, Permissible permissible)
        {
            Dictionary<Permissible, bool> map = defSubs[op];

            if (map == null)
            {
                map = new Dictionary<Permissible, bool>();
                defSubs.Add(op, map);
            }

            map.Add(permissible, true);
        }

        public void unsubscribeFromDefaultPerms(bool op, Permissible permissible)
        {
            Dictionary<Permissible, bool> map = defSubs[op];

            if (map != null)
            {
                map.Remove(permissible);

                if (map.Count == 0)
                {
                    defSubs.Remove(op);
                }
            }
        }

        public HashSet<Permissible> getDefaultPermSubscriptions(bool op)
        {
            Dictionary<Permissible, bool> map = defSubs[op];

            if (map == null)
            {
                return new HashSet<Permissible>();
            }
            else
            {
                return new HashSet<Permissible>(map.Keys);
            }
        }

        public HashSet<Permission> getPermissions()
        {
            return new HashSet<Permission>(permissions.Values);
        }

        public bool useTimings()
        {
            return usetimings;
        }

        /**
         * Sets whether or not per event timing code should be used
         *
         * @param use True if per event timing code should be used
         */
        public void useTimings(bool use)
        {
            usetimings = use;
        }
    }
}
