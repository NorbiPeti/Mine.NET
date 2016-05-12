namespace Mine.NET.event.weather;

import org.bukkit.World;
import org.bukkit.event.Event;

/**
 * Represents a Weather-related event
 */
public abstract class WeatherEvent : Event {
    protected World world;

    public WeatherEvent(World where) {
        world = where;
    }

    /**
     * Returns the World where this event is occurring
     *
     * @return World this event is occurring in
     */
    public readonly World getWorld() {
        return world;
    }
}
