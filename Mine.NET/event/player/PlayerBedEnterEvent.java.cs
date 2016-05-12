package org.bukkit.event.player;

import org.bukkit.block.Block;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * This event is fired when the player is almost about to enter the bed.
 */
public class PlayerBedEnterEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel = false;
    private readonly Block bed;

    public PlayerBedEnterEvent(Player who, readonly Block bed) {
        base(who);
        this.bed = bed;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Returns the bed block involved in this event.
     *
     * @return the bed block involved in this event
     */
    public Block getBed() {
        return bed;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
