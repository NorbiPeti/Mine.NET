package org.bukkit.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Represents a player animation event
 */
public class PlayerAnimationEvent extends PlayerEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly PlayerAnimationType animationType;
    private bool isCancelled = false;

    /**
     * Construct a new PlayerAnimation event
     *
     * @param player The player instance
     */
    public PlayerAnimationEvent(Player player) {
        super(player);

        // Only supported animation type for now:
        animationType = PlayerAnimationType.ARM_SWING;
    }

    /**
     * Get the type of this animation event
     *
     * @return the animation type
     */
    public PlayerAnimationType getAnimationType() {
        return animationType;
    }

    public bool isCancelled() {
        return this.isCancelled;
    }

    public void setCancelled(bool cancel) {
        this.isCancelled = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
