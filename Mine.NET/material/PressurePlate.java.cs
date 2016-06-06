using System;

namespace Mine.NET.material
{

    /**
     * Represents a pressure plate
     */
    public class PressurePlate : MaterialData, PressureSensor
    {
        public PressurePlate() : base(Materials.WOOD_PLATE)
        {
        }

        public PressurePlate(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public PressurePlate(Materials type, byte data) : base(type, data)
        {
        }

        public bool isPressed()
        {
            return getData() == 0x1;
        }

        public override string ToString()
        {
            return base.ToString() + (isPressed() ? " PRESSED" : "");
        }

        public new PressurePlate Clone() { return (PressurePlate)base.Clone(); }
    }
}
