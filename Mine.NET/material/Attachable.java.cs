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
         * @return BlockFaces attached to
         */
        BlockFaces getAttachedFace();
    }
}
