package org.bukkit.event.player;

import org.bukkit.World;
import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;

/**
 * Called when a player switches to another world.
 */
public class PlayerChangedWorldEvent : PlayerEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly World from;

    public PlayerChangedWorldEvent(Player player, readonly World from) {
        super(player);
        this.from = from;
    }

    /**
     * Gets the world the player is switching from.
     *
     * @return  player's previous world
     */
    public World getFrom() {
        return from;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
