using System;
using System.Collections.Generic;

namespace Mine.NET.material
{
    /**
     * Represents the different types of smooth bricks.
     */
    public class SmoothBrick : TexturedMaterial
    {

        private static readonly List<Materials> textures = new List<Materials>
        {
            Materials.STONE,
            Materials.MOSSY_COBBLESTONE,
            Materials.COBBLESTONE,
            Materials.SMOOTH_BRICK
        };

        public SmoothBrick() : base(Materials.SMOOTH_BRICK)
        {
        }

        public SmoothBrick(Materials type) : base((textures.Contains(type)) ? Materials.SMOOTH_BRICK : type)
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
        public SmoothBrick(Materials type, byte data) : base(type, data)
        {
        }

        public override List<Materials> getTextures()
        {
            return textures;
        }

        public override SmoothBrick clone()
        {
            return (SmoothBrick)base.clone();
        }
    }
}
