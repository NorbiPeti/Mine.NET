using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when a hopper or hopper minecart picks up a dropped item.
     */
    public class InventoryPickupItemEvent : Event, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private readonly Inventory inventory;
        private readonly Item item;

        public InventoryPickupItemEvent(Inventory inventory, Item item) : base()
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
