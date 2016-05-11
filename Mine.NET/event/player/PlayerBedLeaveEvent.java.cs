package org.bukkit.event.player;

import org.bukkit.block.Block;
import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;

/**
 * This event is fired when the player is leaving a bed.
 */
public class PlayerBedLeaveEvent extends PlayerEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Block bed;

    public PlayerBedLeaveEvent(Player who, readonly Block bed) {
        super(who);
        this.bed = bed;
    }

    /**
     * Returns the bed block involved in this event.
     *
     * @return the bed block involved in this event
     */
    public Block getBed() {
        return bed;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
