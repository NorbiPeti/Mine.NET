using Mine.NET.block;

namespace Mine.NET.inventory
{
    /**
     * Interface to the inventory of a Beacon.
     */
    public interface BeaconInventory : Inventory<Beacon>
    {

        /**
         * Set the item powering the beacon.
         *
         * @param item The new item
         */
        void setItem(ItemStack item);

        /**
         * Get the item powering the beacon.
         *
         * @return The current item.
         */
        ItemStack getItem();
    }
}
