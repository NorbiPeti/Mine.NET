using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Represents a block related event.
     */
    public abstract class BlockEvent : Event
    {
        protected Block block;

        public BlockEvent(Block theBlock)
        {
            block = theBlock;
        }

        /**
         * Gets the block involved in this event.
         *
         * @return The Block which block is involved in this event
         */
        public Block getBlock()
        {
            return block;
        }
    }
}
