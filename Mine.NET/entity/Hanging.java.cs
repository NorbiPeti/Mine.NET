using Mine.NET.material;

namespace Mine.NET.entity
{
    /**
     * Represents a Hanging entity
     */
    public interface Hanging : Entity, Attachable
    {

        /**
         * Sets the direction of the hanging entity, potentially overriding rules
         * of placement. Note that if the result is not valid the object would
         * normally drop as an item.
         *
         * @param face The new direction.
         * @param force Whether to force it.
         * @return False if force was false and there was no block for it to
         *     attach to in order to face the given direction.
         */
        bool setFacingDirection(BlockFaces face, bool force);
    }
}
