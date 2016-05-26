using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.hanging
{
    /**
     * Triggered when a hanging entity is created in the world
     */
    public class HangingPlaceEventArgs : HangingEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly Player player;
        private readonly Block block;
        private readonly BlockFaces BlockFaces;

        public HangingPlaceEventArgs(Hanging hanging, Player player, Block block, BlockFaces BlockFaces) : base(hanging)
        {
            this.player = player;
            this.block = block;
            this.BlockFaces = BlockFaces;
        }

        /**
         * Returns the player placing the hanging entity
         *
         * @return the player placing the hanging entity
         */
        public Player getPlayer()
        {
            return player;
        }

        /**
         * Returns the block that the hanging entity was placed on
         *
         * @return the block that the hanging entity was placed on
         */
        public Block getBlock()
        {
            return block;
        }

        /**
         * Returns the face of the block that the hanging entity was placed on
         *
         * @return the face of the block that the hanging entity was placed on
         */
        public BlockFaces getBlockFace()
        {
            return BlockFaces;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }
    }
}
