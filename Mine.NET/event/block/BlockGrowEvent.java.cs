package org.bukkit.event.block;

import org.bukkit.block.Block;
import org.bukkit.block.BlockState;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a block grows naturally in the world.
 * <p>
 * Examples:
 * <ul>
 * <li>Wheat
 * <li>Sugar Cane
 * <li>Cactus
 * <li>Watermelon
 * <li>Pumpkin
 * </ul>
 * <p>
 * If a Block Grow event is cancelled, the block will not grow.
 */
public class BlockGrowEvent : BlockEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly BlockState newState;
    private bool cancelled = false;

    public BlockGrowEvent(Block block, readonly BlockState newState) {
        super(block);
        this.newState = newState;
    }

    /**
     * Gets the state of the block where it will form or spread to.
     *
     * @return The block state for this events block
     */
    public BlockState getNewState() {
        return newState;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
