using System;

namespace Mine.NET.material
{

    /**
     * Represents nether wart
     */
    public class NetherWarts : MaterialData
    {
        public NetherWarts() : base(Materials.NETHER_WARTS)
        {
        }

        public NetherWarts(NetherWartsState state) : this()
        {
            setState(state);
        }

        public NetherWarts(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public NetherWarts(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Gets the current growth state of this nether wart
         *
         * @return NetherWartsState of this nether wart
         */
        public NetherWartsState getState()
        {
            switch (getData())
            {
                case 0:
                    return NetherWartsState.SEEDED;
                case 1:
                    return NetherWartsState.STAGE_ONE;
                case 2:
                    return NetherWartsState.STAGE_TWO;
                default:
                    return NetherWartsState.RIPE;
            }
        }

        /**
         * Sets the growth state of this nether wart
         *
         * @param state New growth state of this nether wart
         */
        public void setState(NetherWartsState state)
        {
            switch (state)
            {
                case NetherWartsState.SEEDED:
                    setData((byte)0x0);
                    return;
                case NetherWartsState.STAGE_ONE:
                    setData((byte)0x1);
                    return;
                case NetherWartsState.STAGE_TWO:
                    setData((byte)0x2);
                    return;
                case NetherWartsState.RIPE:
                    setData((byte)0x3);
                    return;
            }
        }

        public override string ToString()
        {
            return getState() + " " + base.ToString();
        }

        public override NetherWarts clone()
        {
            return (NetherWarts)base.clone();
        }
    }
}
