namespace Mine.NET.entity
{
    /**
     * Represents a wither skull {@link Fireball}.
     */
    public interface WitherSkull : Fireball
    {

        /**
         * Sets the charged status of the wither skull.
         *
         * @param charged whether it should be charged
         */
        void setCharged(bool charged);

        /**
         * Gets whether or not the wither skull is charged.
         *
         * @return whether the wither skull is charged
         */
        bool isCharged();
    }
}
