package org.bukkit.event.vehicle;

import org.bukkit.entity.Vehicle;
import org.bukkit.event.Event;

/**
 * Represents a vehicle-related event.
 */
public abstract class VehicleEvent : Event {
    protected Vehicle vehicle;

    public VehicleEvent(Vehicle vehicle) {
        this.vehicle = vehicle;
    }

    /**
     * Get the vehicle.
     *
     * @return the vehicle
     */
    public readonly Vehicle getVehicle() {
        return vehicle;
    }
}
