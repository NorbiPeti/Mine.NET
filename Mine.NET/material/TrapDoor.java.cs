using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a trap door
     */
    public class TrapDoor : SimpleAttachableMaterialData, Openable
    {
        public TrapDoor() : base(Materials.TRAP_DOOR)
        {
        }

        public TrapDoor(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public TrapDoor(Materials type, byte data) : base(type, data)
        {
        }

        public bool isOpen()
        {
            return ((getData() & 0x4) == 0x4);
        }

        public void setOpen(bool isOpen)
        {
            byte data = getData();

            if (isOpen)
            {
                data |= 0x4;
            }
            else
            {
                data &= ~0x4;
            }

            setData(data);
        }

        /**
         * Test if trapdoor is inverted
         *
         * @return true if inverted (top half), false if normal (bottom half)
         */
        public bool isInverted()
        {
            return ((getData() & 0x8) != 0);
        }

        /**
         * Set trapdoor inverted state
         *
         * @param inv - true if inverted (top half), false if normal (bottom half)
         */
        public void setInverted(bool inv)
        {
            int dat = getData() & 0x7;
            if (inv)
            {
                dat |= 0x8;
            }
            setData((byte)dat);
        }

        public override BlockFaces getAttachedFace()
        {
            byte data = (byte)(getData() & 0x3);

            switch (data)
            {
                case 0x0:
                    return BlockFaces.SOUTH;

                case 0x1:
                    return BlockFaces.NORTH;

                case 0x2:
                    return BlockFaces.EAST;

                case 0x3:
                    return BlockFaces.WEST;
            }

            return null;

        }

        public override void setFacingDirection(BlockFaces face)
        {
            byte data = (byte)(getData() & 0xC);

            switch (face)
            {
                case BlockFaces.SOUTH:
                    data |= 0x1;
                    break;
                case BlockFaces.WEST:
                    data |= 0x2;
                    break;
                case BlockFaces.EAST:
                    data |= 0x3;
                    break;
            }

            setData(data);
        }

        public override string ToString()
        {
            return (isOpen() ? "OPEN " : "CLOSED ") + base.ToString() + " with hinges set " + getAttachedFace() + (isInverted() ? " inverted" : "");
        }

        public override TrapDoor clone()
        {
            return (TrapDoor)base.clone();
        }
    }
}
