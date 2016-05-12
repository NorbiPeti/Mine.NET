namespace Mine.NET.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;

/**
 * Called when a player leaves a server
 */
public class PlayerQuitEvent : PlayerEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private String quitMessage;

    public PlayerQuitEvent(Player who, readonly String quitMessage) {
        base(who);
        this.quitMessage = quitMessage;
    }

    /**
     * Gets the quit message to send to all online players
     *
     * @return string quit message
     */
    public String getQuitMessage() {
        return quitMessage;
    }

    /**
     * Sets the quit message to send to all online players
     *
     * @param quitMessage quit message
     */
    public void setQuitMessage(String quitMessage) {
        this.quitMessage = quitMessage;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
