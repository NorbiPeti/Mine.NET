using Mine.NET.block;

namespace Mine.NET.inventory
{
    /**
     * Interface to the inventory of a Double Chest.
     */
    public interface DoubleChestInventory : Inventory<DoubleChest>
    {

        /**
         * Get the left half of this double chest.
         *
         * @return The left side inventory
         */
        Inventory getLeftSide();

        /**
         * Get the right side of this double chest.
         *
         * @return The right side inventory
         */
        Inventory getRightSide();
    }
}
