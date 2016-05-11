package org.bukkit.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;

/**
 * Called when a players level changes
 */
public class PlayerLevelChangeEvent : PlayerEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly int oldLevel;
    private readonly int newLevel;

    public PlayerLevelChangeEvent(Player player, readonly int oldLevel, readonly int newLevel) {
         base(player);
         this.oldLevel = oldLevel;
         this.newLevel = newLevel;
    }

    /**
     * Gets the old level of the player
     *
     * @return The old level of the player
     */
    public int getOldLevel() {
        return oldLevel;
    }

    /**
     * Gets the new level of the player
     *
     * @return The new (current) level of the player
     */
    public int getNewLevel() {
        return newLevel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
