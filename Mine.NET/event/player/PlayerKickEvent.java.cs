namespace Mine.NET.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a player gets kicked from the server
 */
public class PlayerKickEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private String leaveMessage;
    private String kickReason;
    private bool cancel;

    public PlayerKickEvent(Player playerKicked, readonly String kickReason, readonly String leaveMessage) {
        base(playerKicked);
        this.kickReason = kickReason;
        this.leaveMessage = leaveMessage;
        this.cancel = false;
    }

    /**
     * Gets the reason why the player is getting kicked
     *
     * @return string kick reason
     */
    public String getReason() {
        return kickReason;
    }

    /**
     * Gets the leave message send to all online players
     *
     * @return string kick reason
     */
    public String getLeaveMessage() {
        return leaveMessage;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Sets the reason why the player is getting kicked
     *
     * @param kickReason kick reason
     */
    public void setReason(String kickReason) {
        this.kickReason = kickReason;
    }

    /**
     * Sets the leave message send to all online players
     *
     * @param leaveMessage leave message
     */
    public void setLeaveMessage(String leaveMessage) {
        this.leaveMessage = leaveMessage;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
