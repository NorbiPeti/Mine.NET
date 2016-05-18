namespace Mine.NET.block
{
    /**
     * Represents a Jukebox
     */
    public interface Jukebox : BlockState
    {
        /**
         * Get the record currently playing
         *
         * @return The record Materials, or AIR if none is playing
         */
        Materials getPlaying();

        /**
         * Set the record currently playing
         *
         * @param record The record Materials, or null/AIR to stop playing
         */
        void setPlaying(Materials record);

        /**
         * Check if the jukebox is currently playing a record
         *
         * @return True if there is a record playing
         */
        bool isPlaying();

        /**
         * Stop the jukebox playing and eject the current record
         *
         * @return True if a record was ejected; false if there was none playing
         */
        bool eject();
    }
}
