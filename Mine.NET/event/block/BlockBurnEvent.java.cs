package org.bukkit.event.block;

import org.bukkit.block.Block;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a block is destroyed as a result of being burnt by fire.
 * <p>
 * If a Block Burn event is cancelled, the block will not be destroyed as a
 * result of being burnt by fire.
 */
public class BlockBurnEvent extends BlockEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;

    public BlockBurnEvent(Block block) {
        super(block);
        this.cancelled = false;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
