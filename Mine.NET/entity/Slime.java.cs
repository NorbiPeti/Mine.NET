namespace Mine.NET.entity
{
    /**
     * Represents a Slime.
     */
    public interface Slime : LivingEntity
    {

        /**
         * @return The size of the slime
         */
        int getSize();

        /**
         * @param sz The new size of the slime.
         */
        void setSize(int sz);
    }
}
