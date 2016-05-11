namespace Mine.NET
{
    /**
     * Represents a chest.
     */
    public interface Chest : BlockState, InventoryHolder
    {

        /**
         * Returns the chest's inventory. If this is a double chest, it returns
         * just the portion of the inventory linked to this half of the chest.
         *
         * @return The inventory.
         */
        Inventory getBlockInventory();
    }
}
