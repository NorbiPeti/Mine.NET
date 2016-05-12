using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown when a LivingEntity is tamed
     */
    public class EntityTameEvent : EntityEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private readonly AnimalTamer owner;

        public EntityTameEvent(LivingEntity entity, AnimalTamer owner) : base(entity)
        {
            this.owner = owner;
        }

        public override Entity getEntity()
        {
            return entity;
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
         * Gets the owning AnimalTamer
         *
         * @return the owning AnimalTamer
         */
        public AnimalTamer getOwner()
        {
            return owner;
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
