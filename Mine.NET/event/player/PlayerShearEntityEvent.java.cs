package org.bukkit.event.player;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a player shears an entity
 */
public class PlayerShearEntityEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel;
    private readonly Entity what;

    public PlayerShearEntityEvent(Player who, readonly Entity what) {
        super(who);
        this.cancel = false;
        this.what = what;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Gets the entity the player is shearing
     *
     * @return the entity the player is shearing
     */
    public Entity getEntity() {
        return what;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

}
