package org.bukkit.event.entity;

import org.bukkit.entity.Sheep;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a sheep regrows its wool
 */
public class SheepRegrowWoolEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel;

    public SheepRegrowWoolEvent(Sheep sheep) {
        base(sheep);
        this.cancel = false;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public override Sheep getEntity() {
        return (Sheep) entity;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

}
