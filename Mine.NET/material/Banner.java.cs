using Mine.NET.block;

namespace Mine.NET.material
{
    public class Banner : MaterialData, Attachable {

        public Banner() :
            base(Materials.BANNER)
        {
        }

        public Banner(Materials type) :
            base(type)
        {
        }

        public bool isWallBanner() {
            return getItemType() == Materials.WALL_BANNER;
        }

        public BlockFace getAttachedFace() {
            if (isWallBanner()) {
                byte data = getData();

                switch (data) {
                    case 0x2:
                        return BlockFace.SOUTH;

                    case 0x3:
                        return BlockFace.NORTH;

                    case 0x4:
                        return BlockFace.EAST;

                    case 0x5:
                        return BlockFace.WEST;
                }

                return null;
            } else {
                return BlockFace.DOWN;
            }
        }

        public BlockFace getFacing() {
            byte data = getData();

            if (!isWallBanner()) {
                switch (data) {
                    case 0x0:
                        return BlockFace.SOUTH;

                    case 0x1:
                        return BlockFace.SOUTH_SOUTH_WEST;

                    case 0x2:
                        return BlockFace.SOUTH_WEST;

                    case 0x3:
                        return BlockFace.WEST_SOUTH_WEST;

                    case 0x4:
                        return BlockFace.WEST;

                    case 0x5:
                        return BlockFace.WEST_NORTH_WEST;

                    case 0x6:
                        return BlockFace.NORTH_WEST;

                    case 0x7:
                        return BlockFace.NORTH_NORTH_WEST;

                    case 0x8:
                        return BlockFace.NORTH;

                    case 0x9:
                        return BlockFace.NORTH_NORTH_EAST;

                    case 0xA:
                        return BlockFace.NORTH_EAST;

                    case 0xB:
                        return BlockFace.EAST_NORTH_EAST;

                    case 0xC:
                        return BlockFace.EAST;

                    case 0xD:
                        return BlockFace.EAST_SOUTH_EAST;

                    case 0xE:
                        return BlockFace.SOUTH_EAST;

                    case 0xF:
                        return BlockFace.SOUTH_SOUTH_EAST;
                }

                return null;
            } else {
                return getAttachedFace().getOppositeFace();
            }
        }

        public void setFacingDirection(BlockFace face)
        {
            byte data;

            if (isWallBanner())
            {
                if (face == BlockFace.NORTH)
                    data = 0x2;
                else if (face == BlockFace.SOUTH)
                    data = 0x3;
                else if (face == BlockFace.WEST)
                    data = 0x4;
                else
                    data = 0x5;
            }
            else
            {
                if (face == BlockFace.SOUTH)
                    data = 0x0;
                else if (face == BlockFace.SOUTH_SOUTH_WEST)
                    data = 0x1;
                else if (face == BlockFace.SOUTH_WEST)
                    data = 0x2;
                else if (face == BlockFace.WEST_SOUTH_WEST)
                    data = 0x3;
                else if (face == BlockFace.WEST)
                    data = 0x4;
                else if (face == BlockFace.WEST_NORTH_WEST)
                    data = 0x5;
                else if (face == BlockFace.NORTH_WEST)
                    data = 0x6;
                else if (face == BlockFace.NORTH_NORTH_WEST)
                    data = 0x7;
                else if (face == BlockFace.NORTH)
                    data = 0x8;
                else if (face == BlockFace.NORTH_NORTH_EAST)
                    data = 0x9;
                else if (face == BlockFace.NORTH_EAST)
                    data = 0xA;
                else if (face == BlockFace.EAST_NORTH_EAST)
                    data = 0xB;
                else if (face == BlockFace.EAST)
                    data = 0xC;
                else if (face == BlockFace.EAST_SOUTH_EAST)
                    data = 0xD;
                else if (face == BlockFace.SOUTH_SOUTH_EAST)
                    data = 0xF;
                else
                    data = 0xE;
            }

            setData(data);
        }

        public override string ToString() {
            return base.ToString() + " facing " + getFacing();
        }

        public Banner Clone() {
            return (Banner)base.clone(); //TODO
        }
    }
}
