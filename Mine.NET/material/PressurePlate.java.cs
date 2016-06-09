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

        private bool pressed;
        public bool isPressed()
        {
            //return getData() == 0x1;
            return pressed;
        }

        public override string ToString()
        {
            return base.ToString() + (isPressed() ? " PRESSED" : "");
        }

        public new PressurePlate Clone() { return (PressurePlate)base.Clone(); }
    }
}
