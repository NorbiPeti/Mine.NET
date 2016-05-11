package org.bukkit.event.server;

import org.bukkit.command.CommandSender;
import org.bukkit.event.HandlerList;

/**
 * This event is called when a command is recieved over RCON. See the javadocs
 * of {@link ServerCommandEvent} for more information.
 */
public class RemoteServerCommandEvent : ServerCommandEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public RemoteServerCommandEvent(CommandSender sender, readonly String command) {
        base(sender, command);
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
