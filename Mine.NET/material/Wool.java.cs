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

        public Wool(DyeColor.Colors color) : this()
        {
            setColor(color);
        }

        public Wool(Materials type) : base(type)
        {
        }

        private DyeColor.Colors color;
        /**
         * Gets the current color of this dye
         *
         * @return DyeColor of this dye
         */
        public DyeColor.Colors getColor()
        {
            //return DyeColor.getByWoolData(getData());
            return color;
        }

        /**
         * Sets the color of this dye
         *
         * @param color New color of this dye
         */
        public void setColor(DyeColor.Colors color)
        {
            //setData(color.getWoolData());
            this.color = color;
        }

        public override string ToString()
        {
            return getColor() + " " + base.ToString();
        }

        public new Wool Clone() { return (Wool)base.Clone(); }
    }
}
