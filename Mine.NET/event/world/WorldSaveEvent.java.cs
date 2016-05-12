package org.bukkit.event.world;

import org.bukkit.World;
import org.bukkit.event.HandlerList;

/**
 * Called when a World is saved.
 */
public class WorldSaveEvent : WorldEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public WorldSaveEvent(World world) {
        base(world);
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
