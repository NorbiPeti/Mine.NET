using Mine.NET.block;

namespace Mine.NET.material
{
    public interface Directional
    {

        /**
         * Sets the direction that this block is facing in
         *
         * @param face The facing direction
         */
        void setFacingDirection(BlockFaces face);

        /**
         * Gets the direction this block is facing
         *
         * @return the direction this block is facing
         */
        BlockFaces getFacing();
    }
}
