package org.bukkit.command.defaults;

import com.google.common.collect.ImmutableList;
import org.apache.commons.lang.Validate;
import org.bukkit.ChatColor;
import org.bukkit.command.BlockCommandSender;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.util.StringUtil;

import java.util.List;
import java.util.Arrays;
import java.util.List;
import org.bukkit.Bukkit;
import org.bukkit.World;
import org.bukkit.entity.HumanEntity;

[Obsolete]
public class GameRuleCommand : VanillaCommand {
    private static readonly List<String> GAMERULE_STATES = ImmutableList.of("true", "false");

    public GameRuleCommand() {
        base("gamerule");
        this.description = "Sets a server's game rules";
        this.usageMessage = "/gamerule <rule name> <value> OR /gamerule <rule name>";
        this.setPermission("bukkit.command.gamerule");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;

        if (args.length > 0) {
            String rule = args[0];
            World world = getGameWorld(sender);

            if (world.isGameRule(rule)) {
                if (args.length > 1) {
                    String value = args[1];

                    world.setGameRuleValue(rule, value);
                    Command.broadcastCommandMessage(sender, "Game rule " + rule + " has been set to: " + value);
                } else {
                    String value = world.getGameRuleValue(rule);
                    sender.sendMessage(rule + " = " + value);
                }
            } else {
                sender.sendMessage(ChatColor.RED + "No game rule called " + rule + " is available");
            }

            return true;
        } else {
            sender.sendMessage(ChatColor.RED + "Usage: " + usageMessage);
            sender.sendMessage("Rules: " + this.createString(getGameWorld(sender).getGameRules(), 0, ", "));

            return true;
        }
    }

    private World getGameWorld(CommandSender sender) {
        if (sender is HumanEntity) {
            World world = ((HumanEntity) sender).getWorld();
            if (world != null) {
                return world;
            }
        } else if (sender is BlockCommandSender) {
            return ((BlockCommandSender) sender).getBlock().getWorld();
        }

        return Bukkit.getWorlds().get(0);
    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) throws ArgumentException {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        if (args.length == 1) {
            return StringUtil.copyPartialMatches(args[0], Arrays.asList(getGameWorld(sender).getGameRules()), new List<String>());
        }

        if (args.length == 2) {
            return StringUtil.copyPartialMatches(args[1], GAMERULE_STATES, new List<String>(GAMERULE_STATES.size()));
        }

        return ImmutableList.of();
    }
}
