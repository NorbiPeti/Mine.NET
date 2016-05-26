using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.entity
{
    /**
     * Called whenever a villager acquires a new trade.
     */
    public class VillagerAcquireTradeEventArgs : EntityEventArgs<Villager>, Cancellable {
        private bool cancelled;
        //
        private MerchantRecipe recipe;

        public VillagerAcquireTradeEventArgs(Villager what, MerchantRecipe recipe) : base(what)
        {
            this.recipe = recipe;
        }

        /**
         * Get the recipe to be acquired.
         *
         * @return the new recipe
         */
        public MerchantRecipe getRecipe() {
            return recipe;
        }

        /**
         * Set the recipe to be acquired.
         *
         * @param recipe the new recipe
         */
        public void setRecipe(MerchantRecipe recipe) {
            this.recipe = recipe;
        }

        public bool isCancelled() {
            return cancelled;
        }

        public void setCancelled(bool cancel) {
            this.cancelled = cancel;
        }
    }
}
