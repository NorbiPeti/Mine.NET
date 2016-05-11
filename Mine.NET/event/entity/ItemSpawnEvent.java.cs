package org.bukkit.event.entity;

import org.bukkit.entity.Item;
import org.bukkit.Location;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when an item is spawned into a world
 */
public class ItemSpawnEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Location location;
    private bool canceled;

    public ItemSpawnEvent(Item spawnee, readonly Location loc) {
        super(spawnee);
        this.location = loc;
    }

    public bool isCancelled() {
        return canceled;
    }

    public void setCancelled(bool cancel) {
        canceled = cancel;
    }

    @Override
    public Item getEntity() {
        return (Item) entity;
    }

    /**
     * Gets the location at which the item is spawning.
     *
     * @return The location at which the item is spawning
     */
    public Location getLocation() {
        return location;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
