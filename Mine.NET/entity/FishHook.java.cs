namespace Mine.NET.entity
{
    /**
     * Represents a fishing hook.
     */
    public interface FishHook : Projectile
    {
        /**
         * Gets the chance of a fish biting.
         * <p>
         * 0.0 = No Chance.<br>
         * 1.0 = Instant catch.
         *
         * @return chance the bite chance
         */
        double getBiteChance();

        /**
         * Sets the chance of a fish biting.
         * <p>
         * 0.0 = No Chance.<br>
         * 1.0 = Instant catch.
         *
         * @param chance the bite chance
         * @throws ArgumentException if the bite chance is not between 0
         *     and 1
         */
        void setBiteChance(double chance);
    }
}
