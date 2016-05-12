package org.bukkit.event.world;

import org.bukkit.Chunk;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a chunk is unloaded
 */
public class ChunkUnloadEvent : ChunkEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel = false;

    public ChunkUnloadEvent(Chunk chunk) {
        base(chunk);
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
