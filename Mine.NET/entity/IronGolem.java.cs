namespace Mine.NET.entity
{
    /**
     * An iron Golem that protects Villages.
     */
    public interface IronGolem : Golem
    {

        /**
         * Gets whether this iron golem was built by a player.
         *
         * @return Whether this iron golem was built by a player
         */
        bool isPlayerCreated();

        /**
         * Sets whether this iron golem was built by a player or not.
         *
         * @param playerCreated true if you want to set the iron golem as being
         *     player created, false if you want it to be a natural village golem.
         */
        void setPlayerCreated(bool playerCreated);
    }
}
