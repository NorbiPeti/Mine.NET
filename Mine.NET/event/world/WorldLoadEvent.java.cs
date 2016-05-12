namespace Mine.NET.event.world;

import org.bukkit.World;
import org.bukkit.event.HandlerList;

/**
 * Called when a World is loaded
 */
public class WorldLoadEvent : WorldEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public WorldLoadEvent(World world) {
        base(world);
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
