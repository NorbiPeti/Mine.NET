namespace Mine.NET.event.hanging;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Hanging;

/**
 * Triggered when a hanging entity is removed by an entity
 */
public class HangingBreakByEntityEvent : HangingBreakEvent {
    private readonly Entity remover;

    public HangingBreakByEntityEvent(Hanging hanging, readonly Entity remover) {
        base(hanging, HangingBreakEvent.RemoveCause.ENTITY);
        this.remover = remover;
    }

    /**
     * Gets the entity that removed the hanging entity
     *
     * @return the entity that removed the hanging entity
     */
    public Entity getRemover() {
        return remover;
    }
}
