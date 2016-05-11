package org.bukkit.command;

/**
 * This class is provided as a convenience to implement both TabCompleter and
 * CommandExecutor.
 */
public interface TabExecutor : TabCompleter, CommandExecutor {
}
