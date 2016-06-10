using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents stairs.
     */
    public class Stairs : MaterialData, Directional
    {

        public Stairs(Materials type) : base(type)
        {
        }

        private BlockFaces ascendingdir;
        /**
         * @return the direction the stairs ascend towards
         */
        public BlockFaces getAscendingDirection()
        {
            /*byte data = getData();

            switch (data & 0x3)
            {
                case 0x0:
                default:
                    return BlockFaces.EAST;

                case 0x1:
                    return BlockFaces.WEST;

                case 0x2:
                    return BlockFaces.SOUTH;

                case 0x3:
                    return BlockFaces.NORTH;
            }*/
            return ascendingdir;
        }

        /**
         * @return the direction the stairs descend towards
         */
        public BlockFaces getDescendingDirection()
        {
            return BlockFace.getOppositeFace(getAscendingDirection());
        }

        /**
         * Set the direction the stair part of the block is facing
         */
        public void setFacingDirection(BlockFaces face)
        {
            /*byte data;

            switch (face)
            {
                case BlockFaces.NORTH:
                    data = 0x3;
                    break;

                case BlockFaces.SOUTH:
                    data = 0x2;
                    break;

                case BlockFaces.EAST:
                default:
                    data = 0x0;
                    break;

                case BlockFaces.WEST:
                    data = 0x1;
                    break;
            }

            setData((byte)((getData() & 0xC) | data));*/
            ascendingdir = face;
        }

        /**
         * @return the direction the stair part of the block is facing
         */
        public BlockFaces getFacing()
        {
            return getDescendingDirection();
        }

        private bool inverted = false;
        /**
         * Test if step is inverted
         *
         * @return true if inverted (top half), false if normal (bottom half)
         */
        public bool isInverted()
        {
            //return ((getData() & 0x4) != 0);
            return inverted;
        }

        /**
         * Set step inverted state
         *
         * @param inv - true if step is inverted (top half), false if step is
         *     normal (bottom half)
         */
        public void setInverted(bool inv)
        {
            /*int dat = getData() & 0x3;
            if (inv)
            {
                dat |= 0x4;
            }
            setData((byte)dat);*/
            inverted = inv;
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing() + (isInverted() ? " inverted" : "");
        }

        public new Stairs Clone() { return (Stairs)base.Clone(); }
    }
}
