using Mine.NET.inventory;

namespace Mine.NET.block
{
    /**
     * Represents a brewing stand.
     */
    public interface BrewingStand : BlockState, InventoryHolder
    {

        /**
         * How much time is left in the brewing cycle
         *
         * @return Brew Time
         */
        int getBrewingTime();

        /**
         * Set the time left before brewing completes.
         *
         * @param brewTime Brewing time
         */
        void setBrewingTime(int brewTime);

        /**
         * Get the level of current fuel for brewing.
         *
         * @return The fuel level
         */
        int getFuelLevel();

        /**
         * Set the level of current fuel for brewing.
         *
         * @param level fuel level
         */
        void setFuelLevel(int level);

        BrewerInventory getInventory();
    }
}
