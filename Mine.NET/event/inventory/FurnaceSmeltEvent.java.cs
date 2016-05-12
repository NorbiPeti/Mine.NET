namespace Mine.NET.event.inventory;

import org.bukkit.block.Block;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;
import org.bukkit.event.block.BlockEvent;
import org.bukkit.inventory.ItemStack;

/**
 * Called when an ItemStack is successfully smelted in a furnace.
 */
public class FurnaceSmeltEvent : BlockEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly ItemStack source;
    private ItemStack result;
    private bool cancelled;

    public FurnaceSmeltEvent(Block furnace, readonly ItemStack source, readonly ItemStack result) {
        base(furnace);
        this.source = source;
        this.result = result;
        this.cancelled = false;
    }

    /**
     * Gets the smelted ItemStack for this event
     *
     * @return smelting source ItemStack
     */
    public ItemStack getSource() {
        return source;
    }

    /**
     * Gets the resultant ItemStack for this event
     *
     * @return smelting result ItemStack
     */
    public ItemStack getResult() {
        return result;
    }

    /**
     * Sets the resultant ItemStack for this event
     *
     * @param result new result ItemStack
     */
    public void setResult(ItemStack result) {
        this.result = result;
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
