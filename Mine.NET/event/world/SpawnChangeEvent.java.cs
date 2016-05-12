namespace Mine.NET.event.world;

import org.bukkit.World;
import org.bukkit.Location;
import org.bukkit.event.HandlerList;

/**
 * An event that is called when a world's spawn changes. The world's previous
 * spawn location is included.
 */
public class SpawnChangeEvent : WorldEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Location previousLocation;

    public SpawnChangeEvent(World world, readonly Location previousLocation) {
        base(world);
        this.previousLocation = previousLocation;
    }

    /**
     * Gets the previous spawn location
     *
     * @return Location that used to be spawn
     */
    public Location getPreviousLocation() {
        return previousLocation;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
