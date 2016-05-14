using Mine.NET.potion;
using System.Collections.Generic;

namespace Mine.NET.inventory.meta
{
    /**
     * Represents a potion or item that can have custom effects.
     */
    public interface PotionMeta : ItemMeta<PotionMeta> {

        /**
         * Sets the underlying potion data
         *
         * @param data PotionData to set the base potion state to
         */
        void setBasePotionData(PotionData data);

        /**
         * Returns the potion data about the base potion
         *
         * @return a PotionData object
         */
        PotionData getBasePotionData();

        /**
         * Checks for the presence of custom potion effects.
         *
         * @return true if custom potion effects are applied
         */
        bool hasCustomEffects();

        /**
         * Gets an immutable list containing all custom potion effects applied to
         * this potion.
         * <p>
         * Plugins should check that hasCustomEffects() returns true before calling
         * this method.
         *
         * @return the immutable list of custom potion effects
         */
        List<PotionEffect> getCustomEffects();

        /**
         * Adds a custom potion effect to this potion.
         *
         * @param effect the potion effect to add
         * @param overwrite true if any existing effect of the same type should be
         * overwritten
         * @return true if the potion meta changed as a result of this call
         */
        bool addCustomEffect(PotionEffect effect, bool overwrite);

        /**
         * Removes a custom potion effect from this potion.
         *
         * @param type the potion effect type to remove
         * @return true if the potion meta changed as a result of this call
         */
        bool removeCustomEffect(PotionEffectType type);

        /**
         * Checks for a specific custom potion effect type on this potion.
         *
         * @param type the potion effect type to check for
         * @return true if the potion has this effect
         */
        bool hasCustomEffect(PotionEffectType type);

        /**
         * Removes all custom potion effects from this potion.
         *
         * @return true if the potion meta changed as a result of this call
         */
        bool clearCustomEffects();
    }
}
