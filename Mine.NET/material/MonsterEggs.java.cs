using System;
using System.Collections.Generic;

namespace Mine.NET.material
{

    /**
     * Represents the different types of monster eggs
     */
    public class MonsterEggs : TexturedMaterial
    {

        private static readonly List<Materials> textures = new List<Materials>
        {
            Materials.STONE,
            Materials.COBBLESTONE,
            Materials.SMOOTH_BRICK
        };

        public MonsterEggs() : base(Materials.MONSTER_EGGS)
        {
        }

        public MonsterEggs(Materials type) : base((textures.Contains(type)) ? Materials.MONSTER_EGGS : type)
        {
            if (textures.Contains(type))
            {
                setMaterial(type);
            }
        }

        public override List<Materials> getTextures()
        {
            return textures;
        }

        public new MonsterEggs Clone() { return (MonsterEggs)base.Clone(); }
    }
}
