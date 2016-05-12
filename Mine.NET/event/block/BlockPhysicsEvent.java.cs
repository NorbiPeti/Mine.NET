using Mine.NET.block;

namespace Mine.NET.Event.block
{

    /**
     * Thrown when a block physics check is called
     */
    public class BlockPhysicsEvent : BlockEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Material changed;
        private bool cancel = false;

        public BlockPhysicsEvent(Block block, Material changed) : base(block)
        {
            this.changed = changed;
        }

        /**
         * Gets the type of block that changed, causing this event
         *
         * @return Changed block's type
         */
        public Material getChangedType()
        {
            return changed;
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
