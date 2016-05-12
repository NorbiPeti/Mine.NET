package org.bukkit.plugin.java;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.Reader;
import java.net.URL;
import java.net.URLConnection;
import java.nio.charset.Charset;
import java.util.List;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import org.apache.commons.lang.Validate;
import org.bukkit.Server;
import org.bukkit.Warning.WarningState;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.command.PluginCommand;
import org.bukkit.configuration.InvalidConfigurationException;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.configuration.file.YamlConfiguration;
import org.bukkit.generator.ChunkGenerator;
import org.bukkit.plugin.AuthorNagException;
import org.bukkit.plugin.PluginAwareness;
import org.bukkit.plugin.PluginBase;
import org.bukkit.plugin.PluginDescriptionFile;
import org.bukkit.plugin.PluginLoader;
import org.bukkit.plugin.PluginLogger;

import com.avaje.ebean.EbeanServer;
import com.avaje.ebean.EbeanServerFactory;
import com.avaje.ebean.config.DataSourceConfig;
import com.avaje.ebean.config.ServerConfig;
import com.avaje.ebeaninternal.api.SpiEbeanServer;
import com.avaje.ebeaninternal.server.ddl.DdlGenerator;
import com.google.common.base.Charsets;
import com.google.common.base.Preconditions;
import com.google.common.io.ByteStreams;

/**
 * Represents a Java plugin
 */
public abstract class JavaPlugin : PluginBase {
    private bool isEnabled = false;
    private PluginLoader loader = null;
    private Server server = null;
    private File file = null;
    private PluginDescriptionFile description = null;
    private File dataFolder = null;
    private ClassLoader classLoader = null;
    private bool naggable = true;
    private EbeanServer ebean = null;
    private FileConfiguration newConfig = null;
    private File configFile = null;
    private PluginLogger logger = null;

    public JavaPlugin() {
        readonly ClassLoader classLoader = this.getClass().getClassLoader();
        if (!(classLoader is PluginClassLoader)) {
            throw new IllegalStateException("JavaPlugin requires " + PluginClassLoader.class.getName());
        }
        ((PluginClassLoader) classLoader).initialize(this);
    }

    /**
     * [Obsolete] This method is intended for unit testing purposes when the
     *     other {@linkplain #JavaPlugin(JavaPluginLoader,
     *     PluginDescriptionFile, File, File) constructor} cannot be used.
     *     <p>
     *     Its existence may be temporary.
     * @param loader the plugin loader
     * @param server the server instance
     * @param description the plugin's description
     * @param dataFolder the plugin's data folder
     * @param file the location of the plugin
     */
    [Obsolete]
    protected JavaPlugin(PluginLoader loader, readonly Server server, readonly PluginDescriptionFile description, readonly File dataFolder, readonly File file) {
        readonly ClassLoader classLoader = this.getClass().getClassLoader();
        if (classLoader is PluginClassLoader) {
            throw new IllegalStateException("Cannot use initialization constructor at runtime");
        }
        init(loader, server, description, dataFolder, file, classLoader);
    }

    protected JavaPlugin(JavaPluginLoader loader, readonly PluginDescriptionFile description, readonly File dataFolder, readonly File file) {
        readonly ClassLoader classLoader = this.getClass().getClassLoader();
        if (classLoader is PluginClassLoader) {
            throw new IllegalStateException("Cannot use initialization constructor at runtime");
        }
        init(loader, loader.server, description, dataFolder, file, classLoader);
    }

    /**
     * Returns the folder that the plugin data's files are located in. The
     * folder may not yet exist.
     *
     * @return The folder.
     */
    @Override
    public readonly File getDataFolder() {
        return dataFolder;
    }

    /**
     * Gets the associated PluginLoader responsible for this plugin
     *
     * @return PluginLoader that controls this plugin
     */
    @Override
    public readonly PluginLoader getPluginLoader() {
        return loader;
    }

    /**
     * Returns the Server instance currently running this plugin
     *
     * @return Server running this plugin
     */
    @Override
    public readonly Server getServer() {
        return server;
    }

