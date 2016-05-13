using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.entity
{
    /**
     * Called whenever a villager acquires a new trade.
     */
    public class VillagerAcquireTradeEvent : EntityEvent<Villager>, Cancellable {

        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        //
        private MerchantRecipe recipe;

        public VillagerAcquireTradeEvent(Villager what, MerchantRecipe recipe) : base(what)
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

        public override HandlerList getHandlers() {
            return handlers;
        }

        public static HandlerList getHandlerList() {
            return handlers;
        }
    }
}
