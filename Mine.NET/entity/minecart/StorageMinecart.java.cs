using Mine.NET.inventory;

namespace Mine.NET.entity.minecart
{
    /**
     * Represents a minecart with a chest. These types of {@link Minecart
     * minecarts} have their own inventory that can be accessed using methods
     * from the {@link InventoryHolder} interface.
     */
    public interface StorageMinecart : Minecart, InventoryHolder
    {
    }
}
