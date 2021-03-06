using Mine.NET.block;
using System;

namespace Mine.NET.material
{

    /**
     * Represents a lever
     */
    public class Lever : SimpleAttachableMaterialData, Redstone
    {
        public Lever() : base(Materials.LEVER)
        {
        }

        public Lever(Materials type) : base(type)
        {
        }

        private bool powered;
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
         * Set this lever to be powered or not.
         *
         * @param isPowered whether the lever should be powered or not
         */
        public void setPowered(bool isPowered)
        {
            //setData((byte)(isPowered ? (getData() | 0x8) : (getData() & ~0x8)));
            powered = isPowered;
        }

        private BlockFaces attached;
        /**
         * Gets the face that this block is attached on
         *
         * @return BlockFaces attached to
         */
        public override BlockFaces getAttachedFace()
        {
            /*byte data = (byte)(getData() & 0x7);

            switch (data)
            {
                case 0x1:
                    return BlockFaces.WEST;

                case 0x2:
                    return BlockFaces.EAST;

                case 0x3:
                    return BlockFaces.NORTH;

                case 0x4:
                    return BlockFaces.SOUTH;

                case 0x5:
                case 0x6:
                    return BlockFaces.DOWN;

                case 0x0:
                case 0x7:
                    return BlockFaces.UP;

            }

            return BlockFaces.SELF;*/

            return attached;
        }

        private BlockFaces facing;
        /**
         * Sets the direction this lever is pointing in
         */
        public override void setFacingDirection(BlockFaces face)
        {
            //byte data = (byte)(getData() & 0x8);
            BlockFaces attach = getAttachedFace();

            if (attach == BlockFaces.DOWN)
            {
                switch (face)
                {
                    case BlockFaces.SOUTH:
                    case BlockFaces.NORTH:
                        //data |= 0x5;
                        facing = face;
                        break;

                    case BlockFaces.EAST:
                    case BlockFaces.WEST:
                        //data |= 0x6;
                        facing = face;
                        break;
                }
            }
            else if (attach == BlockFaces.UP)
            {
                switch (face)
                {
                    case BlockFaces.SOUTH:
                    case BlockFaces.NORTH:
                        //data |= 0x7;
                        facing = face;
                        break;

                    case BlockFaces.EAST:
                    case BlockFaces.WEST:
                        //data |= 0x0;
                        facing = face;
                        break;
                }
            }
            else
            {
                switch (face)
                {
                    case BlockFaces.EAST:
                    //data |= 0x1;
                    //break;

                    case BlockFaces.WEST:
                    //data |= 0x2;
                    //break;

                    case BlockFaces.SOUTH:
                    //data |= 0x3;
                    //break;

                    case BlockFaces.NORTH:
                        //data |= 0x4;
                        facing = face;
                        break;
                }
            }
            //setData(data);
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
        }

        public new Lever Clone() { return (Lever)base.Clone(); }
    }
}
