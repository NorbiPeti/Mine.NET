using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is formed or spreads based on world conditions.
     * <p>
     * Use {@link BlockSpreadEvent} to catch blocks that actually spread and don't
     * just "randomly" form.
     * <p>
     * Examples:
     * <ul>
     * <li>Snow forming due to a snow storm.
     * <li>Ice forming in a snowy Biome like Taiga or Tundra.
     * </ul>
     * <p>
     * If a Block Form event is cancelled, the block will not be formed.
     *
     * @see BlockSpreadEvent
     */
    public class BlockFormEventArgs : BlockGrowEventArgs, Cancellable
    {
        public BlockFormEventArgs(Block block, BlockState newState) : base(block, newState)
        {
        }
    }
}
