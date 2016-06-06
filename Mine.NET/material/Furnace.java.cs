using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a furnace.
     */
    public class Furnace : FurnaceAndDispenser {

        public Furnace() : base(Materials.FURNACE) {
        }

        /**
         * Instantiate a furnace facing in a particular direction.
         *
         * @param direction the direction the furnace's "opening" is facing
         */
        public Furnace(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        public Furnace(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public Furnace(Materials type, byte data) : base(type, data) {
    }

    public new Furnace Clone() { return (Furnace)base.Clone(); }
    }
}
