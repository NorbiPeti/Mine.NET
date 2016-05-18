namespace Mine.NET.inventory.meta
{
    /**
     * Represents a meta that can store a single FireworkEffect. An example
     * includes {@link Materials#FIREWORK_CHARGE}.
     */
    public interface FireworkEffectMeta : ItemMeta<FireworkEffectMeta>
    {

        /**
         * Sets the firework effect for this meta.
         *
         * @param effect the effect to set, or null to indicate none.
         */
        void setEffect(FireworkEffect effect);

        /**
         * Checks if this meta has an effect.
         *
         * @return true if this meta has an effect, false otherwise
         */
        bool hasEffect();

        /**
         * Gets the firework effect for this meta.
         *
         * @return the current effect, or null if none
         */
        FireworkEffect getEffect();
    }
}
