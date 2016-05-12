namespace Mine.NET.entity
{
    /**
     * Represents an instance of a lightning strike. May or may not do damage.
     */
    public interface LightningStrike : Weather
    {

        /**
         * Returns whether the strike is an effect that does no damage.
         *
         * @return whether the strike is an effect
         */
        bool isEffect();

    }
}
