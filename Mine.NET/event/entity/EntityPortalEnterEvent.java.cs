using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity comes into contact with a portal
     */
    public class EntityPortalEnterEventArgs : EntityEventArgs<Entity>
    {
        private readonly Location location;

        public EntityPortalEnterEventArgs(Entity entity, Location location) : base(entity)
        {
            this.location = location;
        }

        /**
         * Gets the portal block the entity is touching
         *
         * @return The portal block the entity is touching
         */
        public Location getLocation()
        {
            return location;
        }
    }
}
