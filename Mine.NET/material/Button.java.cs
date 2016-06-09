using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a button
     */
    public class Button : SimpleAttachableMaterialData, Redstone
    {
        private bool powered = false;
        private BlockFaces face;

        public Button() : base(Materials.STONE_BUTTON)
        {
        }

        public Button(Materials type) : base(type)
        {
        }

        /**
         * Gets the current state of this Materials, indicating if it's powered or
         * unpowered
         *
         * @return true if powered, otherwise false
         */
        public bool isPowered()
        {
            //return (getData() & 0x8) == 0x8;
            return powered;
        }

        /**
         * Sets the current state of this button
         *
         * @param bool
         *            whether or not the button is powered
         */
        public void setPowered(bool bool_)
        {
            //setData((byte)(bool_ ? (getData() | 0x8) : (getData() & ~0x8)));
            powered = bool_;
        }

        /**
         * Gets the face that this block is attached on
         *
         * @return BlockFaces attached to
         */
        public override BlockFaces getAttachedFace()
        {
            //byte data = (byte)(getData() & 0x7);

            /*switch (data)
            {
                case 0x0:
                    return BlockFaces.UP;

                case 0x1:
                    return BlockFaces.WEST;

                case 0x2:
                    return BlockFaces.EAST;

                case 0x3:
                    return BlockFaces.NORTH;

                case 0x4:
                    return BlockFaces.SOUTH;

                case 0x5:
                    return BlockFaces.DOWN;
            }*/

            return face;
        }

        /**
         * Sets the direction this button is pointing toward
         */
        public override void setFacingDirection(BlockFaces face)
        {
            //byte data = (byte)(getData() & 0x8);

            /*if (face == BlockFaces.DOWN)
                data |= 0x0;
            else if (face == BlockFaces.EAST)
                data |= 0x1;
            else if (face == BlockFaces.WEST)
                data |= 0x2;
            else if (face == BlockFaces.SOUTH)
                data |= 0x3;
            else if (face == BlockFaces.NORTH)
                data |= 0x4;
            else if (face == BlockFaces.UP)
                data |= 0x5;*/

            this.face = face;
        }

        public override string ToString()
        {
            return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
        }

        public new Button Clone()
        {
            return (Button)base.Clone();
        }
    }
}
