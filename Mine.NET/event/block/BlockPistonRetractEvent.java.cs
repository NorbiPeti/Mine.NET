using Mine.NET.block;
using System.Collections.Generic;

namespace Mine.NET.Event.block
{
    /**
     * Called when a piston retracts
     */
    public class BlockPistonRetractEvent : BlockPistonEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private List<Block> blocks;

        public BlockPistonRetractEvent(Block block, List<Block> blocks, BlockFace direction) : base(block, direction)
        {
            this.blocks = blocks;
        }

        /**
         * Get an immutable list of the blocks which will be moved by the
         * extending.
         *
         * @return Immutable list of the moved blocks.
         */
        public List<Block> getBlocks()
        {
            return blocks;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
