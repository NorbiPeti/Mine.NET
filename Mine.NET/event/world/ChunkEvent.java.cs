package org.bukkit.event.world;

import org.bukkit.Chunk;

/**
 * Represents a Chunk related event
 */
public abstract class ChunkEvent : WorldEvent {
    protected Chunk chunk;

    protected ChunkEvent(Chunk chunk) {
        super(chunk.getWorld());
        this.chunk = chunk;
    }

    /**
     * Gets the chunk being loaded/unloaded
     *
     * @return Chunk that triggered this event
     */
    public Chunk getChunk() {
        return chunk;
    }
}
