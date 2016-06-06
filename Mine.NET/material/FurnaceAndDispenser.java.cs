using System;

namespace Mine.NET.material
{
    /**
     * Represents a furnace or dispenser, two types of directional containers
     */
    public class FurnaceAndDispenser : DirectionalContainer {
        public FurnaceAndDispenser(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public FurnaceAndDispenser(Materials type, byte data) : base(type, data) {
    }

    public new FurnaceAndDispenser Clone() { return (FurnaceAndDispenser)base.Clone(); }
    }
}
