namespace Mine.NET.entity
{
    /**
     * A representation of an explosive entity
     */
    public interface Explosive : Entity
    {

        /**
         * Set the radius affected by this explosive's explosion
         *
         * @param yield The explosive yield
         */
        void setYield(float yield);

        /**
         * Return the radius or yield of this explosive's explosion
         *
         * @return the radius of blocks affected
         */
        float getYield();

        /**
         * Set whether or not this explosive's explosion causes fire
         *
         * @param isIncendiary Whether it should cause fire
         */
        void setIsIncendiary(bool isIncendiary);

        /**
         * Return whether or not this explosive creates a fire when exploding
         *
         * @return true if the explosive creates fire, false otherwise
         */
        bool isIncendiary();
    }
}
