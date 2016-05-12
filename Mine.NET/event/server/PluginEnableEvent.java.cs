namespace Mine.NET.event.server;

import org.bukkit.event.HandlerList;
import org.bukkit.plugin.Plugin;

/**
 * Called when a plugin is enabled.
 */
public class PluginEnableEvent : PluginEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public PluginEnableEvent(Plugin plugin) {
        base(plugin);
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
