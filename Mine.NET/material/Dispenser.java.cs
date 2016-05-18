using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a dispenser.
     */
    public class Dispenser : FurnaceAndDispenser {

        public Dispenser() : base(Materials.DISPENSER) {
        }

        public Dispenser(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        /**
         * @param type the raw type id
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Dispenser(int type) : base(type) {
        }

        public Dispenser(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public Dispenser(Materials type, byte data) : base(type, data) {
    }

    public override void setFacingDirection(BlockFaces face) {
            byte data;

            switch (face) {
                case BlockFaces.DOWN:
                    data = 0x0;
                    break;

                case BlockFaces.UP:
                    data = 0x1;
                    break;

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

        public override BlockFaces getFacing() {
            int data = getData() & 0x7;

            switch (data) {
                case 0x0:
                    return BlockFaces.DOWN;

                case 0x1:
                    return BlockFaces.UP;

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

        public override Dispenser clone() {
            return (Dispenser)base.clone();
        }
    }
}
