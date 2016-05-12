package org.bukkit.potion;

/**
 * This enum reflects and matches each potion state that can be obtained from
 * the Creative mode inventory
 */
public enum PotionType {
    UNCRAFTABLE(null, false, false),
    WATER(null, false, false),
    MUNDANE(null, false, false),
    THICK(null, false, false),
    AWKWARD(null, false, false),
    NIGHT_VISION(PotionEffectType.NIGHT_VISION, false, true),
    INVISIBILITY(PotionEffectType.INVISIBILITY, false, true),
    JUMP(PotionEffectType.JUMP, true, true),
    FIRE_RESISTANCE(PotionEffectType.FIRE_RESISTANCE, false, true),
    SPEED(PotionEffectType.SPEED, true, true),
    SLOWNESS(PotionEffectType.SLOW, false, true),
    WATER_BREATHING(PotionEffectType.WATER_BREATHING, false, true),
    INSTANT_HEAL(PotionEffectType.HEAL, true, false),
    INSTANT_DAMAGE(PotionEffectType.HARM, true, false),
    POISON(PotionEffectType.POISON, true, true),
    REGEN(PotionEffectType.REGENERATION, true, true),
    STRENGTH(PotionEffectType.INCREASE_DAMAGE, true, true),
    WEAKNESS(PotionEffectType.WEAKNESS, false, true),
    LUCK(PotionEffectType.LUCK, false, false);
    ;

    private readonly PotionEffectType effect;
    private readonly bool upgradeable;
    private readonly bool extendable;

    PotionType(PotionEffectType effect, bool upgradeable, bool extendable) {
        this.effect = effect;
        this.upgradeable = upgradeable;
        this.extendable = extendable;
    }

    public PotionEffectType getEffectType() {
        return effect;
    }

    public bool isInstant() {
        return effect != null && effect.isInstant();
    }
    
    /**
     * Checks if the potion type has an upgraded state.
     * This refers to whether or not the potion type can be Tier 2,
     * such as Potion of Fire Resistance II.
     * 
     * @return true if the potion type can be upgraded;
     */
    public bool isUpgradeable() {
        return upgradeable;
    }

    /**
     * Checks if the potion type has an extended state.
     * This refers to the extended duration potions
     * 
     * @return true if the potion type can be extended
     */
    public bool isExtendable() {
        return extendable;
    }
    
    /**
     * [Obsolete] Non-functional
     */
    [Obsolete]
    public int getDamageValue() {
        return 0;
    }

    public int getMaxLevel() {
        return upgradeable ? 2 : 1;
    }

    /**
     * [Obsolete] Non-functional
     */
    [Obsolete]
    public static PotionType getByDamageValue(int damage) {
        return null;
    }

    /**
     * [Obsolete] Misleading
     */
    [Obsolete]
    public static PotionType getByEffect(PotionEffectType effectType) {
        if (effectType == null)
            return WATER;
        foreach (PotionType type  in  PotionType.values()) {
            if (effectType.equals(type.effect))
                return type;
        }
        return null;
    }
}
