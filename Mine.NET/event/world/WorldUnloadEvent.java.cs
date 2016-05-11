package org.bukkit.event.world;

import org.bukkit.World;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a World is unloaded
 */
public class WorldUnloadEvent extends WorldEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool isCancelled;

    public WorldUnloadEvent(World world) {
        super(world);
    }

    public bool isCancelled() {
        return this.isCancelled;
    }

    public void setCancelled(bool cancel) {
        this.isCancelled = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
