package org.bukkit.event.entity;

import org.bukkit.entity.Item;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

public class ItemMergeEvent : EntityEvent : Cancellable {

    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private readonly Item target;

    public ItemMergeEvent(Item item, Item target) {
        super(item);
        this.target = target;
    }

    @Override
    public bool isCancelled() {
        return cancelled;
    }

    @Override
    public void setCancelled(bool cancelled) {
        this.cancelled = cancelled;
    }

    @Override
    public Item getEntity() {
        return (Item) entity;
    }

    /**
     * Gets the Item entity the main Item is being merged into.
     *
     * @return The Item being merged with
     */
    public Item getTarget() {
        return target;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
