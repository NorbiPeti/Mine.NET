using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a piston block is triggered
     */
    public abstract class BlockPistonEventArgs : BlockEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly BlockFaces direction;

        public BlockPistonEventArgs(Block block, BlockFaces direction) : base(block)
        {
            this.direction = direction;
        }

        public bool isCancelled()
        {
            return this.cancelled;
        }

        public void setCancelled(bool cancelled)
        {
            this.cancelled = cancelled;
        }

        /**
         * Returns true if the Piston in the event is sticky.
         *
         * @return stickiness of the piston
         */
        public bool isSticky()
        {
            return block.getType() == Materials.PISTON_STICKY_BASE || block.getType() == Materials.PISTON_MOVING_PIECE;
        }

        /**
         * Return the direction in which the piston will operate.
         *
         * @return direction of the piston
         */
        public BlockFaces getDirection()
        {
            // Both are meh!
            // return ((PistonBaseMaterial) block.getType().getNewData(block.getData())).getFacing();
            // return ((PistonBaseMaterial) block.getState().getData()).getFacing();
            return direction;
        }
    }
}
