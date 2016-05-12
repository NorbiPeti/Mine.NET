package org.bukkit.event.enchantment;

import java.util.HashMap;
import java.util.Map;

import org.bukkit.block.Block;
import org.bukkit.enchantments.Enchantment;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;
import org.bukkit.event.inventory.InventoryEvent;
import org.bukkit.inventory.InventoryView;
import org.bukkit.inventory.ItemStack;

/**
 * Called when an ItemStack is successfully enchanted (currently at
 * enchantment table)
 */
public class EnchantItemEvent : InventoryEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Block table;
    private readonly ItemStack item;
    private int level;
    private bool cancelled;
    private readonly Dictionary<Enchantment,int> enchants;
    private readonly Player enchanter;
    private int button;

    public EnchantItemEvent(Player enchanter, readonly InventoryView view, readonly Block table, readonly ItemStack item, readonly int level, readonly Dictionary<Enchantment, int> enchants, readonly int i) {
        base(view);
        this.enchanter = enchanter;
        this.table = table;
        this.item = item;
        this.level = level;
        this.enchants = new Dictionary<Enchantment, int>(enchants);
        this.cancelled = false;
        this.button = i;
    }

    /**
     * Gets the player enchanting the item
     *
     * @return enchanting player
     */
    public Player getEnchanter() {
        return enchanter;
    }

    /**
     * Gets the block being used to enchant the item
     *
     * @return the block used for enchanting
     */
    public Block getEnchantBlock() {
        return table;
    }

    /**
     * Gets the item to be enchanted (can be modified)
     *
     * @return ItemStack of item
     */
    public ItemStack getItem() {
        return item;
    }

    /**
     * Get cost in exp levels of the enchantment
     *
     * @return experience level cost
     */
    public int getExpLevelCost() {
        return level;
    }

    /**
     * Set cost in exp levels of the enchantment
     *
     * @param level - cost in levels
     */
    public void setExpLevelCost(int level) {
        this.level = level;
    }

    /**
     * Get map of enchantment (levels, keyed by type) to be added to item
     * (modify map returned to change values). Note: Any enchantments not
     * allowed for the item will be ignored
     *
     * @return map of enchantment levels, keyed by enchantment
     */
    public Dictionary<Enchantment, int> getEnchantsToAdd() {
        return enchants;
    }

    /**
     * Which button was pressed to initiate the enchanting.
     *
     * @return The button index (0, 1, or 2).
     */
    public int whichButton() {
        return button;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
