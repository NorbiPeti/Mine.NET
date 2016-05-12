namespace Mine.NET.entity
{
    /**
     * A wild tameable cat
     */
    public interface Ocelot : Animals, Tameable
    {

        /**
         * Gets the current type of this cat.
         *
         * @return Type of the cat.
         */
        OcelotType getCatType();

        /**
         * Sets the current type of this cat.
         *
         * @param type New type of this cat.
         */
        void setCatType(OcelotType type);

        /**
         * Checks if this ocelot is sitting
         *
         * @return true if sitting
         */
        bool isSitting();

        /**
         * Sets if this ocelot is sitting. Will remove any path that the ocelot
         * was following beforehand.
         *
         * @param sitting true if sitting
         */
        void setSitting(bool sitting);
    }

    /**
     * Represents the various different cat types there are.
     */
    public enum OcelotType
    {
        WILD_OCELOT = 0,
        BLACK_CAT = 1,
        RED_CAT = 2,
        SIAMESE_CAT = 3
    }
}
