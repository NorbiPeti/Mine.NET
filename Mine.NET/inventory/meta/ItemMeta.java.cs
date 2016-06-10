using Mine.NET.configuration.serialization;
using Mine.NET.enchantments;
using System;
using System.Collections.Generic;

namespace Mine.NET.inventory.meta
{
    /**
     * This type represents the storage mechanism for auxiliary item data.
     * <p>
     * An implementation will handle the creation and application for ItemMeta.
     * This class should not be implemented by a plugin in a live environment.
     */
    public interface ItemMeta<T> : ItemMeta where T : ItemMeta<T> //Recursion FTW
    {
        new T Clone(); //TODO
    }

    public interface ItemMeta : ConfigurationSerializable
    {
        ItemMeta Clone();

        /**
         * Checks for existence of a display name.
         *
         * @return true if this has a display name
         */
        bool hasDisplayName();

        /**
         * Gets the display name that is set.
         * <p>
         * Plugins should check that hasDisplayName() returns <code>true</code>
         * before calling this method.
         *
         * @return the display name that is set
         */
        String getDisplayName();

        /**
         * Sets the display name.
         *
         * @param name the name to set
         */
        void setDisplayName(String name);

        /**
         * Checks for existence of lore.
         *
         * @return true if this has lore
         */
        bool hasLore();

        /**
         * Gets the lore that is set.
         * <p>
         * Plugins should check if hasLore() returns <code>true</code> before
         * calling this method.
         * 
         * @return a list of lore that is set
         */
        List<String> getLore();

        /**
         * Sets the lore for this item. 
         * Removes lore when given null.
         *
         * @param lore the lore that will be set
         */
        void setLore(List<String> lore);

        /**
         * Checks for the existence of any enchantments.
         *
         * @return true if an enchantment exists on this meta
         */
        bool hasEnchants();

        /**
         * Checks for existence of the specified enchantment.
         *
         * @param ench enchantment to check
         * @return true if this enchantment exists for this meta
         */
        bool hasEnchant(Enchantment ench);

        /**
         * Checks for the level of the specified enchantment.
         *
         * @param ench enchantment to check
         * @return The level that the specified enchantment has, or 0 if none
         */
        int getEnchantLevel(Enchantment ench);

        /**
         * Returns a copy the enchantments in this ItemMeta. <br> 
         * Returns an empty map if none.
         *
         * @return An immutable copy of the enchantments
         */
        Dictionary<Enchantment, int> getEnchants();

        /**
         * Adds the specified enchantment to this item meta.
         *
         * @param ench Enchantment to add
         * @param level Level for the enchantment
         * @param ignoreLevelRestriction this indicates the enchantment should be
         *     applied, ignoring the level limit
         * @return true if the item meta changed as a result of this call, false
         *     otherwise
         */
        bool addEnchant(Enchantment ench, int level, bool ignoreLevelRestriction);

        /**
         * Removes the specified enchantment from this item meta.
         *
         * @param ench Enchantment to remove
         * @return true if the item meta changed as a result of this call, false
         *     otherwise
         */
        bool removeEnchant(Enchantment ench);

        /**
         * Checks if the specified enchantment conflicts with any enchantments in
         * this ItemMeta.
         *
         * @param ench enchantment to test
         * @return true if the enchantment conflicts, false otherwise
         */
        bool hasConflictingEnchant(Enchantment ench);

        /**
         * Set itemflags which should be ignored when rendering a ItemStack in the Client. This Method does silently ignore double set itemFlags.
         *
         * @param itemFlags The hideflags which shouldn't be rendered
         */
        void addItemFlags(params ItemFlag[] itemFlags);

        /**
         * Remove specific set of itemFlags. This tells the Client it should render it again. This Method does silently ignore double removed itemFlags.
         *
         * @param itemFlags Hideflags which should be removed
         */
        void removeItemFlags(params ItemFlag[] itemFlags);

        /**
         * Get current set itemFlags. The collection returned is unmodifiable.
         *
         * @return A set of all itemFlags set
         */
        HashSet<ItemFlag> getItemFlags();

        /**
         * Check if the specified flag is present on this item.
         *
         * @param flag the flag to check
         * @return if it is present
         */
        bool hasItemFlag(ItemFlag flag);
    }
}
