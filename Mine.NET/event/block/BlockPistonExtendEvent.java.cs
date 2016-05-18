using Mine.NET.block;
using System.Collections.Generic;

namespace Mine.NET.Event.block
{
    /**
     * Called when a piston :
     */
    public class BlockPistonExtendEvent : BlockPistonEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly int length;
        private List<Block> blocks;

        public BlockPistonExtendEvent(Block block, List<Block> blocks, BlockFaces direction) : base(block, direction)
        {
            this.length = blocks.Count;
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
            if (blocks == null)
            {
                List<Block> tmp = new List<Block>();
                for (int i = 0; i < blocks.Count; i++)
                {
                    tmp.Add(block.getRelative(getDirection(), i + 1));
                }
                blocks = tmp;
            }
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
