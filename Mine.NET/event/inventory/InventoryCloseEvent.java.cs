
package org.bukkit.event.inventory;

import org.bukkit.entity.HumanEntity;
import org.bukkit.event.HandlerList;
import org.bukkit.inventory.InventoryView;

/**
 * Represents a player related inventory event
 */
public class InventoryCloseEvent : InventoryEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public InventoryCloseEvent(InventoryView transaction) {
        base(transaction);
    }

    /**
     * Returns the player involved in this event
     *
     * @return Player who is involved in this event
     */
    public readonly HumanEntity getPlayer() {
        return transaction.getPlayer();
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
