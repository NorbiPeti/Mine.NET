using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Represents an Entity-related event
     */
    public abstract class EntityEvent : Event
    {
        protected Entity entity;

        public EntityEvent(Entity what)
        {
            entity = what;
        }

        /**
         * Returns the Entity involved in this event
         *
         * @return Entity who is involved in this event
         */
        public virtual Entity getEntity()
        {
            return entity;
        }

        /**
         * Gets the EntityType of the Entity involved in this event.
         *
         * @return EntityType of the Entity involved in this event
         */
        public EntityType getEntityType()
        {
            return entity.getType();
        }
    }
}
