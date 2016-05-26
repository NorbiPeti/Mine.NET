using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Represents a player related inventory event
     */
    public class InventoryCloseEventArgs : InventoryEventArgs
    {
        public InventoryCloseEventArgs(InventoryView transaction) : base(transaction)
        {
        }

        /**
         * Returns the player involved in this event
         *
         * @return Player who is involved in this event
         */
        public HumanEntity getPlayer()
        {
            return transaction.getPlayer();
        }
    }
}
