using Mine.NET.block;

namespace Mine.NET.material
{
    /**
     * Indicates that a block can be attached to another block
     */
    public interface Attachable : Directional
    {

        /**
         * Gets the face that this block is attached on
         *
         * @return BlockFace attached to
         */
        BlockFace getAttachedFace();
    }
}
