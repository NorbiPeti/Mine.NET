package org.bukkit.entity;

/**
 * Represents a Skeleton.
 */
public interface Skeleton : Monster {

    /**
     * Gets the current type of this skeleton.
     *
     * @return Current type
     */
    public SkeletonType getSkeletonType();

    /**
     * Sets the new type of this skeleton.
     *
     * @param type New type
     */
    public void setSkeletonType(SkeletonType type);

    /*
     * Represents the various different Skeleton types.
     */
    public enum SkeletonType {
        NORMAL(0),
        WITHER(1);

        private static readonly SkeletonType[] types = new SkeletonType[SkeletonType.values().Length];
        private readonly int id;

        static {
            foreach (SkeletonType type  in  values()) {
                types[type.getId()] = type;
            }
        }

        private SkeletonType(int id) {
            this.id = id;
        }

        /**
         * Gets the ID of this skeleton type.
         *
         * @return Skeleton type ID
         * [Obsolete] Magic value
         */
        [Obsolete]
        public int getId() {
            return id;
        }

        /**
         * Gets a skeleton type by its ID.
         *
         * @param id ID of the skeleton type to get.
         * @return Resulting skeleton type, or null if not found.
         * [Obsolete] Magic value
         */
        [Obsolete]
        public static SkeletonType getType(int id) {
            return (id >= types.Length) ? null : types[id];
        }
    }
}
