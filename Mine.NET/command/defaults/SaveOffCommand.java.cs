package org.bukkit.command.defaults;

import java.util.List;

import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.World;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;

import com.google.common.collect.ImmutableList;

[Obsolete]
public class SaveOffCommand : VanillaCommand {
    public SaveOffCommand() {
        super("save-off");
        this.description = "Disables server autosaving";
        this.usageMessage = "/save-off";
        this.setPermission("bukkit.command.save.disable");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;

        for (World world : Bukkit.getWorlds()) {
            world.setAutoSave(false);
        }

        Command.broadcastCommandMessage(sender, "Disabled level saving..");
        return true;
    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) throws ArgumentException {
        Validate.notNull(sender, "Sender cannot be null");
        Validate.notNull(args, "Arguments cannot be null");
        Validate.notNull(alias, "Alias cannot be null");

        return ImmutableList.of();
    }
}
