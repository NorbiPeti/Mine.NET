using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when the recipe of an Item is completed inside a crafting matrix.
     */
    public class CraftItemEventArgs : InventoryClickEventArgs<CraftingInventory>
    {
        private Recipe recipe;

        public CraftItemEventArgs(Recipe recipe, InventoryView what, SlotType type, int slot, ClickTypes click, InventoryAction action) :
            base(what, type, slot, click, action) {
            this.recipe = recipe;
        }

        public CraftItemEventArgs(Recipe recipe, InventoryView what, SlotType type, int slot, ClickTypes click, InventoryAction action, int key):
            base(what, type, slot, click, action, key)
        {
            this.recipe = recipe;
        }

        /**
         * @return A copy of the current recipe on the crafting matrix.
         */
        public Recipe getRecipe()
        {
            return recipe;
        }
    }
}
