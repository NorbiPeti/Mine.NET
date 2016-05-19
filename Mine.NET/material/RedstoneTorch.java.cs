using System;

namespace Mine.NET.material
{
    /**
     * Represents a redstone torch
     */
    public class RedstoneTorch : Torch, Redstone
    {
        public RedstoneTorch() : base(Materials.REDSTONE_TORCH_ON)
        {
        }

        public RedstoneTorch(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public RedstoneTorch(Materials type, byte data) : base(type, data)
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
            return getItemType() == Materials.REDSTONE_TORCH_ON;
        }

        public override string ToString()
        {
            return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
        }

        public override RedstoneTorch clone()
        {
            return (RedstoneTorch)base.clone();
        }
    }
}
