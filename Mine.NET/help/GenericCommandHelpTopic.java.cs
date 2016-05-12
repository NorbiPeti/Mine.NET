package org.bukkit.help;

import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.apache.commons.lang.StringUtils;
import org.bukkit.command.ConsoleCommandSender;
import org.bukkit.command.PluginCommand;
import org.bukkit.command.defaults.VanillaCommand;
import org.bukkit.help.HelpTopic;

/**
 * Lacking an alternative, the help system will create instances of
 * GenericCommandHelpTopic for each command in the server's CommandMap. You
 * can use this class as a base class for custom help topics, or as an example
 * for how to write your own.
 */
public class GenericCommandHelpTopic : HelpTopic {

    protected Command command;

    public GenericCommandHelpTopic(Command command) {
        this.command = command;

        if (command.getLabel().startsWith("/")) {
            name = command.getLabel();
        } else {
            name = "/" + command.getLabel();
        }

        // The short text is the first line of the description
        int i = command.getDescription().indexOf("\n");
        if (i > 1) {
            shortText = command.getDescription().substring(0, i - 1);
        } else {
            shortText = command.getDescription();
        }

        // Build full text
        StringBuffer sb = new StringBuffer();

        sb.Append(ChatColors.GOLD);
        sb.Append("Description: ");
        sb.Append(ChatColors.WHITE);
        sb.Append(command.getDescription());

        sb.Append("\n");

        sb.Append(ChatColors.GOLD);
        sb.Append("Usage: ");
        sb.Append(ChatColors.WHITE);
        sb.Append(command.getUsage().replace("<command>", name.substring(1)));

        if (command.getAliases().size() > 0) {
            sb.Append("\n");
            sb.Append(ChatColors.GOLD);
            sb.Append("Aliases: ");
            sb.Append(ChatColors.WHITE);
            sb.Append(ChatColors.WHITE + StringUtils.join(command.getAliases(), ", "));
        }
        fullText = sb.toString();
    }

    public bool canSee(CommandSender sender) {
        if (!command.isRegistered() && !(command is VanillaCommand)) {
            // Unregistered commands should not show up in the help (ignore VanillaCommands)
            return false;
        }

        if (sender is ConsoleCommandSender) {
            return true;
        }

        if (amendedPermission != null) {
            return sender.hasPermission(amendedPermission);
        } else {
            return command.testPermissionSilent(sender);
        }
    }
}
