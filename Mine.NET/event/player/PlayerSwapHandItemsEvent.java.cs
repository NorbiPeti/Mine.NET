using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player swap items between main hand and off hand using the
     * hotkey.
     */
    public class PlayerSwapHandItemsEventArgs : PlayerEventArgs, Cancellable
    {
        //
        private ItemStack mainHandItem;
        private ItemStack offHandItem;
        private bool cancelled;

        public PlayerSwapHandItemsEventArgs(NET.entity.Player player, ItemStack mainHandItem, ItemStack offHandItem) : base(player)
        {
            this.mainHandItem = mainHandItem;
            this.offHandItem = offHandItem;
        }

        /**
         * Gets the item switched to the main hand.
         *
         * @return item in the main hand
         */
        public ItemStack getMainHandItem()
        {
            return mainHandItem;
        }

        /**
         * Sets the item in the main hand.
         *
         * @param mainHandItem new item in the main hand
         */
        public void setMainHandItem(ItemStack mainHandItem)
        {
            this.mainHandItem = mainHandItem;
        }

        /**
         * Gets the item switched to the off hand.
         *
         * @return item in the off hand
         */
        public ItemStack getOffHandItem()
        {
            return offHandItem;
        }

        /**
         * Sets the item in the off hand.
         *
         * @param offHandItem new item in the off hand
         */
        public void setOffHandItem(ItemStack offHandItem)
        {
            this.offHandItem = offHandItem;
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
