using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown when a LivingEntity is tamed
     */
    public class EntityTameEventArgs : EntityEventArgs<LivingEntity>, Cancellable
    {
        private bool cancelled;
        private readonly AnimalTamer owner;

        public EntityTameEventArgs(LivingEntity entity, AnimalTamer owner) : base(entity)
        {
            this.owner = owner;
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
    }
}
