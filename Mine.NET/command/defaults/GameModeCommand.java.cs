package org.bukkit.command.defaults;

import java.util.List;
import java.util.List;

import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.GameMode;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;
import org.bukkit.util.StringUtil;

import com.google.common.collect.ImmutableList;

[Obsolete]
public class GameModeCommand : VanillaCommand {
    private static readonly List<String> GAMEMODE_NAMES = ImmutableList.of("adventure", "creative", "survival", "spectator");

    public GameModeCommand() {
        base("gamemode");
        this.description = "Changes the player to a specific game mode";
        this.usageMessage = "/gamemode <mode> [player]";
        this.setPermission("bukkit.command.gamemode");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;
        if (args.length == 0) {
            sender.sendMessage(ChatColor.RED + "Usage: " + usageMessage);
            return false;
        }

        String modeArg = args[0];
        String playerArg = sender.getName();

        if (args.length == 2) {
            playerArg = args[1];
        }

        Player player = Bukkit.getPlayerExact(playerArg);

        if (player != null) {
            int value = -1;

            try {
                value = Integer.parseInt(modeArg);
            } catch (NumberFormatException ex) {}

            GameMode mode = GameMode.getByValue(value);

            if (mode == null) {
                if (modeArg.equalsIgnoreCase("creative") || modeArg.equalsIgnoreCase("c")) {
                    mode = GameMode.CREATIVE;
                } else if (modeArg.equalsIgnoreCase("adventure") || modeArg.equalsIgnoreCase("a")) {
                    mode = GameMode.ADVENTURE;
                } else if (modeArg.equalsIgnoreCase("spectator") || modeArg.equalsIgnoreCase("sp")) {
                    mode = GameMode.SPECTATOR;
                } else {
                    mode = GameMode.SURVIVAL;
                }
            }

            if (mode != player.getGameMode()) {
                player.setGameMode(mode);

                if (mode != player.getGameMode()) {
                    sender.sendMessage("Game mode change for " + player.getName() + " failed!");
                } else {
                    if (player == sender) {
                        Command.broadcastCommandMessage(sender, "Set own game mode to " + mode.toString() + " mode");
                    } else {
                        Command.broadcastCommandMessage(sender, "Set " + player.getName() + "'s game mode to " + mode.toString() + " mode");
                    }
                }
            } else {
                sender.sendMessage(player.getName() + " already has game mode " + mode.getValue());
            }
        } else {
            sender.sendMessage("Can't find player " + playerArg);
        }

        return true;
    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        if (args.length == 1) {
            return StringUtil.copyPartialMatches(args[0], GAMEMODE_NAMES, new List<String>(GAMEMODE_NAMES.size()));
        } else if (args.length == 2) {
            return base.tabComplete(sender, alias, args);
        }
        return ImmutableList.of();
    }
}
