package org.bukkit.event.world;

import org.bukkit.World;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a World is unloaded
 */
public class WorldUnloadEvent : WorldEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool isCancelled;

    public WorldUnloadEvent(World world) {
        base(world);
    }

    public bool isCancelled() {
        return this.isCancelled;
    }

    public void setCancelled(bool cancel) {
        this.isCancelled = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
