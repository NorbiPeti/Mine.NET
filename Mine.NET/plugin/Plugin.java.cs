using Mine.NET.command;
using Mine.NET.configuration.file;
using Mine.NET.generator;
using Mine.NET.permissions;
using System;
using System.IO;

namespace Mine.NET.plugin
{
    /**
     * Represents a Plugin
     * <p>
     * The use of {@link PluginBase} is recommended for actual Implementation
     */
    public interface Plugin : TabExecutor
    {
        /**
         * Returns the folder that the plugin data's files are located in. The
         * folder may not yet exist.
         *
         * @return The folder
         */
        DirectoryInfo getDataFolder();

        /**
         * Returns the plugin.yaml file containing the details for this plugin
         *
         * @return Contents of the plugin.yaml file
         */

        /**
         * Gets a {@link FileConfiguration} for this plugin, read through
         * "config.yml"
         * <p>
         * If there is a default config.yml embedded in this plugin, it will be
         * provided as a default for this Configuration.
         *
         * @return Plugin configuration
         */
        FileConfiguration getConfig();

        /**
         * Gets an embedded resource in this plugin
         *
         * @param filename Filename of the resource
         * @return FileInfo if found, otherwise null
         */
        MemoryStream getResource(String filename);

        /**
         * Saves the {@link FileConfiguration} retrievable by {@link #getConfig()}.
         */
        void saveConfig();
            
        /**
         * Saves the raw contents of the default config.yml file to the location
         * retrievable by {@link #getConfig()}. If there is no default config.yml
         * embedded in the plugin, an empty config.yml file is saved. This should
         * fail silently if the config.yml already exists.
         */
        void saveDefaultConfig();

        /**
         * Saves the raw contents of any resource embedded with a plugin's .jar
         * file assuming it can be found using {@link #getResource(String)}.
         * <p>
         * The resource is saved into the plugin's data folder using the same
         * hierarchy as the .jar file (subdirectories are preserved).
         *
         * @param resourcePath the embedded resource path to look for within the
         *     plugin's .jar file. (No preceding slash).
         * @param replace if true, the embedded resource will overwrite the
         *     contents of an existing file.
         * @throws ArgumentException if the resource path is null, empty,
         *     or points to a nonexistent resource.
         */
        void saveResource(String resourcePath, bool replace);

        /**
         * Discards any data in {@link #getConfig()} and reloads from disk.
         */
        void reloadConfig();

        /**
         * Gets the associated PluginLoader responsible for this plugin
         *
         * @return PluginLoader that controls this plugin
         */
        PluginLoader getPluginLoader();

        /**
         * Returns the Server instance currently running this plugin
         *
         * @return Server running this plugin
         */
        Server getServer();

        /**
         * Returns a value indicating whether or not this plugin is currently
         * enabled
         *
         * @return true if this plugin is enabled, otherwise false
         */
        bool Enabled { get; }

        /**
         * Called when this plugin is disabled
         */
        void onDisable();

        /**
         * Called after a plugin is loaded but before it has been enabled.
         * <p>
         * When mulitple plugins are loaded, the onLoad() for all plugins is
         * called before any onEnable() is called.
         */
        void onLoad();

        /**
         * Called when this plugin is enabled
         */
        void onEnable();

        /**
         * Simple bool if we can still nag to the logs about things
         *
         * @return bool whether we can nag
         */
        bool Naggable { get; }

        /**
         * Gets the {@link EbeanServer} tied to this plugin. This will only be
         * available if enabled in the {@link
         * PluginDescriptionFile#isDatabaseEnabled()}
         * <p>
         * <i>For more information on the use of <a href="http://www.avaje.org/">
         * Avaje Ebeans ORM</a>, see <a
         * href="http://www.avaje.org/ebean/documentation.html">Avaje Ebeans
         * Documentation</a></i>
         * <p>
         * <i>For an example using Ebeans ORM, see <a
         * href="https://github.com/Bukkit/HomeBukkit">Bukkit's Homebukkit Plugin
         * </a></i>
         *
         * @return ebean server instance or null if not enabled
         */
        DataMine Database { get; } //TODO

        /**
         * Gets a {@link ChunkGenerator} for use in a default world, as specified
         * in the server configuration
         *
         * @param worldName Name of the world that this will be applied to
         * @param id Unique ID, if any, that was specified to indicate which
         *     generator was requested
         * @return ChunkGenerator for use in the default world generation
         */
        ChunkGenerator getDefaultWorldGenerator(String worldName, String id);

        /**
         * Returns the plugin logger associated with this server's logger. The
         * returned logger automatically tags all log messages with the plugin's
         * name.
         *
         * @return Logger associated with this plugin
         */
        Logger Logger { get; }

        /**
         * Returns the name of the plugin.
         * <p>
         * This should return the bare name of the plugin and should be used for
         * comparison.
         *
         * @return name of the plugin
         */
        string Name { get; }

        string Description { get; }

        string[] Depends { get; }

        string[] SoftDepends { get; }

        Version Version { get; }

        PluginLoadOrder LoadOrder { get; }

        string Website { get; }

        bool HasDatabase { get; } //TODO: ?

        string[] LoadBefore { get; }

        string Prefix { get; }

        //TODO: Go through every class, if it derives from NetPlugin, store entry point, if from Command, store command
        Command[] Commands { get; }

        Permission[] Permissions { get; }

        PermissionDefaults PermissionDefaults { get; }

        string FullName { get; }
    }
}
