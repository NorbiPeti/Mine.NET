using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a dispenser.
     */
    public class Dispenser : FurnaceAndDispenser {
        private BlockFaces face;

        public Dispenser() : base(Materials.DISPENSER) {
        }

        public Dispenser(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        public Dispenser(Materials type) : base(type) {
        }

    public override void setFacingDirection(BlockFaces face) {
            /*byte data;

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

            setData(data);*/
            this.face = face;
        }

        public override BlockFaces getFacing() {
            /*int data = getData() & 0x7;

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
            }*/
            return face;
        }

        public new Dispenser Clone() { return (Dispenser)base.Clone(); }
    }
}
