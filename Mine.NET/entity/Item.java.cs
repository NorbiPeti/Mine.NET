using Mine.NET.inventory;

namespace Mine.NET.entity
{
    /**
     * Represents an Item.
     */
    public interface Item : Entity
    {

        /**
         * Gets the item stack associated with this item drop.
         *
         * @return An item stack.
         */
        ItemStack getItemStack();

        /**
         * Sets the item stack associated with this item drop.
         *
         * @param stack An item stack.
         */
        void setItemStack(ItemStack stack);

        /**
         * Gets the delay before this Item is available to be picked up by players
         *
         * @return Remaining delay
         */
        int getPickupDelay();

        /**
         * Sets the delay before this Item is available to be picked up by players
         *
         * @param delay New delay
         */
        void setPickupDelay(int delay);
    }
}
