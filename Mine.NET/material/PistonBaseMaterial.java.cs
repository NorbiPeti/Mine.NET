using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Materials data for the piston base block
     */
    public class PistonBaseMaterial : MaterialData, Directional, Redstone
    {
        public PistonBaseMaterial(Materials type) : base(type)
        {
        }

        /**
         * Constructs a PistonBaseMaterial.
         * 
         * @param type the Materials type to use
         * @param data the raw data value 
         * [Obsolete] Magic value
         */
        [Obsolete]
        public PistonBaseMaterial(Materials type, byte data) : base(type, data)
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

        public bool isPowered()
        {
            return (getData() & 0x8) == 0x8;
        }

        /**
         * Sets the current state of this piston
         *
         * @param powered true if the piston is extended {@literal &} powered, or false
         */
        public void setPowered(bool powered)
        {
            setData((byte)(powered ? (getData() | 0x8) : (getData() & ~0x8)));
        }

        /**
         * Checks if this piston base is sticky, and returns true if so
         *
         * @return true if this piston is "sticky", or false
         */
        public bool isSticky()
        {
            return this.getItemType() == Materials.PISTON_STICKY_BASE;
        }

        public new PistonBaseMaterial Clone() { return (PistonBaseMaterial)base.Clone(); }
    }
}
