package org.bukkit.enchantments;

import org.bukkit.inventory.ItemStack;

/**
 * A simple wrapper for ease of selecting {@link Enchantment}s
 */
public class EnchantmentWrapper : Enchantment {
    public EnchantmentWrapper(int id) {
        base(id);
    }

    /**
     * Gets the enchantment bound to this wrapper
     *
     * @return Enchantment
     */
    public Enchantment getEnchantment() {
        return Enchantment.getById(getId());
    }

    public override int getMaxLevel() {
        return getEnchantment().getMaxLevel();
    }

    public override int getStartLevel() {
        return getEnchantment().getStartLevel();
    }

    public override EnchantmentTarget getItemTarget() {
        return getEnchantment().getItemTarget();
    }

    public override bool canEnchantItem(ItemStack item) {
        return getEnchantment().canEnchantItem(item);
    }

    public override String getName() {
        return getEnchantment().getName();
    }

    public override bool conflictsWith(Enchantment other) {
        return getEnchantment().conflictsWith(other);
    }
}
