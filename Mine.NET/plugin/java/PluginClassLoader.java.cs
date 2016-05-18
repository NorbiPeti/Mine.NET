namespace Mine.NET.plugin.java{

/**
 * A ClassLoader for plugins, to allow shared classes across multiple plugins
 */
sealed class PluginClassLoader : URLClassLoader {
    private readonly JavaPluginLoader loader;
    private readonly Dictionary<String, Class<?>> classes = new Dictionary<String, Class<?>>();
    private readonly PluginDescriptionFile description;
    private readonly FileInfo dataFolder;
    private readonly FileInfo file;
    readonly JavaPlugin plugin;
    private JavaPlugin pluginInit;
    private IllegalStateException pluginState;

    PluginClassLoader(JavaPluginLoader loader, readonly ClassLoader parent, readonly PluginDescriptionFile description, readonly FileInfo dataFolder, readonly FileInfo file) {
        base(new URL[] {file.toURI().toURL()}, parent);
        if(loader==null) throw new ArgumentNullException("Loader cannot be null");

        this.loader = loader;
        this.description = description;
        this.dataFolder = dataFolder;
        this.file = file;

        try {
            Class<?> jarClass;
            try {
                jarClass = Class.forName(description.getMain(), true, this);
            } catch (ClassNotFoundException ex) {
                throw new InvalidPluginException("Cannot find main class `" + description.getMain() + "'", ex);
            }

            Class<? : JavaPlugin> pluginClass;
            try {
                pluginClass = jarClass.asSubclass(JavaPlugin.class);
            } catch (ClassCastException ex) {
                throw new InvalidPluginException("main class `" + description.getMain() + "' does not extend JavaPlugin", ex);
            }

            plugin = pluginClass.newInstance();
        } catch (IllegalAccessException ex) {
            throw new InvalidPluginException("No public constructor", ex);
        } catch (InstantiationException ex) {
            throw new InvalidPluginException("Abnormal plugin type", ex);
        }
    }

    @Override
    protected Class<?> findClass(String name) {
        return findClass(name, true);
    }

    Class<?> findClass(String name, bool checkGlobal) {
        if (name.StartsWith("org.bukkit.") || name.StartsWith("net.minecraft.")) {
            throw new ClassNotFoundException(name);
        }
        Class<?> result = classes[name];

        if (result == null) {
            if (checkGlobal) {
                result = loader.getClassByName(name);
            }

            if (result == null) {
                result = base.findClass(name);

                if (result != null) {
                    loader.setClass(name, result);
                }
            }

            classes.Add(name, result);
        }

        return result;
    }

    HashSet<String> getClasses() {
        return classes.keySet();
    }

    synchronized void initialize(JavaPlugin javaPlugin) {
        if(javaPlugin==null) throw new ArgumentNullException("Initializing plugin cannot be null");
        if(javaPlugin.getClass().getClassLoader() == this) throw new ArgumentException("Cannot initialize plugin outside of this class loader");
        if (this.plugin != null || this.pluginInit != null) {
            throw new ArgumentException("Plugin already initialized!", pluginState);
        }

        pluginState = new IllegalStateException("Initial initialization");
        this.pluginInit = javaPlugin;

        javaPlugin.init(loader, loader.server, description, dataFolder, file, this);
    }
}
