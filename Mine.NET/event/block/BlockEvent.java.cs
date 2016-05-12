namespace Mine.NET.event.block;

import org.bukkit.block.Block;
import org.bukkit.event.Event;

/**
 * Represents a block related event.
 */
public abstract class BlockEvent : Event {
    protected Block block;

    public BlockEvent(Block theBlock) {
        block = theBlock;
    }

    /**
     * Gets the block involved in this event.
     *
     * @return The Block which block is involved in this event
     */
    public readonly Block getBlock() {
        return block;
    }
}
