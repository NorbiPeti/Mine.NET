namespace Mine.NET.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;

/**
 * This event is called after a player registers or unregisters a new plugin
 * channel.
 */
public abstract class PlayerChannelEvent : PlayerEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly String channel;

    public PlayerChannelEvent(Player player, readonly String channel) {
        base(player);
        this.channel = channel;
    }

    public readonly String getChannel() {
        return channel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
