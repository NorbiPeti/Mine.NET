package org.bukkit.event.vehicle;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Vehicle;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Raised when a vehicle is destroyed, which could be caused by either a
 * player or the environment. This is not raised if the boat is simply
 * 'removed' due to other means.
 */
public class VehicleDestroyEvent : VehicleEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Entity attacker;
    private bool cancelled;

    public VehicleDestroyEvent(Vehicle vehicle, readonly Entity attacker) {
        base(vehicle);
        this.attacker = attacker;
    }

    /**
     * Gets the Entity that has destroyed the vehicle, potentially null
     *
     * @return the Entity that has destroyed the vehicle, potentially null
     */
    public Entity getAttacker() {
        return attacker;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
