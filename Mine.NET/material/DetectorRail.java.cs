using System;

namespace Mine.NET.material
{
    /**
     * Represents a detector rail
     */
    public class DetectorRail : ExtendedRails, PressureSensor {
        public DetectorRail() : base(Materials.DETECTOR_RAIL) {
        }

        public DetectorRail(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public DetectorRail(Materials type, byte data) : base(type, data) {
    }

    public bool isPressed() {
            return (getData() & 0x8) == 0x8;
        }

        public void setPressed(bool isPressed) {
            setData((byte)(isPressed ? (getData() | 0x8) : (getData() & ~0x8)));
        }

        public override DetectorRail clone() {
            return (DetectorRail)base.clone();
        }
    }
}
