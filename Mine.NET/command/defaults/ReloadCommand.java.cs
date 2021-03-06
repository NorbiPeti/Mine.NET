using System;
using System.Collections.Generic;

namespace Mine.NET.command.defaults
{
    public class ReloadCommand : BukkitCommand
    {
        public ReloadCommand(String name) : base(name)
        {
            this.description = "Reloads the server configuration and plugins";
            this.usageMessage = "/reload";
            this.setPermission("bukkit.command.reload");
            this.setAliases(new List<string> { "rl" });
        }

        public override bool execute(CommandSender sender, String currentAlias, String[] args)
        {
            if (!testPermission(sender)) return true;

            Command.broadcastCommandMessage(sender, ChatColors.RED + "Please note that this command is not supported and may cause issues when using some plugins.");
            Command.broadcastCommandMessage(sender, ChatColors.RED + "If you encounter any issues please use the /stop command to restart your server.");
            Bukkit.reload();
            Command.broadcastCommandMessage(sender, ChatColors.GREEN + "Reload complete.");

            return true;
        }
    }
}
