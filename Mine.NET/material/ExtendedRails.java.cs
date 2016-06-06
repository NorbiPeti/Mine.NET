using Mine.NET.block;
using System;

namespace Mine.NET.material {
    /**
     * This is the superclass for the {@link DetectorRail} and {@link PoweredRail}
     * classes
     */
    public class ExtendedRails : Rails {
        public ExtendedRails(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public ExtendedRails(Materials type, byte data) : base(type, data) {
    }

    public override bool isCurve() {
            return false;
        }

        /**
         *
         * [Obsolete] Magic value
         */
        [Obsolete]
        protected override byte getConvertedData() {
            return (byte)(getData() & 0x7);
        }

        public override void setDirection(BlockFaces face, bool isOnSlope) {
            bool extraBitSet = (getData() & 0x8) == 0x8; //Convert data bit fields

            if (face != BlockFaces.WEST && face != BlockFaces.EAST && face != BlockFaces.NORTH && face != BlockFaces.SOUTH) {
                throw new ArgumentException("Detector rails and powered rails cannot be set on a curve!");
            }

            base.setDirection(face, isOnSlope);
            setData((byte)(extraBitSet ? (getData() | 0x8) : (getData() & ~0x8)));
        }

        public new ExtendedRails Clone() { return (ExtendedRails)base.Clone(); }
    }
}
