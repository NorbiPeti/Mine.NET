package org.bukkit.event.entity;

import org.bukkit.Location;
import org.bukkit.entity.Entity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Thrown when a non-player entity (such as an Enderman) tries to teleport
 * from one location to another.
 */
public class EntityTeleportEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel;
    private Location from;
    private Location to;

    public EntityTeleportEvent(Entity what, Location from, Location to) {
        base(what);
        this.from = from;
        this.to = to;
        this.cancel = false;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Gets the location that this entity moved from
     *
     * @return Location this entity moved from
     */
    public Location getFrom() {
        return from;
    }

    /**
     * Sets the location that this entity moved from
     *
     * @param from New location this entity moved from
     */
    public void setFrom(Location from) {
        this.from = from;
    }

    /**
     * Gets the location that this entity moved to
     *
     * @return Location the entity moved to
     */
    public Location getTo() {
        return to;
    }

    /**
     * Sets the location that this entity moved to
     *
     * @param to New Location this entity moved to
     */
    public void setTo(Location to) {
        this.to = to;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}