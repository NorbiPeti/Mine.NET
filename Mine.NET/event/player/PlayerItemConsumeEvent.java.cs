using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * This event will fire when a player is finishing consuming an item (food,
     * potion, milk bucket).
     * <br>
     * If the ItemStack is modified the server will use the effects of the new
     * item and not remove the original one from the player's inventory.
     * <br>
     * If the event is cancelled the effect will not be applied and the item will
     * not be removed from the player's inventory.
     */
    public class PlayerItemConsumeEventArgs : PlayerEventArgs, Cancellable
    {
        private bool iscancelled = false;
        private ItemStack item;

        /**
         * @param player the player consuming
         * @param item the ItemStack being consumed
         */
        public PlayerItemConsumeEventArgs(Player player, ItemStack item) : base(player)
        {
            this.item = item;
        }

        /**
         * Gets the item that is being consumed. Modifying the returned item will
         * have no effect, you must use {@link
         * #setItem(org.bukkit.inventory.ItemStack)} instead.
         *
         * @return an ItemStack for the item being consumed
         */
        public ItemStack getItem()
        {
            return item.Clone();
        }

        /**
         * Set the item being consumed
         *
         * @param item the item being consumed
         */
        public void setItem(ItemStack item)
        {
            if (item == null)
            {
                this.item = new ItemStack(Materials.AIR);
            }
            else
            {
                this.item = item;
            }
        }

        public bool isCancelled()
        {
            return this.iscancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.iscancelled = cancel;
        }
    }
}
