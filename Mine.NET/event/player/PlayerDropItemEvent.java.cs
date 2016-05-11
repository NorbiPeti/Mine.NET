package org.bukkit.event.player;

import org.bukkit.entity.Item;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Thrown when a player drops an item from their inventory
 */
public class PlayerDropItemEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Item drop;
    private bool cancel = false;

    public PlayerDropItemEvent(Player player, readonly Item drop) {
        base(player);
        this.drop = drop;
    }

    /**
     * Gets the ItemDrop created by the player
     *
     * @return ItemDrop created by the player
     */
    public Item getItemDrop() {
        return drop;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
