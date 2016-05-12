using Mine.NET.inventory;

namespace Mine.NET.entity
{
    /**
     * Represents an Item Frame
     */
    public interface ItemFrame : Hanging
    {

        /**
         * Get the item in this frame
         *
         * @return a defensive copy the item in this item frame
         */
        ItemStack getItem();

        /**
         * Set the item in this frame
         *
         * @param item the new item
         */
        void setItem(ItemStack item);

        /**
         * Get the rotation of the frame's item
         *
         * @return the direction
         */
        Rotation getRotation();

        /**
         * Set the rotation of the frame's item
         *
         * @param rotation the new rotation
         * @throws ArgumentException if rotation is null
         */
        void setRotation(Rotation rotation);
    }
}
