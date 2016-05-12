package org.bukkit.event.enchantment;

import org.bukkit.block.Block;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;
import org.bukkit.event.inventory.InventoryEvent;
import org.bukkit.inventory.InventoryView;
import org.bukkit.inventory.ItemStack;

/**
 * Called when an ItemStack is inserted in an enchantment table - can be
 * called multiple times
 */
public class PrepareItemEnchantEvent : InventoryEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Block table;
    private readonly ItemStack item;
    private readonly int[] levelsOffered;
    private readonly int bonus;
    private bool cancelled;
    private readonly Player enchanter;

    public PrepareItemEnchantEvent(Player enchanter, InventoryView view, readonly Block table, readonly ItemStack item, readonly int[] levelsOffered, readonly int bonus) {
        base(view);
        this.enchanter = enchanter;
        this.table = table;
        this.item = item;
        this.levelsOffered = levelsOffered;
        this.bonus = bonus;
        this.cancelled = false;
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
     * Get list of offered exp level costs of the enchantment (modify values
     * to change offer)
     *
     * @return experience level costs offered
     */
    public int[] getExpLevelCostsOffered() {
        return levelsOffered;
    }

    /**
     * Get enchantment bonus in effect - corresponds to number of bookshelves
     *
     * @return enchantment bonus
     */
    public int getEnchantmentBonus() {
        return bonus;
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
