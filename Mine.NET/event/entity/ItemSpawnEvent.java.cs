using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an item is spawned into a world
     */
    public class ItemSpawnEvent : EntityEvent<Item>, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Location location;
        private bool canceled;

        public ItemSpawnEvent(Item spawnee, Location loc) : base(spawnee)
        {
            this.location = loc;
        }

        public bool isCancelled()
        {
            return canceled;
        }

        public void setCancelled(bool cancel)
        {
            canceled = cancel;
        }

        /**
         * Gets the location at which the item is spawning.
         *
         * @return The location at which the item is spawning
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
