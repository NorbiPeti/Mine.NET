using Mine.NET.entity;
using Mine.NET.inventory;
using System.Collections.Generic;

namespace Mine.NET.Event.inventory
{
    /**
     * Represents a player related inventory event
     */
    public class InventoryEvent<T> : Event where T : Inventory
    {
        private static readonly HandlerList handlers = new HandlerList();
        protected InventoryView transaction;

        public InventoryEvent(InventoryView transaction)
        {
            this.transaction = transaction;
        }

        /**
         * Gets the primary Inventory involved in this transaction
         *
         * @return The upper inventory.
         */
        public virtual T getInventory()
        {
            return (T)transaction.getTopInventory();
        }

        /**
         * Gets the list of players viewing the primary (upper) inventory involved
         * in this event
         *
         * @return A list of people viewing.
         */
        public List<HumanEntity> getViewers()
        {
            return transaction.getTopInventory().getViewers();
        }

        /**
         * Gets the view object itself
         *
         * @return InventoryView
         */
        public InventoryView getView()
        {
            return transaction;
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

    public class InventoryEvent : InventoryEvent<Inventory>
    {
        public InventoryEvent(InventoryView transaction) : base(transaction)
        {
        }
    }
}
