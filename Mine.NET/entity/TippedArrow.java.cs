using Mine.NET.potion;
using System.Collections.Generic;

namespace Mine.NET.entity
{
    public interface TippedArrow : Arrow
    {

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
         * this arrow.
         * <p>
         * Plugins should check that hasCustomEffects() returns true before calling
         * this method.
         *
         * @return the immutable list of custom potion effects
         */
        List<PotionEffect> getCustomEffects();

        /**
         * Adds a custom potion effect to this arrow.
         *
         * @param effect the potion effect to add
         * @param overwrite true if any existing effect of the same type should be
         * overwritten
         * @return true if the effect was added as a result of this call
         */
        bool addCustomEffect(PotionEffect effect, bool overwrite);

        /**
         * Removes a custom potion effect from this arrow.
         *
         * @param type the potion effect type to remove
         * @return true if the an effect was removed as a result of this call
         * @throws ArgumentException if this operation would leave the Arrow
         * in a state with no Custom Effects and PotionType.UNCRAFTABLE
         */
        bool removeCustomEffect(PotionEffectType type);

        /**
         * Checks for a specific custom potion effect type on this arrow.
         *
         * @param type the potion effect type to check for
         * @return true if the potion has this effect
         */
        bool hasCustomEffect(PotionEffectType type);

        /**
         * Removes all custom potion effects from this arrow.
         *
         * @throws ArgumentException if this operation would leave the Arrow
         * in a state with no Custom Effects and PotionType.UNCRAFTABLE
         */
        void clearCustomEffects();
    }
}
