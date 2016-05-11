namespace Mine.NET
{
    /**
     * Represents the different growth states of crops
     */
    public enum CropState
    {

        /**
         * State when first seeded
         */
        SEEDED = 0x0,
        /**
         * First growth stage
         */
        GERMINATED = 0x1,
        /**
         * Second growth stage
         */
        VERY_SMALL = 0x2,
        /**
         * Third growth stage
         */
        SMALL = 0x3,
        /**
         * Fourth growth stage
         */
        MEDIUM = 0x4,
        /**
         * Fifth growth stage
         */
        TALL = 0x5,
        /**
         * Almost ripe stage
         */
        VERY_TALL = 0x6,
        /**
         * Ripe stage
         */
        RIPE = 0x7
    }
}
