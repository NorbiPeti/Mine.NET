package org.bukkit.event.vehicle;

import org.bukkit.block.Block;
import org.bukkit.entity.Vehicle;
import org.bukkit.event.HandlerList;

/**
 * Raised when a vehicle collides with a block.
 */
public class VehicleBlockCollisionEvent : VehicleCollisionEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Block block;

    public VehicleBlockCollisionEvent(Vehicle vehicle, readonly Block block) {
        base(vehicle);
        this.block = block;
    }

    /**
     * Gets the block the vehicle collided with
     *
     * @return the block the vehicle collided with
     */
    public Block getBlock() {
        return block;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
