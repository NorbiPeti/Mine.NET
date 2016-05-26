using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Represents an Entity-related event
     */
    public abstract class EntityEventArgs<T> : GameEventArgs where T : Entity
    {
        protected Entity entity;

        public EntityEventArgs(Entity what)
        {
            entity = what;
        }

        /**
         * Returns the Entity involved in this event
         *
         * @return Entity who is involved in this event
         */
        public T getEntity()
        {
            return (T)entity;
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
