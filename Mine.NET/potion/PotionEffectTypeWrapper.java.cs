package org.bukkit.potion;

public class PotionEffectTypeWrapper : PotionEffectType {
    protected PotionEffectTypeWrapper(int id) {
        base(id);
    }

    @Override
    public double getDurationModifier() {
        return getType().getDurationModifier();
    }

    @Override
    public String getName() {
        return getType().getName();
    }

    /**
     * Get the potion type bound to this wrapper.
     *
     * @return The potion effect type
     */
    public PotionEffectType getType() {
        return PotionEffectType.getById(getId());
    }

    @Override
    public bool isInstant() {
        return getType().isInstant();
    }
}
