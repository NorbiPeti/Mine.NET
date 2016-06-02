namespace Mine.NET.inventory
{
    public interface InventoryHolder
    {
        /**
         * Get the object's inventory.
         *
         * @return The inventory.
         */
        Inventory getInventory();
    }

    public interface InventoryHolder<T>
    {
        /**
         * Get the object's inventory.
         *
         * @return The inventory.
         */
        T getInventory();
    }
}
