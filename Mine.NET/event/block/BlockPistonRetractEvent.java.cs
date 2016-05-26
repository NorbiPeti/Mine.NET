using Mine.NET.block;
using System.Collections.Generic;

namespace Mine.NET.Event.block
{
    /**
     * Called when a piston retracts
     */
    public class BlockPistonRetractEventArgs : BlockPistonEventArgs
    {
        private List<Block> blocks;

        public BlockPistonRetractEventArgs(Block block, List<Block> blocks, BlockFaces direction) : base(block, direction)
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
    }
}
