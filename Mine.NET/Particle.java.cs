using Mine.NET.inventory;
using Mine.NET.material;
using System;

namespace Mine.NET
{
    public enum Particle
    {
        EXPLOSION_NORMAL,
        EXPLOSION_LARGE,
        EXPLOSION_HUGE,
        FIREWORKS_SPARK,
        WATER_BUBBLE,
        WATER_SPLASH,
        WATER_WAKE,
        SUSPENDED,
        SUSPENDED_DEPTH,
        CRIT,
        CRIT_MAGIC,
        SMOKE_NORMAL,
        SMOKE_LARGE,
        SPELL,
        SPELL_INSTANT,
        SPELL_MOB,
        SPELL_MOB_AMBIENT,
        SPELL_WITCH,
        DRIP_WATER,
        DRIP_LAVA,
        VILLAGER_ANGRY,
        VILLAGER_HAPPY,
        TOWN_AURA,
        NOTE,
        PORTAL,
        ENCHANTMENT_TABLE,
        FLAME,
        LAVA,
        FOOTSTEP,
        CLOUD,
        REDSTONE,
        SNOWBALL,
        SNOW_SHOVEL,
        SLIME,
        HEART,
        BARRIER,
        ITEM_CRACK,
        BLOCK_CRACK,
        BLOCK_DUST,
        WATER_DROP,
        ITEM_TAKE,
        MOB_APPEARANCE,
        DRAGON_BREATH,
        END_ROD,
        DAMAGE_INDICATOR,
        SWEEP_ATTACK
    }

    public static class ParticleC
    {
        /**
         * Returns the required data type for the particle
         * @return the required data type
         */
        public static Type GetDataType(Particle p)
        {
            switch (p)
            {
                case Particle.ITEM_CRACK:
                    return typeof(ItemStack);
                case Particle.BLOCK_CRACK:
                    return typeof(MaterialData);
                case Particle.BLOCK_DUST:
                    return typeof(MaterialData);
                default:
                    return typeof(void);
            }
        }
    }
}
