using Mine.NET.inventory;

namespace Mine.NET.entity.minecart
{
    /**
     * Represents a Minecart with a Hopper inside it
     */
    public interface HopperMinecart : Minecart, InventoryHolder
    {

        /**
         * Checks whether or not this Minecart will pick up 
         * items into its inventory.
         * 
         * @return true if the Minecart will pick up items
         */
        bool isEnabled();

        /**
         * Sets whether this Minecart will pick up items.
         * 
         * @param enabled new enabled state
         */
        void setEnabled(bool enabled);
    }
}
