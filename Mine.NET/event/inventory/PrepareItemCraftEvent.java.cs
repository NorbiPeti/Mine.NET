package org.bukkit.event.inventory;

import org.bukkit.event.HandlerList;
import org.bukkit.inventory.CraftingInventory;
import org.bukkit.inventory.InventoryView;
import org.bukkit.inventory.Recipe;

public class PrepareItemCraftEvent : InventoryEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private bool repair;
    private CraftingInventory matrix;

    public PrepareItemCraftEvent(CraftingInventory what, InventoryView view, bool isRepair) {
        base(view);
        this.matrix = what;
        this.repair = isRepair;
    }

    /**
     * Get the recipe that has been formed. If this event was triggered by a
     * tool repair, this will be a temporary shapeless recipe representing the
     * repair.
     *
     * @return The recipe being crafted.
     */
    public Recipe getRecipe() {
        return matrix.getRecipe();
    }

    /**
     * @return The crafting inventory on which the recipe was formed.
     */
    public override CraftingInventory getInventory() {
        return matrix;
    }

    /**
     * Check if this event was triggered by a tool repair operation rather
     * than a crafting recipe.
     *
     * @return True if this is a repair.
     */
    public bool isRepair() {
        return repair;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
