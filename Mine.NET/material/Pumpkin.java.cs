using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a pumpkin.
     */
    public class Pumpkin : MaterialData, Directional
    {
        public Pumpkin() : base(Materials.PUMPKIN)
        {
        }

        /**
         * Instantiate a pumpkin facing in a particular direction.
         *
         * @param direction the direction the pumkin's face is facing
         */
        public Pumpkin(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        public Pumpkin(Materials type) : base(type)
        {
        }

        public bool isLit()
        {
            return getItemType() == Materials.JACK_O_LANTERN;
        }

        private BlockFaces facing;
        public void setFacingDirection(BlockFaces face)
        {
            /*byte data;

            switch (face)
            {
                case BlockFaces.NORTH:
                    data = 0x0;
                    break;

                case BlockFaces.EAST:
                    data = 0x1;
                    break;

                case BlockFaces.SOUTH:
                    data = 0x2;
                    break;

                case BlockFaces.WEST:
                default:
                    data = 0x3;
                    break;
            }

            setData(data);*/
            facing = face;
        }

        public BlockFaces getFacing()
        {
            /*byte data = getData();

            switch (data)
            {
                case 0x0:
                    return BlockFaces.NORTH;

                case 0x1:
                    return BlockFaces.EAST;

                case 0x2:
                    return BlockFaces.SOUTH;

                case 0x3:
                default:
                    return BlockFaces.EAST;
            }*/
            return facing;
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing() + " " + (isLit() ? "" : "NOT ") + "LIT";
        }

        public new Pumpkin Clone() { return (Pumpkin)base.Clone(); }
    }
}
