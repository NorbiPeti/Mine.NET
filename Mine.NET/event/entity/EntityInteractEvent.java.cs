using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity interacts with an object
     */
    public class EntityInteractEventArgs : EntityEventArgs<Entity>, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        protected Block block;
        private bool cancelled;

        public EntityInteractEventArgs(Entity entity, Block block) : base(entity)
        {
            this.block = block;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            cancelled = cancel;
        }

        /**
         * Returns the involved block
         *
         * @return the block clicked with this item.
         */
        public Block getBlock()
        {
            return block;
        }
    }
}
