using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when an item is put in a slot for repair by an anvil.
     */
    public class PrepareAnvilEventArgs : InventoryEventArgs<AnvilInventory>
    {
        private ItemStack result;

        public PrepareAnvilEventArgs(InventoryView inventory, ItemStack result) : base(inventory)
        {
            this.result = result;
        }

        /**
         * Get result item, may be null.
         *
         * @return result item
         */
        public ItemStack getResult()
        {
            return result;
        }

        public void setResult(ItemStack result)
        {
            this.result = result;
        }
    }
}
