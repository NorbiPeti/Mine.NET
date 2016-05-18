using Mine.NET.block;

namespace Mine.NET.material
{
    /**
     * Represents a chest
     */
    public class Chest : DirectionalContainer
    {

        public Chest() : base(Materials.CHEST)
        {
        }

        /**
         * Instantiate a chest facing in a particular direction.
         *
         * @param direction the direction the chest's lit opens towards
         */
        public Chest(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        public Chest(Materials type) : base(type)
        {
        }

        public override Chest clone()
        {
            return (Chest)base.clone();
        }
    }
}
