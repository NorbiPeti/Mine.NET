package org.bukkit.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a player toggles their sprinting state
 */
public class PlayerToggleSprintEvent extends PlayerEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly bool isSprinting;
    private bool cancel = false;

    public PlayerToggleSprintEvent(Player player, readonly bool isSprinting) {
        super(player);
        this.isSprinting = isSprinting;
    }

    /**
     * Gets whether the player is now sprinting or not.
     *
     * @return sprinting state
     */
    public bool isSprinting() {
        return isSprinting;
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