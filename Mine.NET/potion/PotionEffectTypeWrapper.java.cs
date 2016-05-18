namespace Mine.NET.potion;

public class PotionEffectTypeWrapper : PotionEffectType {
    protected PotionEffectTypeWrapper(int id) : base(id) {
    }

    public override double getDurationModifier() {
        return getType().getDurationModifier();
    }

    public override String getName() {
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

    public override bool isInstant() {
        return getType().isInstant();
    }
}
