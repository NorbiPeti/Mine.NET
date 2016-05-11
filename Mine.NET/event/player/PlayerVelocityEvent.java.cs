package org.bukkit.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;
import org.bukkit.util.Vector;

/**
 * Called when the velocity of a player changes.
 */
public class PlayerVelocityEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel = false;
    private Vector velocity;

    public PlayerVelocityEvent(Player player, readonly Vector velocity) {
        base(player);
        this.velocity = velocity;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Gets the velocity vector that will be sent to the player
     *
     * @return Vector the player will get
     */
    public Vector getVelocity() {
        return velocity;
    }

    /**
     * Sets the velocity vector that will be sent to the player
     *
     * @param velocity The velocity vector that will be sent to the player
     */
    public void setVelocity(Vector velocity) {
        this.velocity = velocity;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
