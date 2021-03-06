using Mine.NET.entity;
using Mine.NET.inventory;
using System.Collections.Generic;

namespace Mine.NET.entity
{
    /**
     * Represents a villager NPC
     */
    public interface Villager : Ageable, NPC, InventoryHolder
    {

        /**
         * Gets the current profession of this villager.
         *
         * @return Current profession.
         */
        VillagerProfession getProfession();

        /**
         * Sets the new profession of this villager.
         *
         * @param profession New profession.
         */
        void setProfession(VillagerProfession profession);

        /**
         * Get a list of trades currently available from this villager.
         *
         * @return an immutable list of trades
         */
        List<MerchantRecipe> getRecipes();

        /**
         * Set the list of trades currently available from this villager.
         * <br>
         * This will not change the selected trades of players currently trading
         * with this villager.
         *
         * @param recipes a list of recipes
         */
        void setRecipes(List<MerchantRecipe> recipes);

        /**
         * Get the recipe at a certain index of this villager's trade list.
         *
         * @param i the index
         * @return the recipe
         * @throws IndexOutOfBoundsException
         */
        MerchantRecipe getRecipe(int i);

        /**
         * Set the recipe at a certain index of this villager's trade list.
         *
         * @param i the index
         * @param recipe the recipe
         * @throws IndexOutOfBoundsException
         */
        void setRecipe(int i, MerchantRecipe recipe);

        /**
         * Get the number of trades this villager currently has available.
         *
         * @return the recipe count
         */
        int getRecipeCount();

        /**
         * Gets whether this villager is currently trading.
         *
         * @return whether the villager is trading
         */
        bool isTrading();

        /**
         * Gets the player this villager is trading with, or null if it is not
         * currently trading.
         *
         * @return the trader, or null
         */
        HumanEntity getTrader();

        /**
         * Gets this villager's riches, the number of emeralds this villager has
         * been given.
         *
         * @return the villager's riches
         */
        int getRiches();

        /**
         * Sets this villager's riches.
         *
         * @see Villager#getRiches()
         *
         * @param riches the new riches
         */
        void setRiches(int riches);
    }

    /**
     * Represents the various different Villager professions there may be.
     */
    public enum VillagerProfession
    {
        FARMER = 0,
        LIBRARIAN = 1,
        PRIEST = 2,
        BLACKSMITH = 3,
        BUTCHER = 4
    }
}
