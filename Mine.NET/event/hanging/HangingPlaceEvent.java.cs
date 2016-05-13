using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.hanging
{
    /**
     * Triggered when a hanging entity is created in the world
     */
    public class HangingPlaceEvent : HangingEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private readonly Player player;
        private readonly Block block;
        private readonly BlockFace blockFace;

        public HangingPlaceEvent(Hanging hanging, Player player, Block block, BlockFace blockFace) : base(hanging)
        {
            this.player = player;
            this.block = block;
            this.blockFace = blockFace;
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
        public BlockFace getBlockFace()
        {
            return blockFace;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
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
