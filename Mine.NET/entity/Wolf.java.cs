namespace Mine.NET.entity
{
    /**
     * Represents a Wolf
     */
    public interface Wolf : Animals, Tameable
    {

        /**
         * Checks if this wolf is angry
         *
         * @return Anger true if angry
         */
        bool isAngry();

        /**
         * Sets the anger of this wolf.
         * <p>
         * An angry wolf can not be fed or tamed, and will actively look for
         * targets to attack.
         *
         * @param angry true if angry
         */
        void setAngry(bool angry);

        /**
         * Checks if this wolf is sitting
         *
         * @return true if sitting
         */
        bool isSitting();

        /**
         * Sets if this wolf is sitting.
         * <p>
         * Will remove any path that the wolf was following beforehand.
         *
         * @param sitting true if sitting
         */
        void setSitting(bool sitting);

        /**
         * Get the collar color of this wolf
         *
         * @return the color of the collar
         */
        DyeColor getCollarColor();

        /**
         * Set the collar color of this wolf
         *
         * @param color the color to apply
         */
        void setCollarColor(DyeColor color);
    }
}
