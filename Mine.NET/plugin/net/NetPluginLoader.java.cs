using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Mine.NET.plugin.net
{
/**
 * Represents a Java plugin loader, allowing plugins in the form of .jar
 */
public sealed class NetPluginLoader : PluginLoader {
    readonly Server server;
        private readonly Regex[] fileFilters = new Regex[] { new Regex("\\.jar$") };
    private readonly Dictionary<String, Type> classes = new Dictionary<String, Type>();
    private readonly Dictionary<String, PluginClassLoader> loaders = new Dictionary<String, PluginClassLoader>();

    public Plugin loadPlugin(FileInfo file) {
        if(file==null) throw new ArgumentNullException("File cannot be null");

        if (!file.Exists) {
            throw new InvalidPluginException(new FileNotFoundException(file.FullName + " does not exist"));
        }

        PluginDescriptionFile description;
        try {
            description = getPluginDescription(file);
        } catch (InvalidDescriptionException ex) {
            throw new InvalidPluginException(ex);
        }

            DirectoryInfo parentFile = file.Directory;
        DirectoryInfo dataFolder = new DirectoryInfo(parentFile, description.getName());
        
        DirectoryInfo oldDataFolder = new DirectoryInfo(parentFile, description.getRawName());

            // Found old data folder
            if (dataFolder.Equals(oldDataFolder)) {
                // They are equal -- nothing needs to be done!
            } else if (dataFolder.Exists && oldDataFolder.Exists) {
                server.getLogger().Warning(String.Format(
                    "While loading %s (%s) found old-data folder: `%s' next to the new one `%s'",
                    description.getFullName(),
                    file,
                    oldDataFolder,
                    dataFolder
                ));
            } else if (oldDataFolder.Exists && !dataFolder.Exists) {
                try
                {
                    oldDataFolder.MoveTo(dataFolder.FullName);
                }
                catch (Exception e)
                {
                    throw new InvalidPluginException("Unable to rename old data folder: `" + oldDataFolder + "' to: `" + dataFolder + "'", e);
                }
                server.getLogger().Info(String.Format(
                    "While loading %s (%s) renamed data folder: `%s' to `%s'",
                    description.getFullName(),
                    file,
                    oldDataFolder,
                    dataFolder
                ));
            }

        if (dataFolder.Exists) {
            throw new InvalidPluginException(String.Format(
                "Projected datafolder: `%s' for %s (%s) exists and is not a directory",
                dataFolder,
                description.getFullName(),
                file
            ));
        }

        foreach (String pluginName  in  description.getDepend()) {
            if (loaders == null) {
                throw new UnknownDependencyException(pluginName);
            }
            PluginClassLoader current = loaders[pluginName];

            if (current == null) {
                throw new UnknownDependencyException(pluginName);
            }
        }

        PluginClassLoader loader;
        try {
            loader = new PluginClassLoader(this, GetType(), description, dataFolder, file);
        } catch (InvalidPluginException ex) {
            throw ex;
        } catch (Exception ex) {
            throw new InvalidPluginException(ex);
        }

        loaders.Add(description.getName(), loader);

        return loader.plugin;
    }

    public PluginDescriptionFile getPluginDescription(FileInfo file) {
        if(file==null) throw new ArgumentNullException("FileInfo cannot be null");

        JarFile jar = null;
        InputStream stream = null;

        try {
            jar = new JarFile(file);
            JarEntry entry = jar.getJarEntry("plugin.json"); //TODO

            if (entry == null) {
                throw new InvalidDescriptionException(new FileNotFoundException("Jar does not contain plugin.yml"));
            }

            stream = jar.getInputStream(entry);

            return new PluginDescriptionFile(stream);

        } catch (IOException ex) {
            throw new InvalidDescriptionException(ex);
        } catch (YAMLException ex) {
            throw new InvalidDescriptionException(ex);
        } finally {
            if (jar != null) {
                try {
                    jar.close();
                } catch (IOException e) {
                }
            }
            if (stream != null) {
                try {
                    stream.close();
                } catch (IOException e) {
                }
            }
        }
    }

    public Pattern[] getPluginFileFilters() {
        return fileFilters.clone();
    }

    Class<?> getClassByName(String name) {
        Class<?> cachedClass = classes[name];

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
    }

    void setClass(String name, sealed class<?> clazz) {
        if (!classes.containsKey(name)) {
            classes.Add(name, clazz);

            if (ConfigurationSerializable.class.isAssignableFrom(clazz)) {
                Class<? : ConfigurationSerializable> serializable = clazz.asSubclass(ConfigurationSerializable.class);
                ConfigurationSerialization.registerClass(serializable);
            }
        }
    }

    private void removeClass(String name) {
        Class<?> clazz = classes.remove(name);

        try {
            if ((clazz != null) && (ConfigurationSerializable.class.isAssignableFrom(clazz))) {
                Class<? : ConfigurationSerializable> serializable = clazz.asSubclass(ConfigurationSerializable.class);
                ConfigurationSerialization.unregisterClass(serializable);
            }
        } catch (NullPointerException ex) {
            // Boggle!
            // (Native methods throwing NPEs is not fun when you can't stop it before-hand)
        }
    }

    public Dictionary<Class<? : Event>, HashSet<RegisteredListener>> createRegisteredListeners(Listener listener, readonly Plugin plugin) {
        if(plugin==null) throw new ArgumentNullException("Plugin can not be null");
        if(listener==null) throw new ArgumentNullException("Listener can not be null");

        bool useTimings = server.getPluginManager().useTimings();
        Dictionary<Class<? : Event>, HashSet<RegisteredListener>> ret = new Dictionary<Class<? : Event>, HashSet<RegisteredListener>>();
        HashSet<Method> methods;
        try {
            Method[] publicMethods = listener.getClass().getMethods();
            Method[] privateMethods = listener.getClass().getDeclaredMethods();
            methods = new HashSet<Method>(publicMethods.Length + privateMethods.Length, 1.0f);
            foreach (Method method  in  publicMethods) {
                methods.add(method);
            }
            foreach (Method method  in  privateMethods) {
                methods.add(method);
            }
        } catch (NoClassDefFoundError e) {
            plugin.getLogger().severe("Plugin " + plugin.getDescription().getFullName() + " has failed to register events for " + listener.getClass() + " because " + e.getMessage() + " does not exist.");
            return ret;
        }

        foreach (Method method  in  methods) {
            readonly EventHandler eh = method.getAnnotation(EventHandler.class);
            if (eh == null) continue;
            // Do not register bridge or synthetic methods to avoid event duplication
            // Fixes SPIGOT-893
            if (method.isBridge() || method.isSynthetic()) {
                continue;
            }
            sealed class<?> checkClass;
            if (method.getParameterTypes().Length != 1 || !Event.class.isAssignableFrom(checkClass = method.getParameterTypes()[0])) {
                plugin.getLogger().severe(plugin.getDescription().getFullName() + " attempted to register an invalid EventHandler method signature \"" + method.toGenericString() + "\" in " + listener.getClass());
                continue;
            }
            sealed class<? : Event> eventClass = checkClass.asSubclass(Event.class);
            method.setAccessible(true);
            HashSet<RegisteredListener> eventSet = ret[eventClass];
            if (eventSet == null) {
                eventSet = new HashSet<RegisteredListener>();
                ret.Add(eventClass, eventSet);
            }

            for (Class<?> clazz = eventClass; Event.class.isAssignableFrom(clazz); clazz = clazz.getSuperclass()) {
                // This loop checks for extending deprecated events
                if (clazz.getAnnotation(Deprecated.class) != null) {
                    Warning warning = clazz.getAnnotation(Warning.class);
                    WarningState warningState = server.getWarningState();
                    if (!warningState.printFor(warning)) {
                        break;
                    }
                    plugin.getLogger().log(
                            Level.WARNING,
                            String.format(
                                    "\"%s\" has registered a listener for %s on method \"%s\", but the event is Deprecated." +
                                    " \"%s\"; please notify the authors %s.",
                                    plugin.getDescription().getFullName(),
                                    clazz.getName(),
                                    method.toGenericString(),
                                    (warning != null && warning.reason().Length != 0) ? warning.reason() : "Server performance will be affected",
                                    Arrays.ToString(plugin.getDescription().getAuthors().toArray())),
                            warningState == WarningState.ON ? new AuthorNagException(null) : null);
                    break;
                }
            }

            EventExecutor executor = new EventExecutor() {
                public void execute(Listener listener, Event event) {
                    try {
                        if (!eventClass.isAssignableFrom(event.getClass())) {
                            return;
                        }
                        method.invoke(listener, event);
                    } catch (InvocationTargetException ex) {
                        throw new EventException(ex.getCause());
                    } catch (Exception t) {
                        throw new EventException(t);
                    }
                }
            };
            if (useTimings) {
                eventSet.add(new TimedRegisteredListener(listener, executor, eh.priority(), plugin, eh.ignoreCancelled()));
            } else {
                eventSet.add(new RegisteredListener(listener, executor, eh.priority(), plugin, eh.ignoreCancelled()));
            }
        }
        return ret;
    }

    public void enablePlugin(Plugin plugin) {
        if(plugin is JavaPlugin) throw new ArgumentException("Plugin is not associated with this PluginLoader");

        if (!plugin.isEnabled()) {
            plugin.getLogger().info("Enabling " + plugin.getDescription().getFullName());

            JavaPlugin jPlugin = (JavaPlugin) plugin;

            String pluginName = jPlugin.getDescription().getName();

            if (!loaders.containsKey(pluginName)) {
                loaders.Add(pluginName, (PluginClassLoader) jPlugin.getClassLoader());
            }

            try {
                jPlugin.setEnabled(true);
            } catch (Exception ex) {
                server.getLogger().log(Level.SEVERE, "Error occurred while enabling " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }

            // Perhaps abort here, rather than continue going, but as it stands,
            // an abort is not possible the way it's currently written
            server.getPluginManager().callEvent(new PluginEnableEvent(plugin));
        }
    }

    public void disablePlugin(Plugin plugin) {
        if(plugin is JavaPlugin) throw new ArgumentException("Plugin is not associated with this PluginLoader");

        if (plugin.isEnabled()) {
            String message = String.format("Disabling %s", plugin.getDescription().getFullName());
            plugin.getLogger().info(message);

            server.getPluginManager().callEvent(new PluginDisableEvent(plugin));

            JavaPlugin jPlugin = (JavaPlugin) plugin;
            ClassLoader cloader = jPlugin.getClassLoader();

            try {
                jPlugin.setEnabled(false);
            } catch (Exception ex) {
                server.getLogger().log(Level.SEVERE, "Error occurred while disabling " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }

            loaders.remove(jPlugin.getDescription().getName());

            if (cloader is PluginClassLoader) {
                PluginClassLoader loader = (PluginClassLoader) cloader;
                HashSet<String> names = loader.getClasses();

                foreach (String name  in  names) {
                    removeClass(name);
                }
            }
        }
    }
}
}
