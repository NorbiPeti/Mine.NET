using Mine.NET.block;
using Mine.NET.enchantments;
using Mine.NET.entity;
using Mine.NET.Event.inventory;
using Mine.NET.inventory;
using System.Collections.Generic;

namespace Mine.NET.Event.enchantment
{
    /**
     * Called when an ItemStack is successfully enchanted (currently at
     * enchantment table)
     */
    public class EnchantItemEventArgs : InventoryEventArgs, Cancellable
    {
        private readonly Block table;
        private readonly ItemStack item;
        private int level;
        private bool cancelled;
        private readonly Dictionary<Enchantment, int> enchants;
        private readonly Player enchanter;
        private int button;

        public EnchantItemEventArgs(Player enchanter, InventoryView view, Block table, ItemStack item, int level, Dictionary<Enchantment, int> enchants, int i) : base(view)
        {
            this.enchanter = enchanter;
            this.table = table;
            this.item = item;
            this.level = level;
            this.enchants = new Dictionary<Enchantment, int>(enchants);
            this.cancelled = false;
            this.button = i;
        }

        /**
         * Gets the player enchanting the item
         *
         * @return enchanting player
         */
        public Player getEnchanter()
        {
            return enchanter;
        }

        /**
         * Gets the block being used to enchant the item
         *
         * @return the block used for enchanting
         */
        public Block getEnchantBlock()
        {
            return table;
        }

        /**
         * Gets the item to be enchanted (can be modified)
         *
         * @return ItemStack of item
         */
        public ItemStack getItem()
        {
            return item;
        }

        /**
         * Get cost in exp levels of the enchantment
         *
         * @return experience level cost
         */
        public int getExpLevelCost()
        {
            return level;
        }

        /**
         * Set cost in exp levels of the enchantment
         *
         * @param level - cost in levels
         */
        public void setExpLevelCost(int level)
        {
            this.level = level;
        }

        /**
         * Get map of enchantment (levels, keyed by type) to be added to item
         * (modify map returned to change values). Note: Any enchantments not
         * allowed for the item will be ignored
         *
         * @return map of enchantment levels, keyed by enchantment
         */
        public Dictionary<Enchantment, int> getEnchantsToAdd()
        {
            return enchants;
        }

        /**
         * Which button was pressed to initiate the enchanting.
         *
         * @return The button index (0, 1, or 2).
         */
        public int whichButton()
        {
            return button;
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
