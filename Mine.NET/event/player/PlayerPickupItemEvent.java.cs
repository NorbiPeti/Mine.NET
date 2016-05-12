namespace Mine.NET.event.player;

import org.bukkit.entity.Item;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Thrown when a player picks an item up from the ground
 */
public class PlayerPickupItemEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Item item;
    private bool cancel = false;
    private readonly int remaining;

    public PlayerPickupItemEvent(Player player, readonly Item item, readonly int remaining) {
        base(player);
        this.item = item;
        this.remaining = remaining;
    }

    /**
     * Gets the Item picked up by the player.
     *
     * @return Item
     */
    public Item getItem() {
        return item;
    }

    /**
     * Gets the amount remaining on the ground, if any
     *
     * @return amount remaining on the ground
     */
    public int getRemaining() {
        return remaining;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
