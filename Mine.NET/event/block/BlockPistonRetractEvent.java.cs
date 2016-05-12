package org.bukkit.event.block;

import java.util.List;
import org.bukkit.Location;
import org.bukkit.block.Block;
import org.bukkit.block.BlockFace;
import org.bukkit.event.HandlerList;

/**
 * Called when a piston retracts
 */
public class BlockPistonRetractEvent : BlockPistonEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private List<Block> blocks;
    
    public BlockPistonRetractEvent(Block block, readonly List<Block> blocks, readonly BlockFace direction) {
        base(block, direction);
        
        this.blocks = blocks;
    }

    /**
     * Gets the location where the possible moving block might be if the
     * retracting piston is sticky.
     *
     * @return The possible location of the possibly moving block.
     */
    [Obsolete]
    public Location getRetractLocation() {
        return getBlock().getRelative(getDirection(), 2).getLocation();
    }
    
    /**
     * Get an immutable list of the blocks which will be moved by the
     * extending.
     *
     * @return Immutable list of the moved blocks.
     */
    public List<Block> getBlocks() {
        return blocks;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
