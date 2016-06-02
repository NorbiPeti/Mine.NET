using Mine.NET.block;
using System.Collections.Generic;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block explodes
     */
    public class BlockExplodeEventArgs : BlockEventArgs, Cancellable
    {
        private bool cancel;
        private readonly List<Block> blocks;
        private float yield;

        public BlockExplodeEventArgs(Block what, List<Block> blocks, float yield) : base(what)
        {
            this.blocks = blocks;
            this.yield = yield;
            this.cancel = false;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Returns the list of blocks that would have been removed or were removed
         * from the explosion event.
         *
         * @return All blown-up blocks
         */
        public List<Block> blockList()
        {
            return blocks;
        }

        /**
         * Returns the percentage of blocks to drop from this explosion
         *
         * @return The yield.
         */
        public float getYield()
        {
            return yield;
        }

        /**
         * Sets the percentage of blocks to drop from this explosion
         *
         * @param yield The new yield percentage
         */
        public void setYield(float yield)
        {
            this.yield = yield;
        }
    }
}
