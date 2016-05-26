using Mine.NET.command;
using Mine.NET.configuration.file;
using Mine.NET.generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Mine.NET.plugin.net
{

    /**
     * Represents a Java plugin
     */
    public abstract class NetPlugin : PluginBase
    {
        private bool isenabled = false;
        private PluginLoader loader = null;
        private Server server = null;
        private FileInfo file = null;
        private DirectoryInfo dataFolder = null;
        //private ClassLoader classLoader = null;
        private PluginClassLoader classLoader = null;
        private bool naggable = true;
        private FileConfiguration newConfig = null;
        private FileInfo configFile = null;
        private PluginLogger logger = null;
        private DataMine db = null;

        internal NetPlugin(PluginClassLoader loader)
        {
            /*ClassLoader classLoader = this.getClass().getClassLoader();
            if (!(classLoader is PluginClassLoader)) {
                throw new InvalidOperationException("JavaPlugin requires " + typeof(PluginClassLoader).getName());
            }*/ //TODO: Check if there's something like this in .NET
            (classLoader = loader).initialize(this);
        }

        protected NetPlugin(NetPluginLoader loader, DirectoryInfo dataFolder, FileInfo file)
        {
            /*Class0Loader classLoader = this.getClass().getClassLoader();
            if (classLoader is PluginClassLoader) {
                throw new InvalidOperationException("Cannot use initialization constructor at runtime");
            }*/
            init(loader, loader.server, dataFolder, file, classLoader);
        }

        /**
         * Returns the folder that the plugin data's files are located in. The
         * folder may not yet exist.
         *
         * @return The folder.
         */
        public override DirectoryInfo getDataFolder()
        {
            return dataFolder;
        }

        /**
         * Gets the associated PluginLoader responsible for this plugin
         *
         * @return PluginLoader that controls this plugin
         */
        public override PluginLoader getPluginLoader()
        {
            return loader;
        }

        /**
         * Returns the Server instance currently running this plugin
         *
         * @return Server running this plugin
         */
        public override Server getServer()
        {
            return server;
        }

        /**
         * Returns a value indicating whether or not this plugin is currently
         * enabled
         *
         * @return true if this plugin is enabled, otherwise false
         */
        public override bool Enabled
        {
            get
            {
                return isenabled;
            }
        }

        /**
         * Returns the file which contains this plugin
         *
         * @return FileInfo containing this plugin
         */
        protected FileInfo getFile()
        {
            return file;
        }
        
        public override FileConfiguration getConfig()
        {
            if (newConfig == null)
            {
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

        protected StreamReader getTextResource(String file)
        {
            Stream in_ = getResource(file);

            return in_ == null ? null : new StreamReader(in_, Encoding.UTF8);
        }


        public override void reloadConfig()
        {
            newConfig = JsonConfiguration.loadConfiguration(configFile);

            Stream defConfigStream = getResource("config.yml");
            if (defConfigStream == null)
            {
                return;
            }

            newConfig.setDefaults(JsonConfiguration.loadConfiguration(new StreamReader(defConfigStream, Encoding.UTF8)));
        }

        public override void saveConfig()
        {
            try
            {
                getConfig().save(configFile);
            }
            catch (IOException ex)
            {
                logger.Severe("Could not save config to " + configFile, ex);
            }
        }

        public override void saveDefaultConfig()
        {
            if (!configFile.Exists)
            {
                saveResource("config.yml", false);
            }
        }

        public override void saveResource(String resourcePath, bool replace)
        {
            if (resourcePath == null || resourcePath.Equals(""))
            {
                throw new ArgumentException("ResourcePath cannot be null or empty");
            }

            resourcePath = resourcePath.Replace('\\', Path.DirectorySeparatorChar);
            Stream in_ = getResource(resourcePath);
            if (in_ == null)
            {
                throw new ArgumentException("The embedded resource '" + resourcePath + "' cannot be found in " + file);
            }

            FileInfo outFile = new FileInfo(Path.Combine(dataFolder.FullName, resourcePath));
            int lastIndex = resourcePath.LastIndexOf('/');
            DirectoryInfo outDir = new DirectoryInfo(Path.Combine(dataFolder.FullName, resourcePath.Substring(0, lastIndex >= 0 ? lastIndex : 0)));

            if (!outDir.Exists)
            {
                outDir.Create();
            }

            try
            {
                if (!outFile.Exists || replace)
                {
                    Stream out_ = outFile.OpenWrite();
                    in_.CopyTo(out_);
                    in_.Close();
                    out_.Close();
                }
                else
                {
                    logger.Warning("Could not save " + outFile.Name + " to " + outFile + " because " + outFile.Name + " already exists.");
                }
            }
            catch (IOException ex)
            {
                logger.Severe("Could not save " + outFile.Name + " to " + outFile, ex);
            }
        }

        public override Stream getResource(String filename)
        {
            if (filename == null)
            {
                throw new ArgumentException("Filename cannot be null");
            }

            try
            {
                Stream stream = getClassLoader().getResource(filename);

                if (stream.Length == 0) //TODO
                {
                    return null;
                }

                //TODO: Separate AppDomain for the plugins
                return stream;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /**
         * Returns the ClassLoader which holds this plugin
         *
         * @return ClassLoader holding this plugin
         */
        internal PluginClassLoader getClassLoader()
        {
            return classLoader;
        }

        /**
         * Sets the enabled state of this plugin
         *
         * @param enabled true if enabled, otherwise false
         */
        internal void setEnabled(bool enabled)
        {
            if (isenabled != enabled)
            {
                isenabled = enabled;

                if (isenabled)
                {
                    onEnable();
                }
                else
                {
                    onDisable();
                }
            }
        }

        internal void init(PluginLoader loader, Server server, DirectoryInfo dataFolder, FileInfo file, PluginClassLoader classLoader)
        {
            this.loader = loader;
            this.server = server;
            this.file = file;
            this.dataFolder = dataFolder;
            this.classLoader = classLoader;
            this.configFile = new FileInfo(Path.Combine(dataFolder.FullName, "config.yml"));
            this.logger = new PluginLogger(this);

            if (this.HasDatabase)
            {
                /*ServerConfig db = new ServerConfig();

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
                Thread.currentThread().setContextClassLoader(previous);*/
                db = server.CreateDatabase();
            }
        }

        /**
         * Provides a list of all classes that should be persisted in the database
         *
         * @return List of Classes that are Ebeans
         */
        public List<Type> getDatabaseClasses()
        {
            return new List<Type>(); //TODO: ???
        }

        private String replaceDatabaseString(String input)
        {
            input = Regex.Replace(Regex.Replace(input, "\\{DIR\\}", dataFolder.FullName), "\\\\", "/") + "/";
            input = Regex.Replace(Regex.Replace(input, "\\{NAME\\}", this.Name), "[^\\w_-]", "");
            return input;
        }

        /**
         * {@inheritDoc}
         */
        public override bool onCommand(CommandSender sender, Command command, String label, String[] args)
        {
            return false;
        }

        /**
         * {@inheritDoc}
         */
        public override List<String> onTabComplete(CommandSender sender, Command command, String alias, String[] args)
        {
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
        public PluginCommand getCommand(String name)
        {
            String alias = name.ToLower();
            PluginCommand command = getServer().getPluginCommand(alias);

            if (command == null || command.getPlugin() != this)
            {
                command = getServer().getPluginCommand(this.Name.ToLower() + ":" + alias);
            }

            if (command != null && command.getPlugin() == this)
            {
                return command;
            }
            else
            {
                return null;
            }
        }

        public override void onLoad() { }

        public override void onDisable() { }

        public override void onEnable() { }

        public override ChunkGenerator getDefaultWorldGenerator(String worldName, String id)
        {
            return null;
        }

        public override bool Naggable
        {
            get
            {
                return naggable;
            }
            protected set
            {
                naggable = value;
            }
        }

        public override DataMine Database
        {
            get
            {
                if (!HasDatabase) throw new InvalidOperationException("Plugin does not have database: true in plugin.yml");

                return db;
            }
        }

        protected void installDDL()
        {
            /*SpiEbeanServer serv = (SpiEbeanServer)getDatabase();
            DdlGenerator gen = serv.getDdlGenerator();

            gen.runScript(false, gen.generateCreateDdl());*/ //TODO
        }

        protected void removeDDL()
        {
            /*SpiEbeanServer serv = (SpiEbeanServer)getDatabase();
            DdlGenerator gen = serv.getDdlGenerator();

            gen.runScript(true, gen.generateDropDdl());*/
        }

        public override Logger Logger
        {
            get
            {
                return logger;
            }
        }

        public override string ToString()
        {
            return FullName;
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
         * @throws InvalidOperationException if clazz was not provided by a plugin,
         *     for example, if called with
         *     <code>JavaPlugin.getPlugin(typeof(JavaPlugin))</code>
         * @throws InvalidOperationException if called from the static initializer for
         *     given JavaPlugin
         * @throws InvalidCastException if plugin that provided the class does not
         *     extend the class
         */
        /*public static T getPlugin<T>() where T : NetPlugin {
            if (!typeof(NetPlugin).IsAssignableFrom(clazz)) {
                throw new ArgumentException(clazz + " does not extend " + typeof(NetPlugin));
            }
            readonly ClassLoader cl = clazz.getClassLoader();
            if (!(cl is PluginClassLoader)) {
                throw new ArgumentException(clazz + " is not initialized by " + typeof(PluginClassLoader));
            }
            JavaPlugin plugin = ((PluginClassLoader) cl).plugin;
            if (plugin == null) {
                throw new InvalidOperationException("Cannot get plugin for " + clazz + " from a static initializer");
            }
            return clazz.cast(plugin);
        }*/ //TODO

        /**
         * This method provides fast access to the plugin that has provided the
         * given class.
         *
         * @param clazz a class belonging to a plugin
         * @return the plugin that provided the class
         * @throws ArgumentException if the class is not provided by a
         *     JavaPlugin
         * @throws ArgumentException if class is null
         * @throws InvalidOperationException if called from the static initializer for
         *     given JavaPlugin
         */
        /*public static NetPlugin getProvidingPlugin(Type clazz)
        {
            if (clazz == null) throw new ArgumentNullException("Null class cannot have a plugin");
            ClassLoader cl = clazz.getClassLoader();
            if (!(cl is PluginClassLoader))
            {
                throw new ArgumentException(clazz + " is not provided by " + typeof(PluginClassLoader));
            }
            NetPlugin plugin = ((PluginClassLoader)cl).plugin;
            if (plugin == null)
            {
                throw new InvalidOperationException("Cannot get plugin for " + clazz + " from a static initializer");
            }
            return plugin;
        }*/ //TODO
    }
}