    /**
     * Returns a value indicating whether or not this plugin is currently
     * enabled
     *
     * @return true if this plugin is enabled, otherwise false
     */
    @Override
    public readonly bool isEnabled() {
        return isEnabled;
    }

    /**
     * Returns the file which contains this plugin
     *
     * @return File containing this plugin
     */
    protected File getFile() {
        return file;
    }

    /**
     * Returns the plugin.yaml file containing the details for this plugin
     *
     * @return Contents of the plugin.yaml file
     */
    @Override
    public readonly PluginDescriptionFile getDescription() {
        return description;
    }

    @Override
    public FileConfiguration getConfig() {
        if (newConfig == null) {
            reloadConfig();
        }
        return newConfig;
    }

    /**
     * Provides a reader for a text file located inside the jar.
     * <p>
     * The returned reader will read text with the UTF-8 charset.
     *
     * @param file the filename of the resource to load
     * @return null if {@link #getResource(String)} returns null
     * @throws ArgumentException if file is null
     * @see ClassLoader#getResourceAsStream(String)
     */
    @SuppressWarnings("deprecation")
    protected readonly Reader getTextResource(String file) {
        readonly InputStream in = getResource(file);

        return in == null ? null : new InputStreamReader(in, Charsets.UTF_8);
    }

    @SuppressWarnings("deprecation")
    @Override
    public void reloadConfig() {
        newConfig = YamlConfiguration.loadConfiguration(configFile);

        readonly InputStream defConfigStream = getResource("config.yml");
        if (defConfigStream == null) {
            return;
        }

        newConfig.setDefaults(YamlConfiguration.loadConfiguration(new InputStreamReader(defConfigStream, Charsets.UTF_8)));
    }

    @Override
    public void saveConfig() {
        try {
            getConfig().save(configFile);
        } catch (IOException ex) {
            logger.log(Level.SEVERE, "Could not save config to " + configFile, ex);
        }
    }

    @Override
    public void saveDefaultConfig() {
        if (!configFile.exists()) {
            saveResource("config.yml", false);
        }
    }

    @Override
    public void saveResource(String resourcePath, bool replace) {
        if (resourcePath == null || resourcePath.equals("")) {
            throw new ArgumentException("ResourcePath cannot be null or empty");
        }

        resourcePath = resourcePath.replace('\\', '/');
        InputStream in = getResource(resourcePath);
        if (in == null) {
            throw new ArgumentException("The embedded resource '" + resourcePath + "' cannot be found in " + file);
        }

        File outFile = new File(dataFolder, resourcePath);
        int lastIndex = resourcePath.lastIndexOf('/');
        File outDir = new File(dataFolder, resourcePath.substring(0, lastIndex >= 0 ? lastIndex : 0));

        if (!outDir.exists()) {
            outDir.mkdirs();
        }

        try {
            if (!outFile.exists() || replace) {
                OutputStream out = new FileOutputStream(outFile);
                byte[] buf = new byte[1024];
                int len;
                while ((len = in.read(buf)) > 0) {
                    out.write(buf, 0, len);
                }
                out.close();
                in.close();
            } else {
                logger.log(Level.WARNING, "Could not save " + outFile.getName() + " to " + outFile + " because " + outFile.getName() + " already exists.");
            }
        } catch (IOException ex) {
            logger.log(Level.SEVERE, "Could not save " + outFile.getName() + " to " + outFile, ex);
        }
    }

    @Override
    public InputStream getResource(String filename) {
        if (filename == null) {
            throw new ArgumentException("Filename cannot be null");
        }

        try {
            URL url = getClassLoader().getResource(filename);

            if (url == null) {
                return null;
            }

            URLConnection connection = url.openConnection();
            connection.setUseCaches(false);
            return connection.getInputStream();
        } catch (IOException ex) {
            return null;
        }
    }

