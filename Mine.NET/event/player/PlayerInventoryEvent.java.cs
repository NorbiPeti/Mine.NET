namespace Mine.NET.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;
import org.bukkit.event.inventory.InventoryClickEvent;
import org.bukkit.event.inventory.InventoryOpenEvent;
import org.bukkit.inventory.Inventory;

/**
 * Represents a player related inventory event; note that this event never
 * actually did anything
 *
 * [Obsolete] Use {@link InventoryClickEvent} or {@link InventoryOpenEvent}
 *     instead, or one of the other inventory events in {@link
 *     org.bukkit.event.inventory}.
 */
[Obsolete]
public class PlayerInventoryEvent : PlayerEvent {
    private static readonly HandlerList handlers = new HandlerList();
    protected Inventory inventory;

    public PlayerInventoryEvent(Player player, readonly Inventory inventory) {
        base(player);
        this.inventory = inventory;
    }

    /**
     * Gets the Inventory involved in this event
     *
     * @return Inventory
     */
    public Inventory getInventory() {
        return inventory;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
