using System;

namespace Mine.NET.material
{

    /**
     * Represents a powered rail
     */
    public class PoweredRail : ExtendedRails, Redstone
    {
        private bool powered;
        public PoweredRail() : base(Materials.POWERED_RAIL)
        {
        }

        public PoweredRail(Materials type) : base(type)
        {
        }

        public bool isPowered()
        {
            //return (getData() & 0x8) == 0x8;
            return powered;
        }

        /**
         * Set whether this PoweredRail should be powered or not.
         *
         * @param isPowered whether or not the rail is powered
         */
        public void setPowered(bool isPowered)
        {
            //setData((byte)(isPowered ? (getData() | 0x8) : (getData() & ~0x8)));
            powered = isPowered;
        }

        public new PoweredRail Clone() { return (PoweredRail)base.Clone(); }
    }
}
