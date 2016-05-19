using System;
using System.Collections.Generic;

namespace Mine.NET.material
{
    /**
     * Represents the different types of steps.
     */
    public class Step : TexturedMaterial
    {
        private static readonly List<Materials> textures = new List<Materials>
        {
            Materials.STONE,
            Materials.SANDSTONE,
            Materials.WOOD,
            Materials.COBBLESTONE,
            Materials.BRICK,
            Materials.SMOOTH_BRICK,
            Materials.NETHER_BRICK,
            Materials.QUARTZ_BLOCK
        };

        public Step() : base(Materials.STEP)
        {
        }

        public Step(Materials type) : base((textures.Contains(type)) ? Materials.STEP : type)
        {
            if (textures.Contains(type))
            {
                setMaterial(type);
            }
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Step(Materials type, byte data) : base(type, data)
        {
        }

        public override List<Materials> getTextures()
        {
            return textures;
        }

        /**
         * Test if step is inverted
         *
         * @return true if inverted (top half), false if normal (bottom half)
         */
        public bool isInverted()
        {
            return ((getData() & 0x8) != 0);
        }

        /**
         * Set step inverted state
         *
         * @param inv - true if step is inverted (top half), false if step is
         *     normal (bottom half)
         */
        public void setInverted(bool inv)
        {
            int dat = getData() & 0x7;
            if (inv)
            {
                dat |= 0x8;
            }
            setData((byte)dat);
        }

        /**
         *
         * [Obsolete] Magic value
         */
        [Obsolete]
        protected override int getTextureIndex()
        {
            return getData() & 0x7;
        }

        /**
         *
         * [Obsolete] Magic value
         */
        [Obsolete]
        protected override void setTextureIndex(int idx)
        {
            setData((byte)((getData() & 0x8) | idx));
        }

        public override Step clone()
        {
            return (Step)base.clone();
        }

        public override string ToString()
        {
            return base.ToString() + (isInverted() ? "inverted" : "");
        }
    }
}
