using System;

namespace Mine.NET.material
{
    /**
     * Represents dye
     */
    public class Dye : MaterialData<byte>, Colorable {
        public Dye() : base(Materials.INK_SACK) {
        }

        public Dye(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public Dye(Materials type, byte data) : base(type, data) {
    }

        /**
         * @param color color of the dye
         */
        public Dye(DyeColor color) : base(Materials.INK_SACK, color.getDyeData())
        {
        }

        /**
         * Gets the current color of this dye
         *
         * @return DyeColor of this dye
         */
        public DyeColor getColor() {
            return DyeColor.getByDyeData(getData());
        }

        /**
         * Sets the color of this dye
         *
         * @param color New color of this dye
         */
        public void setColor(DyeColor color) {
            setData(color.getDyeData());
        }

        public override string ToString() {
            return getColor() + " DYE(" + getData() + ")";
        }

        public override Dye clone() {
            return (Dye)base.clone();
        }
    }
}
