using System;

namespace Mine.NET.material
{

    /**
     * Represents a Wool/Cloth block
     */
    public class Wool : MaterialData, Colorable
    {
        public Wool() : base(Materials.WOOL)
        {
        }

        public Wool(DyeColor color) : this()
        {
            setColor(color);
        }

        public Wool(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Wool(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Gets the current color of this dye
         *
         * @return DyeColor of this dye
         */
        public DyeColor getColor()
        {
            return DyeColor.getByWoolData(getData());
        }

        /**
         * Sets the color of this dye
         *
         * @param color New color of this dye
         */
        public void setColor(DyeColor color)
        {
            setData(color.getWoolData());
        }

        public override string ToString()
        {
            return getColor() + " " + base.ToString();
        }

        public override Wool clone()
        {
            return (Wool)base.clone();
        }
    }
}
