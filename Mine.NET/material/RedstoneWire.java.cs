using System;

namespace Mine.NET.material
{
    /**
     * Represents redstone wire
     */
    public class RedstoneWire : MaterialData, Redstone
    {
        public RedstoneWire() : base(Materials.REDSTONE_WIRE)
        {
        }

        public RedstoneWire(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public RedstoneWire(Materials type, byte data) : base(type, data)
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
            return getData() > 0;
        }

        public override string ToString()
        {
            return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
        }

        public new RedstoneWire Clone() { return (RedstoneWire)base.Clone(); }
    }
}
