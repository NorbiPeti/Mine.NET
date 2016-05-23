namespace Mine.NET.plugin{

/**
 * Handles all plugin management from the Server
 */
public sealed class SimplePluginManager : PluginManager {
    private readonly Server server;
    private readonly Dictionary<Pattern, PluginLoader> fileAssociations = new Dictionary<Pattern, PluginLoader>();
    private readonly List<Plugin> plugins = new List<Plugin>();
    private readonly Dictionary<String, Plugin> lookupNames = new Dictionary<String, Plugin>();
    private static FileInfo updateDirectory = null;
    private readonly SimpleCommandMap commandMap;
    private readonly Dictionary<String, Permission> permissions = new Dictionary<String, Permission>();
    private readonly Dictionary<bool, HashSet<Permission>> defaultPerms = new LinkedHashMap<bool, HashSet<Permission>>();
    private readonly Dictionary<String, Dictionary<Permissible, bool>> permSubs = new Dictionary<String, Dictionary<Permissible, bool>>();
    private readonly Dictionary<bool, Dictionary<Permissible, bool>> defSubs = new Dictionary<bool, Dictionary<Permissible, bool>>();
    private bool useTimings = false;

    public SimplePluginManager(Server instance, SimpleCommandMap commandMap) {
        server = instance;
        this.commandMap = commandMap;

        defaultPerms.Add(true, new HashSet<Permission>());
        defaultPerms.Add(false, new HashSet<Permission>());
    }

    /**
     * Registers the specified plugin loader
     *
     * @param loader Class name of the PluginLoader to register
     * @throws ArgumentException Thrown when the given Class is not a
     *     valid PluginLoader
     */
    public void registerInterface(Class<? : PluginLoader> loader) {
        PluginLoader instance;

        if (typeof(PluginLoader).isAssignableFrom(loader)) {
            Constructor<? : PluginLoader> constructor;

            try {
                constructor = loader.getConstructor(typeof(Server));
                instance = constructor.newInstance(server);
            } catch (NoSuchMethodException ex) {
                String className = loader.getName();

                throw new ArgumentException(String.format("Class %s does not have a public %s(Server) constructor", className, className), ex);
            } catch (Exception ex) {
                throw new ArgumentException(String.format("Unexpected exception %s while attempting to construct a new instance of %s", ex.getClass().getName(), loader.getName()), ex);
            }
        } else {
            throw new ArgumentException(String.format("Class %s does not implement interface PluginLoader", loader.getName()));
        }

        Pattern[] patterns = instance.getPluginFileFilters();

        (this) {
            foreach (Pattern pattern  in  patterns) {
                fileAssociations.Add(pattern, instance);
            }
        }
    }

    /**
     * Loads the plugins contained within the specified directory
     *
     * @param directory Directory to check for plugins
     * @return A list of all plugins loaded
     */
    public Plugin[] loadPlugins(FileInfo directory) {
        if(directory==null) throw new ArgumentNullException("Directory cannot be null");
        if(directory.isDirectory()) throw new ArgumentException("Directory must be a directory");

        List<Plugin> result = new List<Plugin>();
        HashSet<Pattern> filters = fileAssociations.keySet();

        if (!(server.getUpdateFolder().equals(""))) {
            updateDirectory = new FileInfo(directory, server.getUpdateFolder());
        }

        Dictionary<String, FileInfo> plugins = new Dictionary<String, FileInfo>();
        HashSet<String> loadedPlugins = new HashSet<String>();
        Dictionary<String, Collection<String>> dependencies = new Dictionary<String, Collection<String>>();
        Dictionary<String, Collection<String>> softDependencies = new Dictionary<String, Collection<String>>();

        // This is where it figures out all possible plugins
        foreach (FileInfo file  in  directory.listFiles()) {
            PluginLoader loader = null;
            foreach (Pattern filter  in  filters) {
                Matcher match = filter.matcher(file.getName());
                if (match.find()) {
                    loader = fileAssociations[filter];
                }
            }

            if (loader == null) continue;

            PluginDescriptionFile description = null;
            try {
                description = loader.getPluginDescription(file);
                String name = description.getName();
                if (name.equalsIgnoreCase("bukkit") || name.equalsIgnoreCase("minecraft") || name.equalsIgnoreCase("mojang")) {
                    server.getLogger().Severe("Could not load '" + file.getPath() + "' in folder '" + directory.getPath() + "': Restricted Name");
                    continue;
                } else if (description.rawName.IndexOf(' ') != -1) {
                    server.getLogger().warning(String.format(
                        "Plugin `%s' uses the space-char (0x20) in its name `%s' - this is discouraged",
                        description.getFullName(),
                        description.rawName
                        ));
                }
            } catch (InvalidDescriptionException ex) {
                server.getLogger().Severe("Could not load '" + file.getPath() + "' in folder '" + directory.getPath() + "'", ex);
                continue;
            }

            FileInfo replacedFile = plugins.Add(description.getName(), file);
            if (replacedFile != null) {
                server.getLogger().severe(String.format(
                    "Ambiguous plugin name `%s' for files `%s' and `%s' in `%s'",
                    description.getName(),
                    file.getPath(),
                    replacedFile.getPath(),
                    directory.getPath()
                    ));
            }

            Collection<String> softDependencySet = description.getSoftDepend();
            if (softDependencySet != null && !softDependencySet.isEmpty()) {
                if (softDependencies.containsKey(description.getName())) {
                    // Duplicates do not matter, they will be removed together if applicable
                    softDependencies[description.getName(]).addAll(softDependencySet);
                } else {
                    softDependencies.Add(description.getName(), new LinkedList<String>(softDependencySet));
                }
            }

            Collection<String> dependencySet = description.getDepend();
            if (dependencySet != null && !dependencySet.isEmpty()) {
                dependencies.Add(description.getName(), new LinkedList<String>(dependencySet));
            }

            Collection<String> loadBeforeSet = description.getLoadBefore();
            if (loadBeforeSet != null && !loadBeforeSet.isEmpty()) {
                foreach (String loadBeforeTarget  in  loadBeforeSet) {
                    if (softDependencies.containsKey(loadBeforeTarget)) {
                        softDependencies[loadBeforeTarget].add(description.getName());
                    } else {
                        // softDependencies is never iterated, so 'ghost' plugins aren't an issue
                        Collection<String> shortSoftDependency = new LinkedList<String>();
                        shortSoftDependency.add(description.getName());
                        softDependencies.Add(loadBeforeTarget, shortSoftDependency);
                    }
                }
            }
        }

        while (!plugins.isEmpty()) {
            bool missingDependency = true;
            IEnumerator<String> pluginIterator = plugins.keySet().iterator();

            while (pluginIterator.hasNext()) {
                String plugin = pluginIterator.next();

                if (dependencies.containsKey(plugin)) {
                    IEnumerator<String> dependencyIterator = dependencies[plugin].iterator();

                    while (dependencyIterator.hasNext()) {
                        String dependency = dependencyIterator.next();

                        // Dependency loaded
                        if (loadedPlugins.contains(dependency)) {
                            dependencyIterator.remove();

                        // We have a dependency not found
                        } else if (!plugins.containsKey(dependency)) {
                            missingDependency = false;
                            FileInfo file = plugins[plugin];
                            pluginIterator.remove();
                            softDependencies.remove(plugin);
                            dependencies.remove(plugin);

                            server.getLogger().log(
                                Level.SEVERE,
                                "Could not load '" + file.getPath() + "' in folder '" + directory.getPath() + "'",
                                new UnknownDependencyException(dependency));
                            break;
                        }
                    }

                    if (dependencies.containsKey(plugin) && dependencies[plugin].isEmpty()) {
                        dependencies.remove(plugin);
                    }
                }
                if (softDependencies.containsKey(plugin)) {
                    IEnumerator<String> softDependencyIterator = softDependencies[plugin].iterator();

                    while (softDependencyIterator.hasNext()) {
                        String softDependency = softDependencyIterator.next();

                        // Soft depend is no longer around
                        if (!plugins.containsKey(softDependency)) {
                            softDependencyIterator.remove();
                        }
                    }

                    if (softDependencies[plugin].isEmpty()) {
                        softDependencies.remove(plugin);
                    }
                }
                if (!(dependencies.containsKey(plugin) || softDependencies.containsKey(plugin)) && plugins.containsKey(plugin)) {
                    // We're clear to load, no more soft or hard dependencies left
                    FileInfo file = plugins[plugin];
                    pluginIterator.remove();
                    missingDependency = false;

                    try {
                        result.add(loadPlugin(file));
                        loadedPlugins.add(plugin);
                        continue;
                    } catch (InvalidPluginException ex) {
                        server.getLogger().Severe("Could not load '" + file.getPath() + "' in folder '" + directory.getPath() + "'", ex);
                    }
                }
            }

            if (missingDependency) {
                // We now iterate over plugins until something loads
                // This loop will ignore soft dependencies
                pluginIterator = plugins.keySet().iterator();

                while (pluginIterator.hasNext()) {
                    String plugin = pluginIterator.next();

                    if (!dependencies.containsKey(plugin)) {
                        softDependencies.remove(plugin);
                        missingDependency = false;
                        FileInfo file = plugins[plugin];
                        pluginIterator.remove();

                        try {
                            result.add(loadPlugin(file));
                            loadedPlugins.add(plugin);
                            break;
                        } catch (InvalidPluginException ex) {
                            server.getLogger().Severe("Could not load '" + file.getPath() + "' in folder '" + directory.getPath() + "'", ex);
                        }
                    }
                }
                // We have no plugins left without a depend
                if (missingDependency) {
                    softDependencies.clear();
                    dependencies.clear();
                    IEnumerator<FileInfo> failedPluginIterator = plugins.values().iterator();

                    while (failedPluginIterator.hasNext()) {
                        FileInfo file = failedPluginIterator.next();
                        failedPluginIterator.remove();
                        server.getLogger().Severe("Could not load '" + file.getPath() + "' in folder '" + directory.getPath() + "': circular dependency detected");
                    }
                }
            }
        }

        return result.toArray(new Plugin[result.Count]);
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
    public Plugin loadPlugin(FileInfo file) {
        if(file==null) throw new ArgumentNullException("FileInfo cannot be null");

        checkUpdate(file);

        HashSet<Pattern> filters = fileAssociations.keySet();
        Plugin result = null;

        foreach (Pattern filter  in  filters) {
            String name = file.getName();
            Matcher match = filter.matcher(name);

            if (match.find()) {
                PluginLoader loader = fileAssociations[filter];

                result = loader.loadPlugin(file);
            }
        }

        if (result != null) {
            plugins.add(result);
            lookupNames.Add(result.getDescription().getName(), result);
        }

        return result;
    }

    private void checkUpdate(FileInfo file) {
        if (updateDirectory == null || !updateDirectory.isDirectory()) {
            return;
        }

        FileInfo updateFile = new FileInfo(updateDirectory, file.getName());
        if (updateFile.isFile() && FileUtil.copy(updateFile, file)) {
            updateFile.delete();
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
    public Plugin getPlugin(String name) {
        return lookupNames[name.replace(' ', '_']);
    }

    public Plugin[] getPlugins() {
        return plugins.toArray(new Plugin[0]);
    }

    /**
     * Checks if the given plugin is enabled or not
     * <p>
     * Please note that the name of the plugin is case-sensitive.
     *
     * @param name Name of the plugin to check
     * @return true if the plugin is enabled, otherwise false
     */
    public bool isPluginEnabled(String name) {
        Plugin plugin = getPlugin(name);

        return isPluginEnabled(plugin);
    }

    /**
     * Checks if the given plugin is enabled or not
     *
     * @param plugin Plugin to check
     * @return true if the plugin is enabled, otherwise false
     */
    public bool isPluginEnabled(Plugin plugin) {
        if ((plugin != null) && (plugins.contains(plugin))) {
            return plugin.isEnabled();
        } else {
            return false;
        }
    }

    public void enablePlugin(Plugin plugin) {
        if (!plugin.isEnabled()) {
            List<Command> pluginCommands = PluginCommandYamlParser.parse(plugin);

            if (!pluginCommands.isEmpty()) {
                commandMap.registerAll(plugin.getDescription().getName(), pluginCommands);
            }

            try {
                plugin.getPluginLoader().enablePlugin(plugin);
            } catch (Exception ex) {
                server.getLogger().Severe("Error occurred (in the plugin loader) while enabling " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }

            HandlerList.bakeAll();
        }
    }

    public void disablePlugins() {
        Plugin[] plugins = getPlugins();
        for (int i = plugins.Length - 1; i >= 0; i--) {
            disablePlugin(plugins[i]);
        }
    }

    public void disablePlugin(Plugin plugin) {
        if (plugin.isEnabled()) {
            try {
                plugin.getPluginLoader().disablePlugin(plugin);
            } catch (Exception ex) {
                server.getLogger().Severe("Error occurred (in the plugin loader) while disabling " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }

            try {
                server.getScheduler().cancelTasks(plugin);
            } catch (Exception ex) {
                server.getLogger().Severe("Error occurred (in the plugin loader) while cancelling tasks for " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }

            try {
                server.getServicesManager().unregisterAll(plugin);
            } catch (Exception ex) {
                server.getLogger().Severe("Error occurred (in the plugin loader) while unregistering services for " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }

            try {
                HandlerList.unregisterAll(plugin);
            } catch (Exception ex) {
                server.getLogger().Severe("Error occurred (in the plugin loader) while unregistering events for " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }

            try {
                server.getMessenger().unregisterIncomingPluginChannel(plugin);
                server.getMessenger().unregisterOutgoingPluginChannel(plugin);
            } catch(Exception ex) {
                server.getLogger().Severe("Error occurred (in the plugin loader) while unregistering plugin channels for " + plugin.getDescription().getFullName() + " (Is it up to date?)", ex);
            }
        }
    }

    public void clearPlugins() {
        (this) {
            disablePlugins();
            plugins.clear();
            lookupNames.clear();
            HandlerList.unregisterAll();
            fileAssociations.clear();
            permissions.clear();
            defaultPerms[true].clear();
            defaultPerms[false].clear();
        }
    }

    /**
     * Calls an event with the given details.
     * <p>
     * This method only synchronizes when the event is not asynchronous.
     *
     * @param event Event details
     */
    public void callEvent(Event event) {
        if (event.isAsynchronous()) {
            if (Thread.holdsLock(this)) {
                throw new InvalidOperationException(event.getEventName() + " cannot be triggered asynchronously from inside code.");
            }
            if (server.isPrimaryThread()) {
                throw new InvalidOperationException(event.getEventName() + " cannot be triggered asynchronously from primary server thread.");
            }
            fireEvent(event);
        } else {
            (this) {
                fireEvent(event);
            }
        }
    }

    private void fireEvent(Event event) {
        HandlerList handlers = event.getHandlers();
        RegisteredListener[] listeners = handlers.getRegisteredListeners();

        foreach (RegisteredListener registration  in  listeners) {
            if (!registration.getPlugin().isEnabled()) {
                continue;
            }

            try {
                registration.callEvent(event);
            } catch (AuthorNagException ex) {
                Plugin plugin = registration.getPlugin();

                if (plugin.isNaggable()) {
                    plugin.setNaggable(false);

                    server.getLogger().Severe(String.format(
                            "Nag author(s): '%s' of '%s' about the following: %s",
                            plugin.getDescription().getAuthors(),
                            plugin.getDescription().getFullName(),
                            ex.getMessage()
                            ));
                }
            } catch (Exception ex) {
                server.getLogger().Severe("Could not pass event " + event.getEventName() + " to " + registration.getPlugin().getDescription().getFullName(), ex);
            }
        }
    }

    public void registerEvents(Listener listener, Plugin plugin) {
        if (!plugin.isEnabled()) {
            throw new IllegalPluginAccessException("Plugin attempted to register " + listener + " while not enabled");
        }

        foreach (KeyValuePair<Class<? : Event>, HashSet<RegisteredListener>> entry  in  plugin.getPluginLoader().createRegisteredListeners(listener, plugin).entrySet()) {
            getEventListeners(getRegistrationClass(entry.Key)).registerAll(entry.Value);
        }

    }

    public void registerEvent(Class<? : Event> event, Listener listener, EventPriority priority, EventExecutor executor, Plugin plugin) {
        registerEvent(event, listener, priority, executor, plugin, false);
    }

    /**
     * Registers the given event to the specified listener using a directly
     * passed EventExecutor
     *
     * @param event Event class to register
     * @param listener PlayerListener to register
     * @param priority Priority of this event
     * @param executor EventExecutor to register
     * @param plugin Plugin to register
     * @param ignoreCancelled Do not call executor if event was already
     *     cancelled
     */
    public void registerEvent(Class<? : Event> event, Listener listener, EventPriority priority, EventExecutor executor, Plugin plugin, bool ignoreCancelled) {
        if(listener==null) throw new ArgumentNullException("Listener cannot be null");
        if(priority==null) throw new ArgumentNullException("Priority cannot be null");
        if(executor==null) throw new ArgumentNullException("Executor cannot be null");
        if(plugin==null) throw new ArgumentNullException("Plugin cannot be null");

        if (!plugin.isEnabled()) {
            throw new IllegalPluginAccessException("Plugin attempted to register " + event + " while not enabled");
        }

        if (useTimings) {
            getEventListeners(event).register(new TimedRegisteredListener(listener, executor, priority, plugin, ignoreCancelled));
        } else {
            getEventListeners(event).register(new RegisteredListener(listener, executor, priority, plugin, ignoreCancelled));
        }
    }

    private HandlerList getEventListeners(Class<? : Event> type) {
        try {
            Method method = getRegistrationClass(type).getDeclaredMethod("getHandlerList");
            method.setAccessible(true);
            return (HandlerList) method.invoke(null);
        } catch (Exception e) {
            throw new IllegalPluginAccessException(e.ToString());
        }
    }

    private Class<? : Event> getRegistrationClass(Class<? : Event> clazz) {
        try {
            clazz.getDeclaredMethod("getHandlerList");
            return clazz;
        } catch (NoSuchMethodException e) {
            if (clazz.getSuperclass() != null
                    && !clazz.getSuperclass().equals(typeof(Event))
                    && typeof(Event).isAssignableFrom(clazz.getSuperclass())) {
                return getRegistrationClass(clazz.getSuperclass().asSubclass(typeof(Event)));
            } else {
                throw new IllegalPluginAccessException("Unable to find handler list for event " + clazz.getName() + ". Static getHandlerList method required!");
            }
        }
    }

    public Permission getPermission(String name) {
        return permissions[name.ToLower(]);
    }

    public void addPermission(Permission perm) {
        String name = perm.getName().ToLower();

        if (permissions.containsKey(name)) {
            throw new ArgumentException("The permission " + name + " is already defined!");
        }

        permissions.Add(name, perm);
        calculatePermissionDefault(perm);
    }

    public HashSet<Permission> getDefaultPermissions(bool op) {
        return ImmutableSet.copyOf(defaultPerms[op]);
    }

    public void removePermission(Permission perm) {
        removePermission(perm.getName());
    }

    public void removePermission(String name) {
        permissions.remove(name.ToLower());
    }

    public void recalculatePermissionDefaults(Permission perm) {
        if (perm != null && permissions.containsKey(perm.getName().ToLower())) {
            defaultPerms[true].remove(perm);
            defaultPerms[false].remove(perm);

            calculatePermissionDefault(perm);
        }
    }

    private void calculatePermissionDefault(Permission perm) {
        if ((perm.getDefault() == PermissionDefault.OP) || (perm.getDefault() == PermissionDefault.TRUE)) {
            defaultPerms[true].add(perm);
            dirtyPermissibles(true);
        }
        if ((perm.getDefault() == PermissionDefault.NOT_OP) || (perm.getDefault() == PermissionDefault.TRUE)) {
            defaultPerms[false].add(perm);
            dirtyPermissibles(false);
        }
    }

    private void dirtyPermissibles(bool op) {
        HashSet<Permissible> permissibles = getDefaultPermSubscriptions(op);

        foreach (Permissible p  in  permissibles) {
            p.recalculatePermissions();
        }
    }

    public void subscribeToPermission(String permission, Permissible permissible) {
        String name = permission.ToLower();
        Dictionary<Permissible, bool> map = permSubs[name];

        if (map == null) {
            map = new WeakHashMap<Permissible, bool>();
            permSubs.Add(name, map);
        }

        map.Add(permissible, true);
    }

    public void unsubscribeFromPermission(String permission, Permissible permissible) {
        String name = permission.ToLower();
        Dictionary<Permissible, bool> map = permSubs[name];

        if (map != null) {
            map.remove(permissible);

            if (map.isEmpty()) {
                permSubs.remove(name);
            }
        }
    }

    public HashSet<Permissible> getPermissionSubscriptions(String permission) {
        String name = permission.ToLower();
        Dictionary<Permissible, bool> map = permSubs[name];

        if (map == null) {
            return ImmutableSet.of();
        } else {
            return ImmutableSet.copyOf(map.keySet());
        }
    }

    public void subscribeToDefaultPerms(bool op, Permissible permissible) {
        Dictionary<Permissible, bool> map = defSubs[op];

        if (map == null) {
            map = new WeakHashMap<Permissible, bool>();
            defSubs.Add(op, map);
        }

        map.Add(permissible, true);
    }

    public void unsubscribeFromDefaultPerms(bool op, Permissible permissible) {
        Dictionary<Permissible, bool> map = defSubs[op];

        if (map != null) {
            map.remove(permissible);

            if (map.isEmpty()) {
                defSubs.remove(op);
            }
        }
    }

    public HashSet<Permissible> getDefaultPermSubscriptions(bool op) {
        Dictionary<Permissible, bool> map = defSubs[op];

        if (map == null) {
            return ImmutableSet.of();
        } else {
            return ImmutableSet.copyOf(map.keySet());
        }
    }

    public HashSet<Permission> getPermissions() {
        return new HashSet<Permission>(permissions.values());
    }

    public bool useTimings() {
        return useTimings;
    }

    /**
     * Sets whether or not per event timing code should be used
     *
     * @param use True if per event timing code should be used
     */
    public void useTimings(bool use) {
        useTimings = use;
    }
}
