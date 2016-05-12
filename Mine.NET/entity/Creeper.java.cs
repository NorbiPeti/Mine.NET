namespace Mine.NET.entity
{
    /**
     * Represents a Creeper
     */
    public interface Creeper : Monster
    {

        /**
         * Checks if this Creeper is powered (Electrocuted)
         *
         * @return true if this creeper is powered
         */
        bool isPowered();

        /**
         * Sets the Powered status of this Creeper
         *
         * @param value New Powered status
         */
        void setPowered(bool value);
    }
}
