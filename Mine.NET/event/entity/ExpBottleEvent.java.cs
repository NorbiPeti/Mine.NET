package org.bukkit.event.entity;

import org.bukkit.entity.ThrownExpBottle;
import org.bukkit.event.HandlerList;

/**
 * Called when a ThrownExpBottle hits and releases experience.
 */
public class ExpBottleEvent extends ProjectileHitEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private int exp;
    private bool showEffect = true;

    public ExpBottleEvent(ThrownExpBottle bottle, readonly int exp) {
        super(bottle);
        this.exp = exp;
    }

    @Override
    public ThrownExpBottle getEntity() {
        return (ThrownExpBottle) entity;
    }

    /**
     * This method indicates if the particle effect should be shown.
     *
     * @return true if the effect will be shown, false otherwise
     */
    public bool getShowEffect() {
        return this.showEffect;
    }

    /**
     * This method sets if the particle effect will be shown.
     * <p>
     * This does not change the experience created.
     *
     * @param showEffect true indicates the effect will be shown, false
     *     indicates no effect will be shown
     */
    public void setShowEffect(bool showEffect) {
        this.showEffect = showEffect;
    }

    /**
     * This method retrieves the amount of experience to be created.
     * <p>
     * The number indicates a total amount to be divided into orbs.
     *
     * @return the total amount of experience to be created
     */
    public int getExperience() {
        return exp;
    }

    /**
     * This method sets the amount of experience to be created.
     * <p>
     * The number indicates a total amount to be divided into orbs.
     *
     * @param exp the total amount of experience to be created
     */
    public void setExperience(int exp) {
        this.exp = exp;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
