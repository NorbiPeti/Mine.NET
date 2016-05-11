package org.bukkit.command.defaults;

import com.google.common.collect.ImmutableList;
import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.util.StringUtil;
import org.bukkit.Difficulty;

import java.util.List;
import java.util.List;

[Obsolete]
public class DifficultyCommand : VanillaCommand {
    private static readonly List<String> DIFFICULTY_NAMES = ImmutableList.of("peaceful", "easy", "normal", "hard");

    public DifficultyCommand() {
        base("difficulty");
        this.description = "Sets the game difficulty";
        this.usageMessage = "/difficulty <new difficulty> ";
        this.setPermission("bukkit.command.difficulty");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;
        if (args.length != 1 || args[0].length() == 0) {
            sender.sendMessage(ChatColor.RED + "Usage: " + usageMessage);
            return false;
        }

        Difficulty difficulty = Difficulty.getByValue(getDifficultyForString(sender, args[0]));

        if (Bukkit.isHardcore()) {
            difficulty = Difficulty.HARD;
        }

        Bukkit.getWorlds().get(0).setDifficulty(difficulty);

        int levelCount = 1;
        if (Bukkit.getAllowNether()) {
            Bukkit.getWorlds().get(levelCount).setDifficulty(difficulty);
            levelCount++;
        }

        if (Bukkit.getAllowEnd()) {
            Bukkit.getWorlds().get(levelCount).setDifficulty(difficulty);
        }

        Command.broadcastCommandMessage(sender, "Set difficulty to " + difficulty.toString());
        return true;
    }

    protected int getDifficultyForString(CommandSender sender, String name) {
        if (name.equalsIgnoreCase("peaceful") || name.equalsIgnoreCase("p")) {
            return 0;
        } else if (name.equalsIgnoreCase("easy") || name.equalsIgnoreCase("e")) {
            return 1;
        } else if (name.equalsIgnoreCase("normal") || name.equalsIgnoreCase("n")) {
            return 2;
        } else if (name.equalsIgnoreCase("hard") || name.equalsIgnoreCase("h")) {
            return 3;
        } else {
            return getInteger(sender, name, 0, 3);
        }
    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        if (args.length == 1) {
            return StringUtil.copyPartialMatches(args[0], DIFFICULTY_NAMES, new List<String>(DIFFICULTY_NAMES.size()));
        }

        return ImmutableList.of();
    }
}
