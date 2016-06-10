using Mine.NET.entity;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a spawn egg that can be used to spawn mobs
     */
    public class SpawnEgg : MaterialData
    {

        public SpawnEgg() : base(Materials.MONSTER_EGG)
        {
        }

        public override string ToString()
        {
            return "SPAWN EGG";
        }

        public new SpawnEgg Clone() { return (SpawnEgg)base.Clone(); }
    }
}
