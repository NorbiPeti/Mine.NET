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

        public BlockFaces getAttachedFace() {
            if (isWallBanner()) {
                byte data = getData();

                switch (data) {
                    case 0x2:
                        return BlockFaces.SOUTH;

                    case 0x3:
                        return BlockFaces.NORTH;

                    case 0x4:
                        return BlockFaces.EAST;

                    case 0x5:
                        return BlockFaces.WEST;
                }

                return null;
            } else {
                return BlockFaces.DOWN;
            }
        }

        public BlockFaces getFacing() {
            byte data = getData();

            if (!isWallBanner()) {
                switch (data) {
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

                return null;
            } else {
                return getAttachedFace().getOppositeFace();
            }
        }

        public void setFacingDirection(BlockFaces face)
        {
            byte data;

            if (isWallBanner())
            {
                if (face == BlockFaces.NORTH)
                    data = 0x2;
                else if (face == BlockFaces.SOUTH)
                    data = 0x3;
                else if (face == BlockFaces.WEST)
                    data = 0x4;
                else
                    data = 0x5;
            }
            else
            {
                if (face == BlockFaces.SOUTH)
                    data = 0x0;
                else if (face == BlockFaces.SOUTH_SOUTH_WEST)
                    data = 0x1;
                else if (face == BlockFaces.SOUTH_WEST)
                    data = 0x2;
                else if (face == BlockFaces.WEST_SOUTH_WEST)
                    data = 0x3;
                else if (face == BlockFaces.WEST)
                    data = 0x4;
                else if (face == BlockFaces.WEST_NORTH_WEST)
                    data = 0x5;
                else if (face == BlockFaces.NORTH_WEST)
                    data = 0x6;
                else if (face == BlockFaces.NORTH_NORTH_WEST)
                    data = 0x7;
                else if (face == BlockFaces.NORTH)
                    data = 0x8;
                else if (face == BlockFaces.NORTH_NORTH_EAST)
                    data = 0x9;
                else if (face == BlockFaces.NORTH_EAST)
                    data = 0xA;
                else if (face == BlockFaces.EAST_NORTH_EAST)
                    data = 0xB;
                else if (face == BlockFaces.EAST)
                    data = 0xC;
                else if (face == BlockFaces.EAST_SOUTH_EAST)
                    data = 0xD;
                else if (face == BlockFaces.SOUTH_SOUTH_EAST)
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
