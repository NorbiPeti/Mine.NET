using System;

namespace Mine.NET.material
{
    /**
     * Represents dye
     */
    public class Dye : MaterialData, Colorable
    {
        private DyeColor.Colors color;

        public Dye() : base(Materials.INK_SACK)
        {
        }

        public Dye(Materials type) : base(type)
        {
        }

        /**
         * @param color color of the dye
         */
        public Dye(DyeColor.Colors color) : base(Materials.INK_SACK)
        {
            this.color = color;
        }

        /**
         * Gets the current color of this dye
         *
         * @return DyeColor of this dye
         */
        public DyeColor.Colors getColor()
        {
            return color;
        }

        /**
         * Sets the color of this dye
         *
         * @param color New color of this dye
         */
        public void setColor(DyeColor.Colors color)
        {
            this.color = color;
        }

        public override string ToString()
        {
            return getColor() + " DYE(" + color + ")";
        }

        public new Dye Clone() { return (Dye)base.Clone(); }
    }
}
