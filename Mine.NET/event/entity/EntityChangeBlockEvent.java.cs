package org.bukkit.event.entity;

import org.bukkit.Material;
import org.bukkit.block.Block;
import org.bukkit.entity.Entity;
import org.bukkit.entity.LivingEntity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when any Entity, excluding players, changes a block.
 */
public class EntityChangeBlockEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Block block;
    private bool cancel;
    private readonly Material to;
    private readonly byte data;

    /**
     *
     * @param what the LivingEntity causing the change
     * @param block the block (before the change)
     * @param to the future material being changed to
     * [Obsolete] Provided as a backward compatibility before the data byte
     *     was provided, and type increased to all entities
     */
    [Obsolete]
    public EntityChangeBlockEvent(LivingEntity what, readonly Block block, readonly Material to) {
        this (what, block, to, (byte) 0);
    }

    /**
     *
     * @param what the Entity causing the change
     * @param block the block (before the change)
     * @param to the future material being changed to
     * @param data the future block data
     * [Obsolete] Magic value
     */
    [Obsolete]
    public EntityChangeBlockEvent(Entity what, readonly Block block, readonly Material to, readonly byte data) {
        base(what);
        this.block = block;
        this.cancel = false;
        this.to = to;
        this.data = data;
    }

    /**
     * Gets the block the entity is changing
     *
     * @return the block that is changing
     */
    public Block getBlock() {
        return block;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Gets the Material that the block is changing into
     *
     * @return the material that the block is changing into
     */
    public Material getTo() {
        return to;
    }

    /**
     * Gets the data for the block that would be changed into
     *
     * @return the data for the block that would be changed into
     * [Obsolete] Magic value
     */
    [Obsolete]
    public byte getData() {
        return data;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
