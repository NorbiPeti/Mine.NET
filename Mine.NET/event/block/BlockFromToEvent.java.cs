using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Represents events with a source block and a destination block, currently
     * only applies to liquid (lava and water) and teleporting dragon eggs.
     * <p>
     * If a Block From To event is cancelled, the block will not move (the liquid
     * will not flow).
     */
    public class BlockFromToEvent : BlockEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        protected Block to;
        protected BlockFaces face;
        protected bool cancel;

        public BlockFromToEvent(Block block, BlockFaces face) : base(block)
        {
            this.face = face;
            this.cancel = false;
        }

        public BlockFromToEvent(Block block, Block toBlock) : base(block)
        {
            this.to = toBlock;
            this.face = BlockFaces.SELF;
            this.cancel = false;
        }

        /**
         * Gets the BlockFaces that the block is moving to.
         *
         * @return The BlockFaces that the block is moving to
         */
        public BlockFaces getFace()
        {
            return face;
        }

        /**
         * Convenience method for getting the faced Block.
         *
         * @return The faced Block
         */
        public Block getToBlock()
        {
            if (to == null)
            {
                to = block.getRelative(face);
            }
            return to;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
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
