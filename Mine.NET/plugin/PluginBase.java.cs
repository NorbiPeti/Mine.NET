using Mine.NET.configuration.file;
using Mine.NET.generator;
using System;
using System.IO;
using Mine.NET.command;
using System.Collections.Generic;
using Mine.NET.permissions;

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
            return Name.GetHashCode();
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
            return Name.Equals(((Plugin)obj).Name);
        }

        public abstract DirectoryInfo getDataFolder();
        public abstract FileConfiguration getConfig();
        public abstract Stream getResource(string filename);
        public abstract void saveConfig();
        public abstract void saveDefaultConfig();
        public abstract void saveResource(string resourcePath, bool replace);
        public abstract void reloadConfig();
        public abstract PluginLoader getPluginLoader();
        public abstract Server getServer();
        public abstract bool Enabled { get; protected set; }
        public abstract void onDisable();
        public abstract void onLoad();
        public abstract void onEnable();
        public abstract bool Naggable { get; protected set; }
        public abstract DataMine Database { get; }
        public abstract ChunkGenerator getDefaultWorldGenerator(string worldName, string id);
        public abstract Logger Logger { get; }
        public abstract List<string> onTabComplete(CommandSender sender, Command command, string alias, string[] args);
        public abstract bool onCommand(CommandSender sender, Command command, string label, string[] args);

        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string[] Depends { get; }
        public virtual string[] SoftDepends { get; }
        public abstract Version Version { get; }
        public virtual PluginLoadOrder LoadOrder
        {
            get
            {
                return PluginLoadOrder.POSTWORLD;
            }
        }
        public virtual string Website
        {
            get
            {
                return "";
            }
        }
        public virtual bool HasDatabase
        {
            get
            {
                return false;
            }
        }
        public virtual string[] LoadBefore
        {
            get
            {
                return new string[0];
            }
        }
        public virtual string Prefix
        {
            get
            {
                return "";
            }
        }
        public virtual Command[] Commands
        {
            get
            {
                return new Command[0];
            }
        }
        public virtual Permission[] Permissions
        {
            get
            {
                return new Permission[0];
            }
        }
        public virtual PermissionDefaults PermissionDefaults
        {
            get
            {
                return PermissionDefaults.OP;
            }
        }
        public virtual string FullName
        {
            get
            {
                return Name;
            }
        }
    }
}
