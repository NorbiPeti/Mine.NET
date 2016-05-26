using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown when a non-player entity (such as an Enderman) tries to teleport
     * from one location to another.
     */
    public class EntityTeleportEventArgs : EntityEventArgs<Entity>, Cancellable
    {
        private bool cancel;
        private Location from;
        private Location to;

        public EntityTeleportEventArgs(Entity what, Location from, Location to) : base(what)
        {
            this.from = from;
            this.to = to;
            this.cancel = false;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Gets the location that this entity moved from
         *
         * @return Location this entity moved from
         */
        public Location getFrom()
        {
            return from;
        }

        /**
         * Sets the location that this entity moved from
         *
         * @param from New location this entity moved from
         */
        public void setFrom(Location from)
        {
            this.from = from;
        }

        /**
         * Gets the location that this entity moved to
         *
         * @return Location the entity moved to
         */
        public Location getTo()
        {
            return to;
        }

        /**
         * Sets the location that this entity moved to
         *
         * @param to New Location this entity moved to
         */
        public void setTo(Location to)
        {
            this.to = to;
        }
    }
}
