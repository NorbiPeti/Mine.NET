using Mine.NET.block;
using Mine.NET.Event.block;
using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when an ItemStack is successfully burned as fuel in a furnace.
     */
    public class FurnaceBurnEventArgs : BlockEventArgs, Cancellable
    {
        private readonly ItemStack fuel;
        private int burnTime;
        private bool cancelled;
        private bool burning;

        public FurnaceBurnEventArgs(Block furnace, ItemStack fuel, int burnTime) : base(furnace)
        {
            this.fuel = fuel;
            this.burnTime = burnTime;
            this.cancelled = false;
            this.burning = true;
        }

        /**
         * Gets the fuel ItemStack for this event
         *
         * @return the fuel ItemStack
         */
        public ItemStack getFuel()
        {
            return fuel;
        }

        /**
         * Gets the burn time for this fuel
         *
         * @return the burn time for this fuel
         */
        public int getBurnTime()
        {
            return burnTime;
        }

        /**
         * Sets the burn time for this fuel
         *
         * @param burnTime the burn time for this fuel
         */
        public void setBurnTime(int burnTime)
        {
            this.burnTime = burnTime;
        }

        /**
         * Gets whether the furnace's fuel is burning or not.
         *
         * @return whether the furnace's fuel is burning or not.
         */
        public bool isBurning()
        {
            return this.burning;
        }

        /**
         * Sets whether the furnace's fuel is burning or not.
         *
         * @param burning true if the furnace's fuel is burning
         */
        public void setBurning(bool burning)
        {
            this.burning = burning;
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
