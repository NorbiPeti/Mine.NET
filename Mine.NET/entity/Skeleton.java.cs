namespace Mine.NET.entity
{
    /**
     * Represents a Skeleton.
     */
    public interface Skeleton : Monster
    {

        /**
         * Gets the current type of this skeleton.
         *
         * @return Current type
         */
        SkeletonType getSkeletonType();

        /**
         * Sets the new type of this skeleton.
         *
         * @param type New type
         */
        void setSkeletonType(SkeletonType type);
    }

    /*
     * Represents the various different Skeleton types.
     */
    public enum SkeletonType
    {
        NORMAL = 0,
        WITHER = 1
    }
}
