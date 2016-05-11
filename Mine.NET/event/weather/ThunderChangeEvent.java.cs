package org.bukkit.event.weather;

import org.bukkit.World;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Stores data for thunder state changing in a world
 */
public class ThunderChangeEvent : WeatherEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool canceled;
    private readonly bool to;

    public ThunderChangeEvent(World world, readonly bool to) {
        base(world);
        this.to = to;
    }

    public bool isCancelled() {
        return canceled;
    }

    public void setCancelled(bool cancel) {
        canceled = cancel;
    }

    /**
     * Gets the state of thunder that the world is being set to
     *
     * @return true if the weather is being set to thundering, false otherwise
     */
    public bool toThunderState() {
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
