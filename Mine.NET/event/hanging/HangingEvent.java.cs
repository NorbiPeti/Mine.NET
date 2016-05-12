namespace Mine.NET.event.hanging;

import org.bukkit.entity.Hanging;
import org.bukkit.event.Event;

/**
 * Represents a hanging entity-related event.
 */
public abstract class HangingEvent : Event {
    protected Hanging hanging;

    protected HangingEvent(Hanging painting) {
        this.hanging = painting;
    }

    /**
     * Gets the hanging entity involved in this event.
     *
     * @return the hanging entity
     */
    public Hanging getEntity() {
        return hanging;
    }
}
