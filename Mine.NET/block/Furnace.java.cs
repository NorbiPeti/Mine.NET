using Mine.NET.inventory;

namespace Mine.NET.block
{
    /**
     * Represents a furnace.
     */
    public interface Furnace : BlockState, InventoryHolder
    {

        /**
         * Get burn time.
         *
         * @return Burn time
         */
        short getBurnTime();

        /**
         * Set burn time.
         *
         * @param burnTime Burn time
         */
        void setBurnTime(short burnTime);

        /**
         * Get cook time.
         *
         * @return Cook time
         */
        short getCookTime();

        /**
         * Set cook time.
         *
         * @param cookTime Cook time
         */
        void setCookTime(short cookTime);

        FurnaceInventory getInventory();
    }
}
