namespace Mine.NET.inventory
{
    /**
     * Represents a trading inventory between a player and a villager.
     * <br>
     * The holder of this Inventory is the owning Villager.
     */
    public interface MerchantInventory : Inventory
    {

        /**
         * Get the index of the currently selected recipe.
         *
         * @return the index of the currently selected recipe
         */
        int getSelectedRecipeIndex();

        /**
         * Get the currently selected recipe.
         *
         * @return the currently selected recipe
         */
        MerchantRecipe getSelectedRecipe();
    }
}
