using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents an ender chest
     */
    public class EnderChest : DirectionalContainer {

        public EnderChest() : base(Materials.ENDER_CHEST) {
        }

        /**
         * Instantiate an ender chest facing in a particular direction.
         *
         * @param direction the direction the ender chest's lid opens towards
         */
        public EnderChest(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        public EnderChest(Materials type) : base(type) {
        }

    public new EnderChest Clone() { return (EnderChest)base.Clone(); }
    }
}
