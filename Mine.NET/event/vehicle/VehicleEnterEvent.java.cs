package org.bukkit.event.vehicle;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Vehicle;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Raised when an entity enters a vehicle.
 */
public class VehicleEnterEvent extends VehicleEvent implements Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private readonly Entity entered;

    public VehicleEnterEvent(Vehicle vehicle, readonly Entity entered) {
        super(vehicle);
        this.entered = entered;
    }

    /**
     * Gets the Entity that entered the vehicle.
     *
     * @return the Entity that entered the vehicle
     */
    public Entity getEntered() {
        return entered;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
