using System;

namespace Mine.NET.material
{
    /**
     * Represents a furnace or dispenser, two types of directional containers
     */
    public class FurnaceAndDispenser : DirectionalContainer
    {
        public FurnaceAndDispenser(Materials type) : base(type)
        {
        }

        public new FurnaceAndDispenser Clone() { return (FurnaceAndDispenser)base.Clone(); }
    }
}
