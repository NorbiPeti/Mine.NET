using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a furnace or a dispenser.
     */
    public class DirectionalContainer : MaterialData<byte>, Directional {
        public DirectionalContainer(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public DirectionalContainer(Materials type, byte data) : base(type, data) {
    }

    public virtual void setFacingDirection(BlockFaces face) {
            byte data;

            switch (face) {
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

            setData(data);
        }

        public virtual BlockFaces getFacing() {
            byte data = getData();

            switch (data) {
                case 0x2:
                    return BlockFaces.NORTH;

                case 0x3:
                    return BlockFaces.SOUTH;

                case 0x4:
                    return BlockFaces.WEST;

                case 0x5:
                default:
                    return BlockFaces.EAST;
            }
        }

        public override string ToString() {
            return base.ToString() + " facing " + getFacing();
        }

        public override DirectionalContainer clone() {
            return (DirectionalContainer)base.clone();
        }
    }
}
