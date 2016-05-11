package org.bukkit.command.defaults;

import java.util.List;

import org.bukkit.command.Command;

public abstract class BukkitCommand : Command {
    protected BukkitCommand(String name) {
        base(name);
    }

    protected BukkitCommand(String name, String description, String usageMessage, List<String> aliases) {
        base(name, description, usageMessage, aliases);
    }
}
