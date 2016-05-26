using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when a hopper or hopper minecart picks up a dropped item.
     */
    public class InventoryPickupItemEventArgs : GameEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly Inventory inventory;
        private readonly Item item;

        public InventoryPickupItemEventArgs(Inventory inventory, Item item) : base()
        {
            this.inventory = inventory;
            this.item = item;
        }

        /**
         * Gets the Inventory that picked up the item
         *
         * @return Inventory
         */
        public Inventory getInventory()
        {
            return inventory;
        }

        /**
         * Gets the Item entity that was picked up
         *
         * @return Item
         */
        public Item getItem()
        {
            return item;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }
    }
}
