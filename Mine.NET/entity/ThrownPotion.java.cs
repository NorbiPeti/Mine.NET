using Mine.NET.inventory;
using Mine.NET.potion;
using System.Collections.ObjectModel;

namespace Mine.NET.entity
{
    /**
     * Represents a thrown potion bottle
     */
    public interface ThrownPotion : Projectile
    {

        /**
         * Returns the effects that are applied by this potion.
         *
         * @return The potion effects
         */
        Collection<PotionEffect> getEffects();

        /**
         * Returns a copy of the ItemStack for this thrown potion.
         * <p>
         * Altering this copy will not alter the thrown potion directly. If you want
         * to alter the thrown potion, you must use the {@link
         * #setItem(ItemStack) setItemStack} method.
         *
         * @return A copy of the ItemStack for this thrown potion.
         */
        ItemStack getItem();

        /**
         * Set the ItemStack for this thrown potion.
         * <p>
         * The ItemStack must be of type {@link org.bukkit.Materials#SPLASH_POTION}
         * or {@link org.bukkit.Materials#LINGERING_POTION}, otherwise an exception
         * is thrown.
         *
         * @param item New ItemStack
         */
        void setItem(ItemStack item);
    }
}
