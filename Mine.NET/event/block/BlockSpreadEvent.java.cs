using Mine.NET.block;

namespace Mine.NET.Event.block
{
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
    public class BlockSpreadEventArgs : BlockFormEventArgs
    {
        private readonly Block source;

        public BlockSpreadEventArgs(Block block, Block source, BlockState newState) : base(block, newState)
        {
            this.source = source;
        }

        /**
         * Gets the source block involved in this event.
         *
         * @return the Block for the source block involved in this event.
         */
        public Block getSource()
        {
            return source;
        }
    }
}
