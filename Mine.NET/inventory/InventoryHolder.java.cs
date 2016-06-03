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

    public interface InventoryHolder<T> : InventoryHolder
    {
        /**
         * Get the object's inventory.
         *
         * @return The inventory.
         */
        new T getInventory();
    }
}
