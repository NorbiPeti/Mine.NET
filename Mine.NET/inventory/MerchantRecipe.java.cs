using System;
using System.Collections.Generic;

namespace Mine.NET.inventory
{
    /**
     * Represents a Villager's trade.
     *
     * Trades can take one or two ingredients, and provide one result. The
     * ingredients' Itemstack amounts are respected in the trade.
     * <br>
     * A trade has a limited number of uses, after which the trade can no longer be
     * used, unless the player uses a different trade, which will cause its maximum
     * uses to increase.
     * <br>
     * A trade may or may not reward experience for being completed.
     *
     * @see org.bukkit.event.entity.VillagerReplenishTradeEvent
     */
    public class MerchantRecipe : Recipe
    {

        private ItemStack result;
        private List<ItemStack> ingredients = new List<ItemStack>();
        private int uses;
        private int maxUses;
        private bool experienceReward;

        public MerchantRecipe(ItemStack result, int maxUses):
            this(result, 0, maxUses, false)
        {
        }

        public MerchantRecipe(ItemStack result, int uses, int maxUses, bool experienceReward)
        {
            this.result = result;
            this.uses = uses;
            this.maxUses = maxUses;
            this.experienceReward = experienceReward;
        }

        public ItemStack getResult()
        {
            return result;
        }

        public void addIngredient(ItemStack item)
        {
            if(ingredients.Count > 2) throw new ArgumentException("Merchant can only have 2 ingredients");
            ingredients.Add((ItemStack)item.Clone());
        }

        public void removeIngredient(int index)
        {
            ingredients.RemoveAt(index);
        }

        public void setIngredients(List<ItemStack> ingredients)
        {
            this.ingredients = new List<ItemStack>();
            foreach (ItemStack item in ingredients)
            {
                this.ingredients.Add((ItemStack)item.Clone());
            }
        }

        public List<ItemStack> getIngredients()
        {
            List<ItemStack> copy = new List<ItemStack>();
            foreach (ItemStack item in ingredients)
            {
                copy.Add((ItemStack)item.Clone());
            }
            return copy;
        }

        /**
         * Get the number of times this trade has been used.
         *
         * @return the number of uses
         */
        public int getUses()
        {
            return uses;
        }

        /**
         * Set the number of times this trade has been used.
         *
         * @param uses the number of uses
         */
        public void setUses(int uses)
        {
            this.uses = uses;
        }

        /**
         * Get the maximum number of uses this trade has.
         * <br>
         * The maximum uses of this trade may increase when a player trades with the
         * owning villager.
         *
         * @return the maximum number of uses
         */
        public int getMaxUses()
        {
            return maxUses;
        }

        /**
         * Set the maximum number of uses this trade has.
         *
         * @param maxUses the maximum number of time this trade can be used
         */
        public void setMaxUses(int maxUses)
        {
            this.maxUses = maxUses;
        }

        /**
         * Whether to reward experience for the trade.
         *
         * @return whether to reward experience for completing this trade
         */
        public bool hasExperienceReward()
        {
            return experienceReward;
        }

        /**
         * Set whether to reward experience for the trade.
         *
         * @param flag whether to reward experience for completing this trade
         */
        public void setExperienceReward(bool flag)
        {
            this.experienceReward = flag;
        }
    }
}
