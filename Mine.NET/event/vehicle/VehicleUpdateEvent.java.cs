package org.bukkit.event.vehicle;

import org.bukkit.entity.Vehicle;
import org.bukkit.event.HandlerList;

/**
 * Called when a vehicle updates
 */
public class VehicleUpdateEvent : VehicleEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public VehicleUpdateEvent(Vehicle vehicle) {
        super(vehicle);
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
