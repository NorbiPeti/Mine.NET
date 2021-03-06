using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block grows naturally in the world.
     * <p>
     * Examples:
     * <ul>
     * <li>Wheat
     * <li>Sugar Cane
     * <li>Cactus
     * <li>Watermelon
     * <li>Pumpkin
     * </ul>
     * <p>
     * If a Block Grow event is cancelled, the block will not grow.
     */
    public class BlockGrowEventArgs : BlockEventArgs, Cancellable {
        private readonly BlockState newState;
        private bool cancelled = false;

        public BlockGrowEventArgs(Block block, BlockState newState) : base(block)
        {
            this.newState = newState;
        }

        /**
         * Gets the state of the block where it will form or spread to.
         *
         * @return The block state for this events block
         */
        public BlockState getNewState() {
            return newState;
        }

        public bool isCancelled() {
            return cancelled;
        }

        public void setCancelled(bool cancel) {
            this.cancelled = cancel;
        }
    }
}
