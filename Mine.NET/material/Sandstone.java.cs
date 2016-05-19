using System;

namespace Mine.NET.material
{
    /**
     * Represents the different types of sandstone.
     */
    public class Sandstone : MaterialData
    {
        public Sandstone() : base(Materials.SANDSTONE)
        {
        }

        public Sandstone(SandstoneType type) : this()
        {
            setType(type);
        }

        public Sandstone(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Sandstone(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Gets the current type of this sandstone
         *
         * @return SandstoneType of this sandstone
         */
        public SandstoneType getType()
        {
            return SandstoneType.getByData(getData());
        }

        /**
         * Sets the type of this sandstone
         *
         * @param type New type of this sandstone
         */
        public void setType(SandstoneType type)
        {
            setData(type.getData());
        }

        public override string ToString()
        {
            return getType() + " " + base.ToString();
        }

        public override Sandstone clone()
        {
            return (Sandstone)base.clone();
        }
    }
}
