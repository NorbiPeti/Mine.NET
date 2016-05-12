namespace Mine.NET.entity
{
    /**
     * Represents a Pig.
     */
    public interface Pig : Animals, Vehicle
    {

        /**
         * Check if the pig has a saddle.
         *
         * @return if the pig has been saddled.
         */
        bool hasSaddle();

        /**
         * Sets if the pig has a saddle or not
         *
         * @param saddled set if the pig has a saddle or not.
         */
        void setSaddle(bool saddled);
    }
}
