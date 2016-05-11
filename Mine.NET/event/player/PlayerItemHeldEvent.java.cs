package org.bukkit.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Fired when a player changes their currently held item
 */
public class PlayerItemHeldEvent extends PlayerEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel = false;
    private readonly int previous;
    private readonly int current;

    public PlayerItemHeldEvent(Player player, readonly int previous, readonly int current) {
        super(player);
        this.previous = previous;
        this.current = current;
    }

    /**
     * Gets the previous held slot index
     *
     * @return Previous slot index
     */
    public int getPreviousSlot() {
        return previous;
    }

    /**
     * Gets the new held slot index
     *
     * @return New slot index
     */
    public int getNewSlot() {
        return current;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
