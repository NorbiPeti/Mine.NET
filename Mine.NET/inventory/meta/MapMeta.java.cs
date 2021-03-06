namespace Mine.NET.inventory.meta
{
    /**
     * Represents a map that can be scalable.
     */
    public interface MapMeta : ItemMeta<MapMeta>
    {

        /**
         * Checks to see if this map is scaling.
         *
         * @return true if this map is scaling
         */
        bool isScaling();

        /**
         * Sets if this map is scaling or not.
         *
         * @param value true to scale
         */
        void setScaling(bool value);
    }
}
