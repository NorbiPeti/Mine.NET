package org.bukkit.event.vehicle;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Vehicle;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Raised when a vehicle collides with an entity.
 */
public class VehicleEntityCollisionEvent : VehicleCollisionEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Entity entity;
    private bool cancelled = false;
    private bool cancelledPickup = false;
    private bool cancelledCollision = false;

    public VehicleEntityCollisionEvent(Vehicle vehicle, readonly Entity entity) {
        super(vehicle);
        this.entity = entity;
    }

    public Entity getEntity() {
        return entity;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    public bool isPickupCancelled() {
        return cancelledPickup;
    }

    public void setPickupCancelled(bool cancel) {
        cancelledPickup = cancel;
    }

    public bool isCollisionCancelled() {
        return cancelledCollision;
    }

    public void setCollisionCancelled(bool cancel) {
        cancelledCollision = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
