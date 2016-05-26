using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a villager's trade's maximum uses is increased, due to a player's
     * trade.
     *
     * @see MerchantRecipe#getMaxUses()
     */
    public class VillagerReplenishTradeEventArgs : EntityEventArgs<Villager>, Cancellable
    {
        private bool cancelled;
        //
        private MerchantRecipe recipe;
        private int bonus;

        public VillagerReplenishTradeEventArgs(Villager what, MerchantRecipe recipe, int bonus) : base(what)
        {
            this.recipe = recipe;
            this.bonus = bonus;
        }

        /**
         * Get the recipe to replenish.
         *
         * @return the replenished recipe
         */
        public MerchantRecipe getRecipe()
        {
            return recipe;
        }

        /**
         * Set the recipe to replenish.
         *
         * @param recipe the replenished recipe
         */
        public void setRecipe(MerchantRecipe recipe)
        {
            this.recipe = recipe;
        }

        /**
         * Get the bonus uses added. The maximum uses of the recipe will be
         * increased by this number.
         *
         * @return the extra uses added
         */
        public int getBonus()
        {
            return bonus;
        }

        /**
         * Set the bonus uses added.
         *
         * @see VillagerReplenishTradeEvent#getBonus()
         * @param bonus the extra uses added
         */
        public void setBonus(int bonus)
        {
            this.bonus = bonus;
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
