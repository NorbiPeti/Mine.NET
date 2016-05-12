namespace Mine.NET.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a player toggles their sprinting state
 */
public class PlayerToggleSprintEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly bool isSprinting;
    private bool cancel = false;

    public PlayerToggleSprintEvent(Player player, readonly bool isSprinting) {
        base(player);
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

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}