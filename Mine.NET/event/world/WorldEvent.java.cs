namespace Mine.NET.event.world;

import org.bukkit.World;
import org.bukkit.event.Event;

/**
 * Represents events within a world
 */
public abstract class WorldEvent : Event {
    private readonly World world;

    public WorldEvent(World world) {
        this.world = world;
    }

    /**
     * Gets the world primarily involved with this event
     *
     * @return World which caused this event
     */
    public World getWorld() {
        return world;
    }
}
