using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * Fired when a player's item breaks (such as a shovel or flint and steel).
     * <p>
     * The item that's breaking will exist in the inventory with a stack size of
     * 0. After the event, the item's durability will be reset to 0.
     */
    public class PlayerItemBreakEventArgs : PlayerEventArgs
    {
        private readonly ItemStack brokenItem;

        public PlayerItemBreakEventArgs(Player player, ItemStack brokenItem) : base(player)
        {
            this.brokenItem = brokenItem;
        }

        /**
         * Gets the item that broke
         *
         * @return The broken item
         */
        public ItemStack getBrokenItem()
        {
            return brokenItem;
        }
    }
}
