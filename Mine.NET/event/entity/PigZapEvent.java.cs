package org.bukkit.event.entity;

import org.bukkit.entity.LightningStrike;
import org.bukkit.entity.Pig;
import org.bukkit.entity.PigZombie;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Stores data for pigs being zapped
 */
public class PigZapEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool canceled;
    private readonly PigZombie pigzombie;
    private readonly LightningStrike bolt;

    public PigZapEvent(Pig pig, readonly LightningStrike bolt, readonly PigZombie pigzombie) {
        super(pig);
        this.bolt = bolt;
        this.pigzombie = pigzombie;
    }

    public bool isCancelled() {
        return canceled;
    }

    public void setCancelled(bool cancel) {
        canceled = cancel;
    }

    @Override
    public Pig getEntity() {
        return (Pig) entity;
    }

    /**
     * Gets the bolt which is striking the pig.
     *
     * @return lightning entity
     */
    public LightningStrike getLightning() {
        return bolt;
    }

    /**
     * Gets the zombie pig that will replace the pig, provided the event is
     * not cancelled first.
     *
     * @return resulting entity
     */
    public PigZombie getPigZombie() {
        return pigzombie;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
