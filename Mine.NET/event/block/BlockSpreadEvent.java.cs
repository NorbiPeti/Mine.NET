namespace Mine.NET.event.block;

import org.bukkit.block.Block;
import org.bukkit.block.BlockState;
import org.bukkit.event.HandlerList;

/**
 * Called when a block spreads based on world conditions.
 * <p>
 * Use {@link BlockFormEvent} to catch blocks that "randomly" form instead of
 * actually spread.
 * <p>
 * Examples:
 * <ul>
 * <li>Mushrooms spreading.
 * <li>Fire spreading.
 * </ul>
 * <p>
 * If a Block Spread event is cancelled, the block will not spread.
 *
 * @see BlockFormEvent
 */
public class BlockSpreadEvent : BlockFormEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Block source;

    public BlockSpreadEvent(Block block, readonly Block source, readonly BlockState newState) {
        base(block, newState);
        this.source = source;
    }

    /**
     * Gets the source block involved in this event.
     *
     * @return the Block for the source block involved in this event.
     */
    public Block getSource() {
        return source;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
