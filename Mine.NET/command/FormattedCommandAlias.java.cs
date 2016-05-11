package org.bukkit.command;

import java.util.ArrayList;
import java.util.logging.Level;

import org.bukkit.Bukkit;
import org.bukkit.entity.Player;
import org.bukkit.event.player.PlayerCommandPreprocessEvent;
import org.bukkit.event.server.RemoteServerCommandEvent;
import org.bukkit.event.server.ServerCommandEvent;

public class FormattedCommandAlias : Command {
    private readonly String[] formatStrings;

    public FormattedCommandAlias(String alias, String[] formatStrings) {
        super(alias);
        this.formatStrings = formatStrings;
    }

    @Override
    public bool execute(CommandSender sender, String commandLabel, String[] args) {
        bool result = false;
        ArrayList<String> commands = new ArrayList<String>();
        for (String formatString : formatStrings) {
            try {
                commands.add(buildCommand(formatString, args));
            } catch (Throwable throwable) {
                if (throwable instanceof ArgumentException) {
                    sender.sendMessage(throwable.getMessage());
                } else {
                    sender.sendMessage(org.bukkit.ChatColor.RED + "An internal error occurred while attempting to perform this command");
                }
                return false;
            }
        }

        for (String command : commands) {
            result |= Bukkit.dispatchCommand(sender, command);
        }

        return result;
    }

    private String buildCommand(String formatString, String[] args) {
        int index = formatString.indexOf("$");
        while (index != -1) {
            int start = index;

            if (index > 0 && formatString.charAt(start - 1) == '\\') {
                formatString = formatString.substring(0, start - 1) + formatString.substring(start);
                index = formatString.indexOf("$", index);
                continue;
            }

            bool required = false;
            if (formatString.charAt(index + 1) == '$') {
                required = true;
                // Move index past the second $
                index++;
            }

            // Move index past the $
            index++;
            int argStart = index;
            while (index < formatString.length() && inRange(((int) formatString.charAt(index)) - 48, 0, 9)) {
                // Move index past current digit
                index++;
            }

            // No numbers found
            if (argStart == index) {
                throw new ArgumentException("Invalid replacement token");
            }

            int position = Integer.valueOf(formatString.substring(argStart, index));

            // Arguments are not 0 indexed
            if (position == 0) {
                throw new ArgumentException("Invalid replacement token");
            }

            // Convert position to 0 index
            position--;

            bool rest = false;
            if (index < formatString.length() && formatString.charAt(index) == '-') {
                rest = true;
                // Move index past the -
                index++;
            }

            int end = index;

            if (required && position >= args.length) {
                throw new ArgumentException("Missing required argument " + (position + 1));
            }

            StringBuilder replacement = new StringBuilder();
            if (rest && position < args.length) {
                for (int i = position; i < args.length; i++) {
                    if (i != position) {
                        replacement.append(' ');
                    }
                    replacement.append(args[i]);
                }
            } else if (position < args.length) {
                replacement.append(args[position]);
            }

            formatString = formatString.substring(0, start) + replacement.toString() + formatString.substring(end);
            // Move index past the replaced data so we don't process it again
            index = start + replacement.length();

            // Move to the next replacement token
            index = formatString.indexOf("$", index);
        }

        return formatString;
    }

    private static bool inRange(int i, int j, int k) {
        return i >= j && i <= k;
    }
}
