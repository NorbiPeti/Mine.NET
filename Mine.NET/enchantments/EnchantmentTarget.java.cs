namespace Mine.NET.enchantments
{
/**
 * Represents the applicable target for a {@link Enchantment}
 */
public enum EnchantmentTarget { //TODO
    /**
     * Allows the Enchantment to be placed on all items
     */
    ALL {
        public override bool includes(Materials item) {
            return true;
        }
    },

    /**
     * Allows the Enchantment to be placed on armor
     */
    ARMOR {
        public override bool includes(Materials item) {
            return ARMOR_FEET.includes(item)
                || ARMOR_LEGS.includes(item)
                || ARMOR_HEAD.includes(item)
                || ARMOR_TORSO.includes(item);
        }
    },

    /**
     * Allows the Enchantment to be placed on feet slot armor
     */
    ARMOR_FEET {
        public override bool includes(Materials item) {
            return item.equals(Materials.LEATHER_BOOTS)
                || item.equals(Materials.CHAINMAIL_BOOTS)
                || item.equals(Materials.IRON_BOOTS)
                || item.equals(Materials.DIAMOND_BOOTS)
                || item.equals(Materials.GOLD_BOOTS);
        }
    },

    /**
     * Allows the Enchantment to be placed on leg slot armor
     */
    ARMOR_LEGS {
        public override bool includes(Materials item) {
            return item.equals(Materials.LEATHER_LEGGINGS)
                || item.equals(Materials.CHAINMAIL_LEGGINGS)
                || item.equals(Materials.IRON_LEGGINGS)
                || item.equals(Materials.DIAMOND_LEGGINGS)
                || item.equals(Materials.GOLD_LEGGINGS);
        }
    },

    /**
     * Allows the Enchantment to be placed on torso slot armor
     */
    ARMOR_TORSO {
        public override bool includes(Materials item) {
            return item.equals(Materials.LEATHER_CHESTPLATE)
                || item.equals(Materials.CHAINMAIL_CHESTPLATE)
                || item.equals(Materials.IRON_CHESTPLATE)
                || item.equals(Materials.DIAMOND_CHESTPLATE)
                || item.equals(Materials.GOLD_CHESTPLATE);
        }
    },

    /**
     * Allows the Enchantment to be placed on head slot armor
     */
    ARMOR_HEAD {
        public override bool includes(Materials item) {
            return item.equals(Materials.LEATHER_HELMET)
                || item.equals(Materials.CHAINMAIL_HELMET)
                || item.equals(Materials.DIAMOND_HELMET)
                || item.equals(Materials.IRON_HELMET)
                || item.equals(Materials.GOLD_HELMET);
        }
    },

    /**
     * Allows the Enchantment to be placed on weapons (swords)
     */
    WEAPON {
        public override bool includes(Materials item) {
            return item.equals(Materials.WOOD_SWORD)
                || item.equals(Materials.STONE_SWORD)
                || item.equals(Materials.IRON_SWORD)
                || item.equals(Materials.DIAMOND_SWORD)
                || item.equals(Materials.GOLD_SWORD);
        }
    },

    /**
     * Allows the Enchantment to be placed on tools (spades, pickaxe, hoes,
     * axes)
     */
    TOOL {
        public override bool includes(Materials item) {
            return item.equals(Materials.WOOD_SPADE)
                || item.equals(Materials.STONE_SPADE)
                || item.equals(Materials.IRON_SPADE)
                || item.equals(Materials.DIAMOND_SPADE)
                || item.equals(Materials.GOLD_SPADE)
                || item.equals(Materials.WOOD_PICKAXE)
                || item.equals(Materials.STONE_PICKAXE)
                || item.equals(Materials.IRON_PICKAXE)
                || item.equals(Materials.DIAMOND_PICKAXE)
                || item.equals(Materials.GOLD_PICKAXE)
                || item.equals(Materials.WOOD_HOE)         // NOTE: No vanilla enchantments for this
                || item.equals(Materials.STONE_HOE)        // NOTE: No vanilla enchantments for this
                || item.equals(Materials.IRON_HOE)         // NOTE: No vanilla enchantments for this
                || item.equals(Materials.DIAMOND_HOE)      // NOTE: No vanilla enchantments for this
                || item.equals(Materials.GOLD_HOE)         // NOTE: No vanilla enchantments for this
                || item.equals(Materials.WOOD_AXE)
                || item.equals(Materials.STONE_AXE)
                || item.equals(Materials.IRON_AXE)
                || item.equals(Materials.DIAMOND_AXE)
                || item.equals(Materials.GOLD_AXE)
                || item.equals(Materials.SHEARS)           // NOTE: No vanilla enchantments for this
                || item.equals(Materials.FLINT_AND_STEEL); // NOTE: No vanilla enchantments for this
        }
    },

    /**
     * Allows the Enchantment to be placed on bows.
     */
    BOW {
        public override bool includes(Materials item) {
            return item.equals(Materials.BOW);
        }
    },

    /**
     * Allows the Enchantment to be placed on fishing rods.
     */
    FISHING_ROD {
        public override bool includes(Materials item) {
            return item.equals(Materials.FISHING_ROD);
        }
    };

    /**
     * Check whether this target includes the specified item.
     *
     * @param item The item to check
     * @return True if the target includes the item
     */
    public abstract bool includes(Materials item);

    /**
     * Check whether this target includes the specified item.
     *
     * @param item The item to check
     * @return True if the target includes the item
     */
    public bool includes(ItemStack item) {
        return includes(item.getType());
    }
}
}
