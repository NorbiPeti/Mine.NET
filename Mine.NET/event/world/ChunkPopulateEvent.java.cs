namespace Mine.NET.event.world;

import org.bukkit.Chunk;
import org.bukkit.event.HandlerList;
import org.bukkit.generator.BlockPopulator;

/**
 * Thrown when a new chunk has finished being populated.
 * <p>
 * If your intent is to populate the chunk using this event, please see {@link
 * BlockPopulator}
 */
public class ChunkPopulateEvent : ChunkEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public ChunkPopulateEvent(Chunk chunk) {
        base(chunk);
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
