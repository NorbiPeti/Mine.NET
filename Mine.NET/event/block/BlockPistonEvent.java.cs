using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a piston block is triggered
     */
    public abstract class BlockPistonEvent : BlockEvent, Cancellable
    {
        private bool cancelled;
        private readonly BlockFace direction;

        public BlockPistonEvent(Block block, BlockFace direction) : base(block)
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
            return block.getType() == Material.AllMaterials[Materials.PISTON_STICKY_BASE] || block.getType() == Material.AllMaterials[Materials.PISTON_MOVING_PIECE];
        }

        /**
         * Return the direction in which the piston will operate.
         *
         * @return direction of the piston
         */
        public BlockFace getDirection()
        {
            // Both are meh!
            // return ((PistonBaseMaterial) block.getType().getNewData(block.getData())).getFacing();
            // return ((PistonBaseMaterial) block.getState().getData()).getFacing();
            return direction;
        }
    }
}
