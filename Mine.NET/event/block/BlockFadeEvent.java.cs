using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block fades, melts or disappears based on world conditions
     * <p>
     * Examples:
     * <ul>
     * <li>Snow melting due to being near a light source.
     * <li>Ice melting due to being near a light source.
     * <li>Fire burning out after time, without destroying fuel block.
     * </ul>
     * <p>
     * If a Block Fade event is cancelled, the block will not fade, melt or
     * disappear.
     */
    public class BlockFadeEvent : BlockEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private readonly BlockState newState;

        public BlockFadeEvent(Block block, BlockState newState) : base(block)
        {
            this.newState = newState;
            this.cancelled = false;
        }

        /**
         * Gets the state of the block that will be fading, melting or
         * disappearing.
         *
         * @return The block state of the block that will be fading, melting or
         *     disappearing
         */
        public BlockState getNewState()
        {
            return newState;
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
