namespace Mine.NET.event.entity;

import org.bukkit.block.Block;
import org.bukkit.entity.Entity;

/**
 * Called when a block causes an entity to combust.
 */
public class EntityCombustByBlockEvent : EntityCombustEvent {
    private readonly Block combuster;

    public EntityCombustByBlockEvent(Block combuster, readonly Entity combustee, readonly int duration) {
        base(combustee, duration);
        this.combuster = combuster;
    }

    /**
     * The combuster can be lava or a block that is on fire.
     * <p>
     * WARNING: block may be null.
     *
     * @return the Block that set the combustee alight.
     */
    public Block getCombuster() {
        return combuster;
    }
}
