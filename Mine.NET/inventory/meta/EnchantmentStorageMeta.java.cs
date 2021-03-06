using Mine.NET.enchantments;
using System.Collections.Generic;

namespace Mine.NET.inventory.meta
{
    /**
     * EnchantmentMeta is specific to items that can <i>store</i> enchantments, as
     * opposed to being enchanted. {@link Materials#ENCHANTED_BOOK} is an example
     * of an item with enchantment storage.
     */
    public interface EnchantmentStorageMeta : ItemMeta<EnchantmentStorageMeta>
    {

        /**
         * Checks for the existence of any stored enchantments.
         *
         * @return true if an enchantment exists on this meta
         */
        bool hasStoredEnchants();

        /**
         * Checks for storage of the specified enchantment.
         *
         * @param ench enchantment to check
         * @return true if this enchantment is stored in this meta
         */
        bool hasStoredEnchant(Enchantment ench);

        /**
         * Checks for the level of the stored enchantment.
         *
         * @param ench enchantment to check
         * @return The level that the specified stored enchantment has, or 0 if
         *     none
         */
        int getStoredEnchantLevel(Enchantment ench);

        /**
         * Gets a copy the stored enchantments in this ItemMeta.
         *
         * @return An immutable copy of the stored enchantments
         */
        Dictionary<Enchantment, int> getStoredEnchants();

        /**
         * Stores the specified enchantment in this item meta.
         *
         * @param ench Enchantment to store
         * @param level Level for the enchantment
         * @param ignoreLevelRestriction this indicates the enchantment should be
         *     applied, ignoring the level limit
         * @return true if the item meta changed as a result of this call, false
         *     otherwise
         * @throws ArgumentException if enchantment is null
         */
        bool addStoredEnchant(Enchantment ench, int level, bool ignoreLevelRestriction);

        /**
         * Remove the specified stored enchantment from this item meta.
         *
         * @param ench Enchantment to remove
         * @return true if the item meta changed as a result of this call, false
         *     otherwise
         * @throws ArgumentException if enchantment is null
         */
        bool removeStoredEnchant(Enchantment ench);

        /**
         * Checks if the specified enchantment conflicts with any enchantments in
         * this ItemMeta.
         *
         * @param ench enchantment to test
         * @return true if the enchantment conflicts, false otherwise
         */
        bool hasConflictingStoredEnchant(Enchantment ench);
    }
}
