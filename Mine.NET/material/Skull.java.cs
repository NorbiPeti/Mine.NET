using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a skull.
     */
    public class Skull : MaterialData, Directional
    {
        public Skull() : base(Materials.SKULL)
        {
        }

        /**
         * Instantiate a skull facing in a particular direction.
         *
         * @param direction the direction the skull's face is facing
         */
        public Skull(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        public Skull(Materials type) : base(type)
        {
        }

        private BlockFaces facing;
        public void setFacingDirection(BlockFaces face)
        {
            /*int data;

            switch (face)
            {
                case BlockFaces.SELF:
                default:
                    data = 0x1;
                    break;

                case BlockFaces.NORTH:
                    data = 0x2;
                    break;

                case BlockFaces.EAST:
                    data = 0x4;
                    break;

                case BlockFaces.SOUTH:
                    data = 0x3;
                    break;

                case BlockFaces.WEST:
                    data = 0x5;
                    break;
            }

            setData((byte)data);*/

            facing = face;
        }

        public BlockFaces getFacing()
        {
            /*int data = getData();

            switch (data)
            {
                case 0x1:
                default:
                    return BlockFaces.SELF;

                case 0x2:
                    return BlockFaces.NORTH;

                case 0x3:
                    return BlockFaces.SOUTH;

                case 0x4:
                    return BlockFaces.EAST;

                case 0x5:
                    return BlockFaces.WEST;
            }*/

            return facing;
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing();
        }

        public new Skull Clone() { return (Skull)base.Clone(); }
    }
}
