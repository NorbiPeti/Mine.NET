package org.bukkit.event.entity;

import org.bukkit.Location;
import org.bukkit.block.Block;
import org.bukkit.entity.Entity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

import java.util.List;

/**
 * Called when an entity explodes
 */
public class EntityExplodeEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel;
    private readonly Location location;
    private readonly List<Block> blocks;
    private float yield;

    public EntityExplodeEvent(Entity what, readonly Location location, readonly List<Block> blocks, readonly float yield) {
        super(what);
        this.location = location;
        this.blocks = blocks;
        this.yield = yield;
        this.cancel = false;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Returns the list of blocks that would have been removed or were removed
     * from the explosion event.
     *
     * @return All blown-up blocks
     */
    public List<Block> blockList() {
        return blocks;
    }

    /**
     * Returns the location where the explosion happened.
     * <p>
     * It is not possible to get this value from the Entity as the Entity no
     * longer exists in the world.
     *
     * @return The location of the explosion
     */
    public Location getLocation() {
        return location;
    }

    /**
     * Returns the percentage of blocks to drop from this explosion
     *
     * @return The yield.
     */
    public float getYield() {
        return yield;
    }

    /**
     * Sets the percentage of blocks to drop from this explosion
     *
     * @param yield The new yield percentage
     */
    public void setYield(float yield) {
        this.yield = yield;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
