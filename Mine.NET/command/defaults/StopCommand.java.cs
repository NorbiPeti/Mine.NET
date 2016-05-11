package org.bukkit.command.defaults;

import java.util.List;

import org.apache.commons.lang.StringUtils;
import org.apache.commons.lang.Validate;

import org.bukkit.Bukkit;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;

import com.google.common.collect.ImmutableList;

[Obsolete]
public class StopCommand : VanillaCommand {
    public StopCommand() {
        super("stop");
        this.description = "Stops the server with optional reason";
        this.usageMessage = "/stop [reason]";
        this.setPermission("bukkit.command.stop");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;

        Command.broadcastCommandMessage(sender, "Stopping the server..");
        Bukkit.shutdown();

        String reason = this.createString(args, 0);
        if (StringUtils.isNotEmpty(reason)) {
            for (Player player : Bukkit.getOnlinePlayers()) {
                player.kickPlayer(reason);
            }
        }

        return true;
    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) throws ArgumentException {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        return ImmutableList.of();
    }
}
