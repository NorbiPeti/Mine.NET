package org.bukkit.command.defaults;

import com.google.common.collect.ImmutableList;
import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.Location;
import org.bukkit.World;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;

import java.util.List;

[Obsolete]
public class SetWorldSpawnCommand : VanillaCommand {

    public SetWorldSpawnCommand() {
        base("setworldspawn");
        this.description = "Sets a worlds's spawn point. If no coordinates are specified, the player's coordinates will be used.";
        this.usageMessage = "/setworldspawn OR /setworldspawn <x> <y> <z>";
        this.setPermission("bukkit.command.setworldspawn");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;

        Player player = null;
        World world;
        if (sender is Player) {
            player = (Player) sender;
            world = player.getWorld();
        } else {
            world = Bukkit.getWorlds().get(0);
        }

        readonly int x, y, z;

        if (args.length == 0) {
            if (player == null) {
                sender.sendMessage("You can only perform this command as a player");
                return true;
            }

            Location location = player.getLocation();

            x = location.getBlockX();
            y = location.getBlockY();
            z = location.getBlockZ();
        } else if (args.length == 3) {
            try {
                x = getInteger(sender, args[0], MIN_COORD, MAX_COORD, true);
                y = getInteger(sender, args[1], 0, world.getMaxHeight(), true);
                z = getInteger(sender, args[2], MIN_COORD, MAX_COORD, true);
            } catch (NumberFormatException ex) {
                sender.sendMessage(ex.getMessage());
                return true;
            }
        } else {
            sender.sendMessage(ChatColor.RED + "Usage: " + usageMessage);
            return false;
        }

        world.setSpawnLocation(x, y, z);

        Command.broadcastCommandMessage(sender, "Set world " + world.getName() + "'s spawnpoint to (" + x + ", " + y + ", " + z + ")");
        return true;

    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        return ImmutableList.of();
    }
}
