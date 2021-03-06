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

        //Find: "public override (\w+) clone\(\)[\s\r]+{[\s\r]+return \(\1\)base.clone\(\);[\s\r]+}" - Replace: "public new $1 Clone() { return ($1)base.Clone(); }"
        public new Chest Clone() { return (Chest)base.Clone(); }
    }
}
