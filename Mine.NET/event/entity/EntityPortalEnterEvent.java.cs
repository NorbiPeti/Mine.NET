using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity comes into contact with a portal
     */
    public class EntityPortalEnterEvent : EntityEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Location location;

        public EntityPortalEnterEvent(Entity entity, Location location) : base(entity)
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
