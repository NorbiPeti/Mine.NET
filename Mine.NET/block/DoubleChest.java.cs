namespace Mine.NET.block
{
    /**
     * Represents a double chest.
     */
    public class DoubleChest : InventoryHolder
    {
        private DoubleChestInventory inventory;

        public DoubleChest(DoubleChestInventory chest)
        {
            inventory = chest;
        }

        public Inventory getInventory()
        {
            return inventory;
        }

        public InventoryHolder getLeftSide()
        {
            return inventory.getLeftSide().getHolder();
        }

        public InventoryHolder getRightSide()
        {
            return inventory.getRightSide().getHolder();
        }

        public Location getLocation()
        {
            return new Location(getWorld(), getX(), getY(), getZ());
        }

        public World getWorld()
        {
            return ((Chest)getLeftSide()).getWorld();
        }

        public double getX()
        {
            return 0.5 * (((Chest)getLeftSide()).getX() + ((Chest)getRightSide()).getX());
        }

        public double getY()
        {
            return 0.5 * (((Chest)getLeftSide()).getY() + ((Chest)getRightSide()).getY());
        }

        public double getZ()
        {
            return 0.5 * (((Chest)getLeftSide()).getZ() + ((Chest)getRightSide()).getZ());
        }
    }
}
