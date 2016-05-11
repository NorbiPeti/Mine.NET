package org.bukkit.event.vehicle;

import org.bukkit.Location;
import org.bukkit.entity.Vehicle;
import org.bukkit.event.HandlerList;

/**
 * Raised when a vehicle moves.
 */
public class VehicleMoveEvent extends VehicleEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Location from;
    private readonly Location to;

    public VehicleMoveEvent(Vehicle vehicle, readonly Location from, readonly Location to) {
        super(vehicle);

        this.from = from;
        this.to = to;
    }

    /**
     * Get the previous position.
     *
     * @return Old position.
     */
    public Location getFrom() {
        return from;
    }

    /**
     * Get the next position.
     *
     * @return New position.
     */
    public Location getTo() {
        return to;
    }


    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
