using Mine.NET.block;
using System;

namespace Mine.NET.material
{

    /**
     * Materials data for the piston extension block
     */
    public class PistonExtensionMaterial : MaterialData, Attachable
    {
        public PistonExtensionMaterial(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public PistonExtensionMaterial(Materials type, byte data) : base(type, data)
        {
        }

        public void setFacingDirection(BlockFaces face)
        {
            byte data = (byte)(getData() & 0x8);

            switch (face)
            {
                case BlockFaces.UP:
                    data |= 1;
                    break;
                case BlockFaces.NORTH:
                    data |= 2;
                    break;
                case BlockFaces.SOUTH:
                    data |= 3;
                    break;
                case BlockFaces.WEST:
                    data |= 4;
                    break;
                case BlockFaces.EAST:
                    data |= 5;
                    break;
            }
            setData(data);
        }

        public BlockFaces getFacing()
        {
            byte dir = (byte)(getData() & 7);

            switch (dir)
            {
                case 0:
                    return BlockFaces.DOWN;
                case 1:
                    return BlockFaces.UP;
                case 2:
                    return BlockFaces.NORTH;
                case 3:
                    return BlockFaces.SOUTH;
                case 4:
                    return BlockFaces.WEST;
                case 5:
                    return BlockFaces.EAST;
                default:
                    return BlockFaces.SELF;
            }
        }

        /**
         * Checks if this piston extension is sticky, and returns true if so
         *
         * @return true if this piston is "sticky", or false
         */
        public bool isSticky()
        {
            return (getData() & 8) == 8;
        }

        /**
         * Sets whether or not this extension is sticky
         *
         * @param sticky true if sticky, otherwise false
         */
        public void setSticky(bool sticky)
        {
            setData((byte)(sticky ? (getData() | 0x8) : (getData() & ~0x8)));
        }

        public BlockFaces getAttachedFace()
        {
            return getFacing().getOppositeFace();
        }

        public override PistonExtensionMaterial clone()
        {
            return (PistonExtensionMaterial)base.clone();
        }
    }
}
