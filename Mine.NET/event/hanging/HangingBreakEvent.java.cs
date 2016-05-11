package org.bukkit.event.hanging;

import org.bukkit.entity.Hanging;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Triggered when a hanging entity is removed
 */
public class HangingBreakEvent extends HangingEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private readonly HangingBreakEvent.RemoveCause cause;

    public HangingBreakEvent(Hanging hanging, readonly HangingBreakEvent.RemoveCause cause) {
        super(hanging);
        this.cause = cause;
    }

    /**
     * Gets the cause for the hanging entity's removal
     *
     * @return the RemoveCause for the hanging entity's removal
     */
    public HangingBreakEvent.RemoveCause getCause() {
        return cause;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    /**
     * An enum to specify the cause of the removal
     */
    public enum RemoveCause {
        /**
         * Removed by an entity
         */
        ENTITY,
        /**
         * Removed by an explosion
         */
        EXPLOSION,
        /**
         * Removed by placing a block on it
         */
        OBSTRUCTION,
        /**
         * Removed by destroying the block behind it, etc
         */
        PHYSICS,
        /**
         * Removed by an uncategorised cause
         */
        DEFAULT,
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
