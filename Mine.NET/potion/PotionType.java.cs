namespace Mine.NET.potion
{
    /**
     * This enum reflects and matches each potion state that can be obtained from
     * the Creative mode inventory
     */
    public enum PotionTypes
    {
        UNCRAFTABLE,
        WATER,
        MUNDANE,
        THICK,
        AWKWARD,
        NIGHT_VISION,
        INVISIBILITY,
        JUMP,
        FIRE_RESISTANCE,
        SPEED,
        SLOWNESS,
        WATER_BREATHING,
        INSTANT_HEAL,
        INSTANT_DAMAGE,
        POISON,
        REGEN,
        STRENGTH,
        WEAKNESS,
        LUCK
    }

    public static class PotionType
    {
        public static PotionEffectType getEffectType(PotionTypes type)
        {
            switch (type)
            {
                case PotionTypes.UNCRAFTABLE:
                case PotionTypes.WATER:
                case PotionTypes.MUNDANE:
                case PotionTypes.THICK:
                case PotionTypes.AWKWARD:
                    return null;
                case PotionTypes.NIGHT_VISION:
                    return PotionEffectType.NIGHT_VISION;
                case PotionTypes.JUMP:
                    return PotionEffectType.JUMP;
                case PotionTypes.FIRE_RESISTANCE:
                    return PotionEffectType.FIRE_RESISTANCE;
                case PotionTypes.SPEED:
                    return PotionEffectType.SPEED;
                case PotionTypes.SLOWNESS:
                    return PotionEffectType.SLOW;
                case PotionTypes.WATER_BREATHING:
                    return PotionEffectType.WATER_BREATHING;
                case PotionTypes.INSTANT_HEAL:
                    return PotionEffectType.HEAL;
                case PotionTypes.INSTANT_DAMAGE:
                    return PotionEffectType.HARM;
                case PotionTypes.POISON:
                    return PotionEffectType.POISON;
                case PotionTypes.REGEN:
                    return PotionEffectType.REGENERATION;
                case PotionTypes.STRENGTH:
                    return PotionEffectType.INCREASE_DAMAGE;
                case PotionTypes.WEAKNESS:
                    return PotionEffectType.WEAKNESS;
                case PotionTypes.LUCK:
                    return PotionEffectType.LUCK;
                case PotionTypes.INVISIBILITY:
                    return PotionEffectType.INVISIBILITY;
                default:
                    return null;
            }
        }

        public static bool isInstant(PotionTypes type)
        {
            return getEffectType(type) != null && getEffectType(type).isInstant();
        }

        /**
         * Checks if the potion type has an upgraded state.
         * This refers to whether or not the potion type can be Tier 2,
         * such as Potion of Fire Resistance II.
         * 
         * @return true if the potion type can be upgraded;
         */
        public static bool isUpgradeable(PotionTypes type)
        {
            switch (type)
            {
                case PotionTypes.JUMP:
                case PotionTypes.SPEED:
                case PotionTypes.INSTANT_HEAL:
                case PotionTypes.INSTANT_DAMAGE:
                case PotionTypes.POISON:
                case PotionTypes.REGEN:
                case PotionTypes.STRENGTH:
                    return true;
                case PotionTypes.UNCRAFTABLE:
                case PotionTypes.WATER:
                case PotionTypes.MUNDANE:
                case PotionTypes.THICK:
                case PotionTypes.AWKWARD:
                case PotionTypes.NIGHT_VISION:
                case PotionTypes.FIRE_RESISTANCE:
                case PotionTypes.SLOWNESS:
                case PotionTypes.WATER_BREATHING:
                case PotionTypes.WEAKNESS:
                case PotionTypes.LUCK:
                case PotionTypes.INVISIBILITY:
                default:
                    return false;
            }
        }

        /**
         * Checks if the potion type has an extended state.
         * This refers to the extended duration potions
         * 
         * @return true if the potion type can be extended
         */
        public static bool isExtendable(PotionTypes type)
        {
            switch (type)
            {
                case PotionTypes.JUMP:
                case PotionTypes.SPEED:
                case PotionTypes.POISON:
                case PotionTypes.REGEN:
                case PotionTypes.STRENGTH:
                case PotionTypes.WEAKNESS:
                case PotionTypes.WATER_BREATHING:
                case PotionTypes.FIRE_RESISTANCE:
                case PotionTypes.SLOWNESS:
                case PotionTypes.NIGHT_VISION:
                case PotionTypes.INVISIBILITY:
                    return true;
                case PotionTypes.UNCRAFTABLE:
                case PotionTypes.WATER:
                case PotionTypes.MUNDANE:
                case PotionTypes.THICK:
                case PotionTypes.AWKWARD:
                case PotionTypes.LUCK:
                case PotionTypes.INSTANT_HEAL:
                case PotionTypes.INSTANT_DAMAGE:
                default:
                    return false;
            }
        }

        public static int getMaxLevel(PotionTypes type)
        {
            return isUpgradeable(type) ? 2 : 1;
        }
    }
}
