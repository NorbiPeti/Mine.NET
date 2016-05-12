package org.bukkit.event.entity;

import org.bukkit.block.Block;
import org.bukkit.entity.Entity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when an entity interacts with an object
 */
public class EntityInteractEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    protected Block block;
    private bool cancelled;

    public EntityInteractEvent(Entity entity, readonly Block block) {
        base(entity);
        this.block = block;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        cancelled = cancel;
    }

    /**
     * Returns the involved block
     *
     * @return the block clicked with this item.
     */
    public Block getBlock() {
        return block;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
