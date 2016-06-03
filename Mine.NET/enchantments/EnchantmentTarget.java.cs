using Mine.NET;
using Mine.NET.inventory;
using System;

namespace Mine.NET.enchantments
{
    /**
     * Represents the applicable target for a {@link Enchantment}
     */
    public enum EnchantmentTarget
    {
        /**
         * Allows the Enchantment to be placed on all items
         */
        All,
        /**
         * Allows the Enchantment to be placed on armor
         */
        Armor,
        /**
         * Allows the Enchantment to be placed on feet slot armor
         */
        ArmorFeet,
        /**
         * Allows the Enchantment to be placed on leg slot armor
         */
        ArmorLegs,
        /**
         * Allows the Enchantment to be placed on torso slot armor
         */
        ArmorTorso,
        /**
         * Allows the Enchantment to be placed on head slot armor
         */
        ArmorHead,
        /**
         * Allows the Enchantment to be placed on weapons (swords)
         */
        Weapon,
        /**
         * Allows the Enchantment to be placed on tools (spades, pickaxe, hoes,
         * axes)
         */
        Tool,
        /**
         * Allows the Enchantment to be placed on bows.
         */
        Bow,
        /**
         * Allows the Enchantment to be placed on fishing rods.
         */
        FishingRod
    }

    public static class EnchantmentTarget_
    {
        /**
         * Check whether this target includes the specified item.
         *
         * @param item The item to check
         * @return True if the target includes the item
         */
        public static bool includes(this EnchantmentTarget type, Materials item)
        { //TODO: Use extension methods for every enum :D
            switch (type)
            {
                case EnchantmentTarget.All:
                    return true;
                case EnchantmentTarget.Armor:
                    return EnchantmentTarget.ArmorFeet.includes(item)
                        || EnchantmentTarget.ArmorLegs.includes(item)
                        || EnchantmentTarget.ArmorHead.includes(item)
                        || EnchantmentTarget.ArmorTorso.includes(item);
                case EnchantmentTarget.ArmorFeet:
                    return item.Equals(Materials.LEATHER_BOOTS)
                        || item.Equals(Materials.CHAINMAIL_BOOTS)
                        || item.Equals(Materials.IRON_BOOTS)
                        || item.Equals(Materials.DIAMOND_BOOTS)
                        || item.Equals(Materials.GOLD_BOOTS);
                case EnchantmentTarget.ArmorLegs:
                    return item.Equals(Materials.LEATHER_LEGGINGS)
                        || item.Equals(Materials.CHAINMAIL_LEGGINGS)
                        || item.Equals(Materials.IRON_LEGGINGS)
                        || item.Equals(Materials.DIAMOND_LEGGINGS)
                        || item.Equals(Materials.GOLD_LEGGINGS);
                case EnchantmentTarget.ArmorTorso:
                    return item.Equals(Materials.LEATHER_CHESTPLATE)
                        || item.Equals(Materials.CHAINMAIL_CHESTPLATE)
                        || item.Equals(Materials.IRON_CHESTPLATE)
                        || item.Equals(Materials.DIAMOND_CHESTPLATE)
                        || item.Equals(Materials.GOLD_CHESTPLATE);
                case EnchantmentTarget.ArmorHead:
                    return item.Equals(Materials.LEATHER_HELMET)
                        || item.Equals(Materials.CHAINMAIL_HELMET)
                        || item.Equals(Materials.DIAMOND_HELMET)
                        || item.Equals(Materials.IRON_HELMET)
                        || item.Equals(Materials.GOLD_HELMET);
                case EnchantmentTarget.Weapon:
                    return item.Equals(Materials.WOOD_SWORD)
                        || item.Equals(Materials.STONE_SWORD)
                        || item.Equals(Materials.IRON_SWORD)
                        || item.Equals(Materials.DIAMOND_SWORD)
                        || item.Equals(Materials.GOLD_SWORD);
                case EnchantmentTarget.Tool:
                    return item.Equals(Materials.WOOD_SPADE)
                        || item.Equals(Materials.STONE_SPADE)
                        || item.Equals(Materials.IRON_SPADE)
                        || item.Equals(Materials.DIAMOND_SPADE)
                        || item.Equals(Materials.GOLD_SPADE)
                        || item.Equals(Materials.WOOD_PICKAXE)
                        || item.Equals(Materials.STONE_PICKAXE)
                        || item.Equals(Materials.IRON_PICKAXE)
                        || item.Equals(Materials.DIAMOND_PICKAXE)
                        || item.Equals(Materials.GOLD_PICKAXE)
                        || item.Equals(Materials.WOOD_HOE)         // NOTE: No vanilla enchantments for this
                        || item.Equals(Materials.STONE_HOE)        // NOTE: No vanilla enchantments for this
                        || item.Equals(Materials.IRON_HOE)         // NOTE: No vanilla enchantments for this
                        || item.Equals(Materials.DIAMOND_HOE)      // NOTE: No vanilla enchantments for this
                        || item.Equals(Materials.GOLD_HOE)         // NOTE: No vanilla enchantments for this
                        || item.Equals(Materials.WOOD_AXE)
                        || item.Equals(Materials.STONE_AXE)
                        || item.Equals(Materials.IRON_AXE)
                        || item.Equals(Materials.DIAMOND_AXE)
                        || item.Equals(Materials.GOLD_AXE)
                        || item.Equals(Materials.SHEARS)           // NOTE: No vanilla enchantments for this
                        || item.Equals(Materials.FLINT_AND_STEEL); // NOTE: No vanilla enchantments for this
                case EnchantmentTarget.Bow:
                    return item.Equals(Materials.BOW);
                case EnchantmentTarget.FishingRod:
                    return item.Equals(Materials.FISHING_ROD);
                default:
                    throw new ArgumentException("Unknown enchantment type!");
            }
        }

        /**
         * Check whether this target includes the specified item.
         *
         * @param item The item to check
         * @return True if the target includes the item
         */
        public static bool includes(this EnchantmentTarget type, ItemStack item)
        {
            return includes(type, item.getType());
        }
    }
}