    /**
     * Returns the ClassLoader which holds this plugin
     *
     * @return ClassLoader holding this plugin
     */
    protected readonly ClassLoader getClassLoader() {
        return classLoader;
    }

    /**
     * Sets the enabled state of this plugin
     *
     * @param enabled true if enabled, otherwise false
     */
    protected readonly void setEnabled(bool enabled) {
        if (isEnabled != enabled) {
            isEnabled = enabled;

            if (isEnabled) {
                onEnable();
            } else {
                onDisable();
            }
        }
    }

    /**
     * @param loader the plugin loader
     * @param server the server instance
     * @param description the plugin's description
     * @param dataFolder the plugin's data folder
     * @param file the location of the plugin
     * @param classLoader the class loader
     * [Obsolete] This method is legacy and will be removed - it must be
     *     replaced by the specially provided constructor(s).
     */
    [Obsolete]
    protected readonly void initialize(PluginLoader loader, Server server, PluginDescriptionFile description, File dataFolder, File file, ClassLoader classLoader) {
        if (server.getWarningState() == WarningState.OFF) {
            return;
        }
        getLogger().log(Level.WARNING, getClass().getName() + " is already initialized", server.getWarningState() == WarningState.DEFAULT ? null : new AuthorNagException("Explicit initialization"));
    }

    readonly void init(PluginLoader loader, Server server, PluginDescriptionFile description, File dataFolder, File file, ClassLoader classLoader) {
        this.loader = loader;
        this.server = server;
        this.file = file;
        this.description = description;
        this.dataFolder = dataFolder;
        this.classLoader = classLoader;
        this.configFile = new File(dataFolder, "config.yml");
        this.logger = new PluginLogger(this);

        if (description.isDatabaseEnabled()) {
            ServerConfig db = new ServerConfig();

            db.setDefaultServer(false);
            db.setRegister(false);
            db.setClasses(getDatabaseClasses());
            db.setName(description.getName());
            server.configureDbConfig(db);

            DataSourceConfig ds = db.getDataSourceConfig();

            ds.setUrl(replaceDatabaseString(ds.getUrl()));
            dataFolder.mkdirs();

            ClassLoader previous = Thread.currentThread().getContextClassLoader();

            Thread.currentThread().setContextClassLoader(classLoader);
            ebean = EbeanServerFactory.create(db);
            Thread.currentThread().setContextClassLoader(previous);
        }
    }

    /**
     * Provides a list of all classes that should be persisted in the database
     *
     * @return List of Classes that are Ebeans
     */
    public List<Class<?>> getDatabaseClasses() {
        return new List<Class<?>>();
    }

    private String replaceDatabaseString(String input) {
        input = input.replaceAll("\\{DIR\\}", dataFolder.getPath().replaceAll("\\\\", "/") + "/");
        input = input.replaceAll("\\{NAME\\}", description.getName().replaceAll("[^\\w_-]", ""));
        return input;
    }

