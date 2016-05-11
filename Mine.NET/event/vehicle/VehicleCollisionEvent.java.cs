package org.bukkit.event.vehicle;

import org.bukkit.entity.Vehicle;

/**
 * Raised when a vehicle collides.
 */
public abstract class VehicleCollisionEvent : VehicleEvent {
    public VehicleCollisionEvent(Vehicle vehicle) {
        base(vehicle);
    }
}
