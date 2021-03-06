using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * This event is called when a {@link org.bukkit.entity.Item} is removed from
     * the world because it has existed for 5 minutes.
     * <p>
     * Cancelling the event results in the item being allowed to exist for 5 more
     * minutes. This behavior is not guaranteed and may change in future versions.
     */
    public class ItemDespawnEventArgs : EntityEventArgs<Item>, Cancellable
    {
        private bool canceled;
        private readonly Location location;

        public ItemDespawnEventArgs(Item despawnee, Location loc) : base(despawnee)
        {
            location = loc;
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
         * Gets the location at which the item is despawning.
         *
         * @return The location at which the item is despawning
         */
        public Location getLocation()
        {
            return location;
        }
    }
}
