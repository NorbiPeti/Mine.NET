package org.bukkit.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a player toggles their sneaking state
 */
public class PlayerToggleSneakEvent extends PlayerEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly bool isSneaking;
    private bool cancel = false;

    public PlayerToggleSneakEvent(Player player, readonly bool isSneaking) {
        super(player);
        this.isSneaking = isSneaking;
    }

    /**
     * Returns whether the player is now sneaking or not.
     *
     * @return sneaking state
     */
    public bool isSneaking() {
        return isSneaking;
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
