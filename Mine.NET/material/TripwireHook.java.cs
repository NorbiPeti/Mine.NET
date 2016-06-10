using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents the tripwire hook
     */
    public class TripwireHook : SimpleAttachableMaterialData, Redstone
    {

        public TripwireHook() : base(Materials.TRIPWIRE_HOOK)
        {
        }

        public TripwireHook(BlockFaces dir) : this()
        {
            setFacingDirection(dir);
        }

        private bool connected;
        /**
         * Test if tripwire is connected
         *
         * @return true if connected, false if not
         */
        public bool isConnected()
        {
            //return (getData() & 0x4) != 0;
            return connected;
        }

        /**
         * Set tripwire connection state
         *
         * @param connected - true if connected, false if not
         */
        public void setConnected(bool connected)
        {
            /*int dat = getData() & (0x8 | 0x3);
            if (connected)
            {
                dat |= 0x4;
            }
            setData((byte)dat);*/
            this.connected = connected;
        }

        private bool activated;
        /**
         * Test if hook is currently activated
         *
         * @return true if activated, false if not
         */
        public bool isActivated()
        {
            //return (getData() & 0x8) != 0;
            return activated;
        }

        /**
         * Set hook activated state
         *
         * @param act - true if activated, false if not
         */
        public void setActivated(bool act)
        {
            /*int dat = getData() & (0x4 | 0x3);
            if (act)
            {
                dat |= 0x8;
            }
            setData((byte)dat);*/
            activated = act;
        }

        private BlockFaces facing;
        public override void setFacingDirection(BlockFaces face)
        {
            /*int dat = getData() & 0xC;
            switch (face)
            {
                case BlockFaces.WEST:
                    dat |= 0x1;
                    break;
                case BlockFaces.NORTH:
                    dat |= 0x2;
                    break;
                case BlockFaces.EAST:
                    dat |= 0x3;
                    break;
                case BlockFaces.SOUTH:
                default:
                    break;
            }
            setData((byte)dat);*/
            facing = face;
        }

        public override BlockFaces getAttachedFace()
        {
            /*switch (getData() & 0x3)
            {
                case 0:
                    return BlockFaces.NORTH;
                case 1:
                    return BlockFaces.EAST;
                case 2:
                    return BlockFaces.SOUTH;
                case 3:
                    return BlockFaces.WEST;
            }
            return BlockFaces.SELF;*/
            return BlockFace.getOppositeFace(facing); //TODO: Equals?
        }

        public bool isPowered()
        {
            return isActivated();
        }

        public new TripwireHook Clone() { return (TripwireHook)base.Clone(); }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing() + (isActivated() ? " Activated" : "") + (isConnected() ? " Connected" : "");
        }
    }
}
