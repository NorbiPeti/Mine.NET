using Mine.NET.plugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mine.NET.command.defaults
{
    public class PluginsCommand : BukkitCommand
    {
        public PluginsCommand(String name) : base(name)
        {
            this.description = "Gets a list of plugins on the server";
            this.usageMessage = "/plugins";
            this.setPermission("bukkit.command.plugins");
            this.setAliases(new List<string> { "pl" });
        }

        public override bool execute(CommandSender sender, String currentAlias, String[] args)
        {
            if (!testPermission(sender)) return true;

            sender.sendMessage("Plugins " + getPluginList());
            return true;
        }

        private String getPluginList()
        {
            StringBuilder pluginList = new StringBuilder();
            Plugin[] plugins = Bukkit.getPluginManager().getPlugins();

            foreach (Plugin plugin in plugins)
            {
                if (pluginList.Length > 0)
                {
                    pluginList.Append(ChatColors.WHITE);
                    pluginList.Append(", ");
                }

                pluginList.Append(plugin.Enabled ? ChatColors.GREEN : ChatColors.RED);
                pluginList.Append(plugin.Name);
            }

            return "(" + plugins.Length + "): " + pluginList.ToString();
        }
    }
}
