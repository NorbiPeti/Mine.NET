namespace Mine.NET.event.vehicle;

import org.bukkit.entity.Vehicle;
import org.bukkit.event.HandlerList;

/**
 * Raised when a vehicle is created.
 */
public class VehicleCreateEvent : VehicleEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public VehicleCreateEvent(Vehicle vehicle) {
        base(vehicle);
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
