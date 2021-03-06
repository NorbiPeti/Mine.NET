using Mine.NET.inventory;
using System;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when some entity or block (e.g. hopper) tries to move items directly
     * from one inventory to another.
     * <p>
     * When this event is called, the initiator may already have removed the item
     * from the source inventory and is ready to move it into the destination
     * inventory.
     * <p>
     * If this event is cancelled, the items will be returned to the source
     * inventory, if needed.
     * <p>
     * If this event is not cancelled, the initiator will try to put the ItemStack
     * into the destination inventory. If this is not possible and the ItemStack
     * has not been modified, the source inventory slot will be restored to its
     * former state. Otherwise any additional items will be discarded.
     */
    public class InventoryMoveItemEventArgs : GameEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly Inventory sourceInventory;
        private readonly Inventory destinationInventory;
        private ItemStack itemStack;
        private readonly bool didSourceInitiate;

        public InventoryMoveItemEventArgs(Inventory sourceInventory, ItemStack itemStack, Inventory destinationInventory, bool didSourceInitiate)
        {
            if (itemStack == null) throw new ArgumentNullException("ItemStack cannot be null");
            this.sourceInventory = sourceInventory;
            this.itemStack = itemStack;
            this.destinationInventory = destinationInventory;
            this.didSourceInitiate = didSourceInitiate;
        }

        /**
         * Gets the Inventory that the ItemStack is being taken from
         *
         * @return Inventory that the ItemStack is being taken from
         */
        public Inventory getSource()
        {
            return sourceInventory;
        }

        /**
         * Gets the ItemStack being moved; if modified, the original item will not
         * be removed from the source inventory.
         *
         * @return ItemStack
         */
        public ItemStack getItem()
        {
            return itemStack.Clone();
        }

        /**
         * Sets the ItemStack being moved; if this is different from the original
         * ItemStack, the original item will not be removed from the source
         * inventory.
         *
         * @param itemStack The ItemStack
         */
        public void setItem(ItemStack itemStack)
        {
            if (itemStack == null) throw new ArgumentNullException("ItemStack cannot be null.  Cancel the event if you want nothing to be transferred.");
            this.itemStack = itemStack.Clone();
        }

        /**
         * Gets the Inventory that the ItemStack is being put into
         *
         * @return Inventory that the ItemStack is being put into
         */
        public Inventory getDestination()
        {
            return destinationInventory;
        }

        /**
         * Gets the Inventory that initiated the transfer. This will always be
         * either the destination or source Inventory.
         *
         * @return Inventory that initiated the transfer
         */
        public Inventory getInitiator()
        {
            return didSourceInitiate ? sourceInventory : destinationInventory;
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
