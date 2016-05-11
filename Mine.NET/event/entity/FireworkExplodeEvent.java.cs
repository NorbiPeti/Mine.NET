package org.bukkit.event.entity;

import org.bukkit.entity.Firework;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a firework explodes.
 */
public class FireworkExplodeEvent : EntityEvent : Cancellable {

    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel;

    public FireworkExplodeEvent(Firework what) {
        base(what);
    }

    @Override
    public bool isCancelled() {
        return cancel;
    }

    /**
     * Set the cancelled state of this event. If the firework explosion is
     * cancelled, the firework will still be removed, but no particles will be
     * displayed.
     *
     * @param cancel whether to cancel or not.
     */
    @Override
    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    @Override
    public Firework getEntity() {
        return (Firework) base.getEntity();
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