    /**
     * Gets the initialization status of this plugin
     *
     * @return true if this plugin is initialized, otherwise false
     * [Obsolete] This method cannot return false, as {@link
     *     JavaPlugin} is now initialized in the constructor.
     */
    [Obsolete]
    public readonly bool isInitialized() {
        return true;
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public bool onCommand(CommandSender sender, Command command, String label, String[] args) {
        return false;
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public List<String> onTabComplete(CommandSender sender, Command command, String alias, String[] args) {
        return null;
    }

    /**
     * Gets the command with the given name, specific to this plugin. Commands
     * need to be registered in the {@link PluginDescriptionFile#getCommands()
     * PluginDescriptionFile} to exist at runtime.
     *
     * @param name name or alias of the command
     * @return the plugin command if found, otherwise null
     */
    public PluginCommand getCommand(String name) {
        String alias = name.ToLower();
        PluginCommand command = getServer().getPluginCommand(alias);

        if (command == null || command.getPlugin() != this) {
            command = getServer().getPluginCommand(description.getName().ToLower() + ":" + alias);
        }

        if (command != null && command.getPlugin() == this) {
            return command;
        } else {
            return null;
        }
    }

    @Override
    public void onLoad() {}

    @Override
    public void onDisable() {}

    @Override
    public void onEnable() {}

    @Override
    public ChunkGenerator getDefaultWorldGenerator(String worldName, String id) {
        return null;
    }

    @Override
    public readonly bool isNaggable() {
        return naggable;
    }

    @Override
    public readonly void setNaggable(bool canNag) {
        this.naggable = canNag;
    }

    @Override
    public EbeanServer getDatabase() {
        Preconditions.checkState(description.isDatabaseEnabled(), "Plugin does not have database: true in plugin.yml");

        return ebean;
    }

    protected void installDDL() {
        SpiEbeanServer serv = (SpiEbeanServer) getDatabase();
        DdlGenerator gen = serv.getDdlGenerator();

        gen.runScript(false, gen.generateCreateDdl());
    }

    protected void removeDDL() {
        SpiEbeanServer serv = (SpiEbeanServer) getDatabase();
        DdlGenerator gen = serv.getDdlGenerator();

        gen.runScript(true, gen.generateDropDdl());
    }

    @Override
    public readonly Logger getLogger() {
        return logger;
    }

    public override string ToString() {
        return description.getFullName();
    }

    /**
     * This method provides fast access to the plugin that has {@link
     * #getProvidingPlugin(Class) provided} the given plugin class, which is
     * usually the plugin that implemented it.
     * <p>
     * An exception to this would be if plugin's jar that contained the class
     * does not extend the class, where the intended plugin would have
     * resided in a different jar / classloader.
     *
     * @param <T> a class that : JavaPlugin
     * @param clazz the class desired
     * @return the plugin that provides and : said class
     * @throws ArgumentException if clazz is null
     * @throws ArgumentException if clazz does not extend {@link
     *     JavaPlugin}
     * @throws IllegalStateException if clazz was not provided by a plugin,
     *     for example, if called with
     *     <code>JavaPlugin.getPlugin(JavaPlugin.class)</code>
     * @throws IllegalStateException if called from the static initializer for
     *     given JavaPlugin
     * @throws ClassCastException if plugin that provided the class does not
     *     extend the class
     */
    public static <T : JavaPlugin> T getPlugin(Class<T> clazz) {
        if(clazz==null) throw new ArgumentNullException("Null class cannot have a plugin");
        if (!JavaPlugin.class.isAssignableFrom(clazz)) {
            throw new ArgumentException(clazz + " does not extend " + JavaPlugin.class);
        }
        readonly ClassLoader cl = clazz.getClassLoader();
        if (!(cl is PluginClassLoader)) {
            throw new ArgumentException(clazz + " is not initialized by " + PluginClassLoader.class);
        }
        JavaPlugin plugin = ((PluginClassLoader) cl).plugin;
        if (plugin == null) {
            throw new IllegalStateException("Cannot get plugin for " + clazz + " from a static initializer");
        }
        return clazz.cast(plugin);
    }

    /**
     * This method provides fast access to the plugin that has provided the
     * given class.
     *
     * @param clazz a class belonging to a plugin
     * @return the plugin that provided the class
     * @throws ArgumentException if the class is not provided by a
     *     JavaPlugin
     * @throws ArgumentException if class is null
     * @throws IllegalStateException if called from the static initializer for
     *     given JavaPlugin
     */
    public static JavaPlugin getProvidingPlugin(Class<?> clazz) {
        if(clazz==null) throw new ArgumentNullException("Null class cannot have a plugin");
        readonly ClassLoader cl = clazz.getClassLoader();
        if (!(cl is PluginClassLoader)) {
            throw new ArgumentException(clazz + " is not provided by " + PluginClassLoader.class);
        }
        JavaPlugin plugin = ((PluginClassLoader) cl).plugin;
        if (plugin == null) {
            throw new IllegalStateException("Cannot get plugin for " + clazz + " from a static initializer");
        }
        return plugin;
    }
}
