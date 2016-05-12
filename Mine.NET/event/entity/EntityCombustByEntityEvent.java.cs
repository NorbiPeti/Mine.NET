using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity causes another entity to combust.
     */
    public class EntityCombustByEntityEvent : EntityCombustEvent
    {
        private readonly Entity combuster;

        public EntityCombustByEntityEvent(Entity combuster, Entity combustee, int duration) :
            base(combustee, duration)
        {
            this.combuster = combuster;
        }

        /**
         * Get the entity that caused the combustion event.
         *
         * @return the Entity that set the combustee alight.
         */
        public Entity getCombuster()
        {
            return combuster;
        }
    }
}
