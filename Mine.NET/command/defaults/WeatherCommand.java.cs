package org.bukkit.command.defaults;

import com.google.common.collect.ImmutableList;
import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.World;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.util.StringUtil;

import java.util.List;
import java.util.List;
import java.util.Random;

[Obsolete]
public class WeatherCommand : VanillaCommand {
    private static readonly List<String> WEATHER_TYPES = ImmutableList.of("clear", "rain", "thunder");

    public WeatherCommand() {
        base("weather");
        this.description = "Changes the weather";
        this.usageMessage = "/weather <clear/rain/thunder> [duration in seconds]";
        this.setPermission("bukkit.command.weather");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;
        if (args.length == 0) {
            sender.sendMessage(ChatColor.RED + "Usage: " + usageMessage);
            return false;
        }

        int duration = (300 + new Random().nextInt(600)) * 20;
        if (args.length >= 2) {
            duration = getInteger(sender, args[1], 1, 1000000) * 20;
        }

        World world = Bukkit.getWorlds().get(0);

        world.setWeatherDuration(duration);
        world.setThunderDuration(duration);

        if ("clear".equalsIgnoreCase(args[0])) {
            world.setStorm(false);
            world.setThundering(false);
            Command.broadcastCommandMessage(sender, "Changed weather to clear for " + (duration / 20) + " seconds.");
        } else if ("rain".equalsIgnoreCase(args[0])) {
            world.setStorm(true);
            world.setThundering(false);
            Command.broadcastCommandMessage(sender, "Changed weather to rainy for " + (duration / 20) + " seconds.");
        } else if ("thunder".equalsIgnoreCase(args[0])) {
            world.setStorm(true);
            world.setThundering(true);
            Command.broadcastCommandMessage(sender, "Changed weather to thundering " + (duration / 20) + " seconds.");
        }

        return true;
    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        if (args.length == 1) {
            return StringUtil.copyPartialMatches(args[0], WEATHER_TYPES, new List<String>(WEATHER_TYPES.size()));
        }

        return ImmutableList.of();
    }
}
