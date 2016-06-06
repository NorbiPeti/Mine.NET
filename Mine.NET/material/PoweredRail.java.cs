using System;

namespace Mine.NET.material
{

    /**
     * Represents a powered rail
     */
    public class PoweredRail : ExtendedRails, Redstone
    {
        public PoweredRail() : base(Materials.POWERED_RAIL)
        {
        }

        public PoweredRail(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public PoweredRail(Materials type, byte data) : base(type, data)
        {
        }

        public bool isPowered()
        {
            return (getData() & 0x8) == 0x8;
        }

        /**
         * Set whether this PoweredRail should be powered or not.
         *
         * @param isPowered whether or not the rail is powered
         */
        public void setPowered(bool isPowered)
        {
            setData((byte)(isPowered ? (getData() | 0x8) : (getData() & ~0x8)));
        }

        public new PoweredRail Clone() { return (PoweredRail)base.Clone(); }
    }
}
