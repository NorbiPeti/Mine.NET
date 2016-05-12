namespace Mine.NET.event.inventory;

import org.bukkit.inventory.InventoryView;
import org.bukkit.entity.HumanEntity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Represents a player related inventory event
 */
public class InventoryOpenEvent : InventoryEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;

    public InventoryOpenEvent(InventoryView transaction) {
        base(transaction);
        this.cancelled = false;
    }

    /**
     * Returns the player involved in this event
     *
     * @return Player who is involved in this event
     */
    public readonly HumanEntity getPlayer() {
        return transaction.getPlayer();
    }

    /**
     * Gets the cancellation state of this event. A cancelled event will not
     * be executed in the server, but will still pass to other plugins.
     * <p>
     * If an inventory open event is cancelled, the inventory screen will not
     * show.
     *
     * @return true if this event is cancelled
     */
    public bool isCancelled() {
        return cancelled;
    }

    /**
     * Sets the cancellation state of this event. A cancelled event will not
     * be executed in the server, but will still pass to other plugins.
     * <p>
     * If an inventory open event is cancelled, the inventory screen will not
     * show.
     *
     * @param cancel true if you wish to cancel this event
     */
    public void setCancelled(bool cancel) {
        cancelled = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
