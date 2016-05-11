package org.bukkit.command;

import java.util.List;

/**
 * Represents a class which can handle command tab completion and commands
 *
 * [Obsolete] Remains for plugins that would have implemented it even without
 *     functionality
 * @see TabExecutor
 */
[Obsolete]
public interface TabCommandExecutor : CommandExecutor {
    public List<String> onTabComplete();

}
