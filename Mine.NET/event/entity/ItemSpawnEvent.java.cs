using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an item is spawned into a world
     */
    public class ItemSpawnEventArgs : EntityEventArgs<Item>, Cancellable
    {
        private readonly Location location;
        private bool canceled;

        public ItemSpawnEventArgs(Item spawnee, Location loc) : base(spawnee)
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
    }
}
