using Mine.NET.configuration.file;
using Mine.NET.generator;
using System;
using System.IO;
using Mine.NET.command;
using System.Collections.Generic;

namespace Mine.NET.plugin
{
    /**
     * Represents a base {@link Plugin}
     * <p>
     * Extend this class if your plugin is not a {@link
     * org.bukkit.plugin.java.JavaPlugin}
     */
    public abstract class PluginBase : Plugin
    {
        public override int GetHashCode()
        {
            return getName().GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Plugin))
            {
                return false;
            }
            return getName().Equals(((Plugin)obj).getName());
        }

        public String getName()
        {
            return getDescription().getName();
        }

        public abstract DirectoryInfo getDataFolder();
        public abstract PluginDescriptionFile getDescription();
        public abstract FileConfiguration getConfig();
        public abstract MemoryStream getResource(string filename);
        public abstract void saveConfig();
        public abstract void saveDefaultConfig();
        public abstract void saveResource(string resourcePath, bool replace);
        public abstract void reloadConfig();
        public abstract PluginLoader getPluginLoader();
        public abstract Server getServer();
        public abstract bool isEnabled();
        public abstract void onDisable();
        public abstract void onLoad();
        public abstract void onEnable();
        public abstract bool isNaggable();
        public abstract void setNaggable(bool canNag);
        public abstract EbeanServer getDatabase();
        public abstract ChunkGenerator getDefaultWorldGenerator(string worldName, string id);
        public abstract Logger getLogger();
        public abstract List<string> onTabComplete(CommandSender sender, Command command, string alias, string[] args);
        public abstract bool onCommand(CommandSender sender, Command command, string label, string[] args);
    }
}
