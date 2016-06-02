using System;

namespace Mine.NET.material
{
    /**
     * Represents the different types of crops in different states of growth.
     *
     * @see Materials#CROPS
     * @see Materials#CARROT
     * @see Materials#POTATO
     * @see Materials#BEETROOT_BLOCK
     * @see Materials#NETHER_WARTS
     */
    public class Crops : MaterialData {
        protected static readonly Materials DEFAULT_TYPE = Materials.CROPS;
        protected static readonly CropState DEFAULT_STATE = CropState.SEEDED;

        private CropState state;

        /**
         * Constructs a wheat crop block in the seeded state.
         */ //Find: "\) \{\r\n\s+this\(([^)]+)\);" - Replace: ") : this($1) {"
        public Crops() : this(DEFAULT_TYPE, DEFAULT_STATE) {
        }

        /**
         * Constructs a wheat crop block in the given growth state
         *
         * @param state The growth state of the crops
         */
        public Crops(CropState state) : this(DEFAULT_TYPE, state) {
            setState(state);
        }

        /**
         * Constructs a crop block of the given type and in the given growth state
         *
         * @param type The type of crops
         * @param state The growth state of the crops
         */ //Find: "\) \{\r\n\s+base\(([^)]+)\);" - Replace: ") : base($1) {"
        public Crops(Materials type, CropState state) : base(type) {
            setState(state);
        }

        /**
         * Constructs a crop block of the given type and in the seeded state
         *
         * @param type The type of crops
         */
        public Crops(Materials type) : this(type, DEFAULT_STATE) {
        }

        /**
         * Gets the current growth state of this crop
         *
         * For crops with only four growth states such as beetroot, only the values SEEDED, SMALL, TALL and RIPE will be
         * returned.
         *
         * @return CropState of this crop
         */
        public CropState getState() {
            switch (getItemType()) {
                case Materials.CROPS:
                case Materials.CARROT:
                case Materials.POTATO:
                    // Mask the data just in case top bit set
                    //return (CropState)(getData() & 0x7);
                    return state;
                case Materials.BEETROOT_BLOCK:
                case Materials.NETHER_WARTS:
                    // Mask the data just in case top bits are set
                    // Will return SEEDED, SMALL, TALL, RIPE for the three growth data values
                    //return (CropState)(((getData() & 0x3) * 7 + 2) / 3);
                    return state;
                default:
                    throw new ArgumentException("Block type is not a crop");
            }
        }

        /**
         * Sets the growth state of this crop
         *
         * For crops with only four growth states such as beetroot, the 8 CropStates are mapped into four states:
         *
         * SEEDED, SMALL, TALL and RIPE
         *
         * GERMINATED will change to SEEDED
         * VERY_SMALL will change to SMALL
         * MEDIUM will change to TALL
         * VERY_TALL will change to RIPE
         *
         * @param state New growth state of this crop
         */
        public void setState(CropState state) {
            switch (getItemType()) {
                case Materials.CROPS:
                case Materials.CARROT:
                case Materials.POTATO:
                    // Preserve the top bit in case it is set
                    //setData((byte)((getData() & 0x8) | (byte)state)));
                    this.state = state;
                    break;
                case Materials.NETHER_WARTS:
                case Materials.BEETROOT_BLOCK:
                    // Preserve the top bits in case they are set
                    //setData((byte)((getData() & 0xC) | ((byte)state >> 1)));
                    this.state = state;
                    break;
                default:
                    throw new ArgumentException("Block type is not a crop");
            }
        }

        public override string ToString() {
            return getState() + " " + base.ToString();
        }

        public override Crops clone() {
            return (Crops)base.clone();
        }
    }
}
