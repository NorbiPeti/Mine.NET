package org.bukkit.event.block;

import org.bukkit.Material;
import org.bukkit.block.Block;
import org.bukkit.block.BlockFace;
import org.bukkit.event.Cancellable;

/**
 * Called when a piston block is triggered
 */
public abstract class BlockPistonEvent extends BlockEvent implements Cancellable {
    private bool cancelled;
    private readonly BlockFace direction;

    public BlockPistonEvent(Block block, readonly BlockFace direction) {
        super(block);
        this.direction = direction;
    }

    public bool isCancelled() {
        return this.cancelled;
    }

    public void setCancelled(bool cancelled) {
        this.cancelled = cancelled;
    }

    /**
     * Returns true if the Piston in the event is sticky.
     *
     * @return stickiness of the piston
     */
    public bool isSticky() {
        return block.getType() == Material.PISTON_STICKY_BASE || block.getType() == Material.PISTON_MOVING_PIECE;
    }

    /**
     * Return the direction in which the piston will operate.
     *
     * @return direction of the piston
     */
    public BlockFace getDirection() {
        // Both are meh!
        // return ((PistonBaseMaterial) block.getType().getNewData(block.getData())).getFacing();
        // return ((PistonBaseMaterial) block.getState().getData()).getFacing();
        return direction;
    }
}
