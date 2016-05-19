using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * MaterialData for signs
     */
    public class Sign : MaterialData, Attachable
    {
        public Sign() : base(Materials.SIGN_POST)
        {
        }

        public Sign(Materials type) : base(type)
        {
        }

        /**
         * @param type the raw type id
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Sign(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Check if this sign is attached to a wall
         *
         * @return true if this sign is attached to a wall, false if set on top of
         *     a block
         */
        public bool isWallSign()
        {
            return getItemType() == Materials.WALL_SIGN;
        }

        /**
         * Gets the face that this block is attached on
         *
         * @return BlockFaces attached to
         */
        public BlockFaces getAttachedFace()
        {
            if (isWallSign())
            {
                byte data = getData();

                switch (data)
                {
                    case 0x2:
                        return BlockFaces.SOUTH;

                    case 0x3:
                        return BlockFaces.NORTH;

                    case 0x4:
                        return BlockFaces.EAST;

                    case 0x5:
                        return BlockFaces.WEST;
                }

                return BlockFaces.SELF;
            }
            else
            {
                return BlockFaces.DOWN;
            }
        }

        /**
         * Gets the direction that this sign is currently facing
         *
         * @return BlockFaces indicating where this sign is facing
         */
        public BlockFaces getFacing()
        {
            byte data = getData();

            if (!isWallSign())
            {
                switch (data)
                {
                    case 0x0:
                        return BlockFaces.SOUTH;

                    case 0x1:
                        return BlockFaces.SOUTH_SOUTH_WEST;

                    case 0x2:
                        return BlockFaces.SOUTH_WEST;

                    case 0x3:
                        return BlockFaces.WEST_SOUTH_WEST;

                    case 0x4:
                        return BlockFaces.WEST;

                    case 0x5:
                        return BlockFaces.WEST_NORTH_WEST;

                    case 0x6:
                        return BlockFaces.NORTH_WEST;

                    case 0x7:
                        return BlockFaces.NORTH_NORTH_WEST;

                    case 0x8:
                        return BlockFaces.NORTH;

                    case 0x9:
                        return BlockFaces.NORTH_NORTH_EAST;

                    case 0xA:
                        return BlockFaces.NORTH_EAST;

                    case 0xB:
                        return BlockFaces.EAST_NORTH_EAST;

                    case 0xC:
                        return BlockFaces.EAST;

                    case 0xD:
                        return BlockFaces.EAST_SOUTH_EAST;

                    case 0xE:
                        return BlockFaces.SOUTH_EAST;

                    case 0xF:
                        return BlockFaces.SOUTH_SOUTH_EAST;
                }

                return BlockFaces.SELF;
            }
            else
            {
                return getAttachedFace().getOppositeFace();
            }
        }

        public void setFacingDirection(BlockFaces face)
        {
            byte data;

            if (isWallSign())
            {
                switch (face)
                {
                    case BlockFaces.NORTH:
                        data = 0x2;
                        break;

                    case BlockFaces.SOUTH:
                        data = 0x3;
                        break;

                    case BlockFaces.WEST:
                        data = 0x4;
                        break;

                    case BlockFaces.EAST:
                    default:
                        data = 0x5;
                        break;
                }
            }
            else
            {
                switch (face)
                {
                    case BlockFaces.SOUTH:
                        data = 0x0;
                        break;

                    case BlockFaces.SOUTH_SOUTH_WEST:
                        data = 0x1;
                        break;

                    case BlockFaces.SOUTH_WEST:
                        data = 0x2;
                        break;

                    case BlockFaces.WEST_SOUTH_WEST:
                        data = 0x3;
                        break;

                    case BlockFaces.WEST:
                        data = 0x4;
                        break;

                    case BlockFaces.WEST_NORTH_WEST:
                        data = 0x5;
                        break;

                    case BlockFaces.NORTH_WEST:
                        data = 0x6;
                        break;

                    case BlockFaces.NORTH_NORTH_WEST:
                        data = 0x7;
                        break;

                    case BlockFaces.NORTH:
                        data = 0x8;
                        break;

                    case BlockFaces.NORTH_NORTH_EAST:
                        data = 0x9;
                        break;

                    case BlockFaces.NORTH_EAST:
                        data = 0xA;
                        break;

                    case BlockFaces.EAST_NORTH_EAST:
                        data = 0xB;
                        break;

                    case BlockFaces.EAST:
                        data = 0xC;
                        break;

                    case BlockFaces.EAST_SOUTH_EAST:
                        data = 0xD;
                        break;

                    case BlockFaces.SOUTH_SOUTH_EAST:
                        data = 0xF;
                        break;

                    case BlockFaces.SOUTH_EAST:
                    default:
                        data = 0xE;
                        break;
                }
            }

            setData(data);
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing();
        }

        public override Sign clone()
        {
            return (Sign)base.clone();
        }
    }
}
