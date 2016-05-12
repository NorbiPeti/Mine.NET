namespace Mine.NET.event.inventory;

import org.bukkit.event.HandlerList;
import org.bukkit.inventory.AnvilInventory;
import org.bukkit.inventory.InventoryView;
import org.bukkit.inventory.ItemStack;

/**
 * Called when an item is put in a slot for repair by an anvil.
 */
public class PrepareAnvilEvent : InventoryEvent {

    private static readonly HandlerList handlers = new HandlerList();
    private ItemStack result;

    public PrepareAnvilEvent(InventoryView inventory, ItemStack result) {
        base(inventory);
        this.result = result;
    }

    public override AnvilInventory getInventory() {
        return (AnvilInventory) base.getInventory();
    }

    /**
     * Get result item, may be null.
     *
     * @return result item
     */
    public ItemStack getResult() {
        return result;
    }

    public void setResult(ItemStack result) {
        this.result = result;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
