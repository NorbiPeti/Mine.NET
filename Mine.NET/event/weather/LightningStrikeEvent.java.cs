package org.bukkit.event.weather;

import org.bukkit.World;
import org.bukkit.entity.LightningStrike;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Stores data for lightning striking
 */
public class LightningStrikeEvent : WeatherEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool canceled;
    private readonly LightningStrike bolt;

    public LightningStrikeEvent(World world, readonly LightningStrike bolt) {
        super(world);
        this.bolt = bolt;
    }

    public bool isCancelled() {
        return canceled;
    }

    public void setCancelled(bool cancel) {
        canceled = cancel;
    }

    /**
     * Gets the bolt which is striking the earth.
     *
     * @return lightning entity
     */
    public LightningStrike getLightning() {
        return bolt;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
