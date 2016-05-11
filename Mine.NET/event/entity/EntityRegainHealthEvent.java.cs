package org.bukkit.event.entity;

import org.bukkit.entity.Entity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;
import org.bukkit.util.NumberConversions;

/**
 * Stores data for health-regain events
 */
public class EntityRegainHealthEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private double amount;
    private readonly RegainReason regainReason;

    [Obsolete]
    public EntityRegainHealthEvent(Entity entity, readonly int amount, readonly RegainReason regainReason) {
        this(entity, (double) amount, regainReason);
    }

    public EntityRegainHealthEvent(Entity entity, readonly double amount, readonly RegainReason regainReason) {
        super(entity);
        this.amount = amount;
        this.regainReason = regainReason;
    }

    /**
     * Gets the amount of regained health
     *
     * @return The amount of health regained
     */
    public double getAmount() {
        return amount;
    }

    /**
     * This method exists for legacy reasons to provide backwards
     * compatibility. It will not exist at runtime and should not be used
     * under any circumstances.
     * 
     * @return the (rounded) amount regained
     */
    [Obsolete]
    public int _INVALID_getAmount() {
        return NumberConversions.ceil(getAmount());
    }

    /**
     * Sets the amount of regained health
     *
     * @param amount the amount of health the entity will regain
     */
    public void setAmount(double amount) {
        this.amount = amount;
    }

    /**
     * This method exists for legacy reasons to provide backwards
     * compatibility. It will not exist at runtime and should not be used
     * under any circumstances.
     * 
     * @param amount the amount that will be regained
     */
    [Obsolete]
    public void _INVALID_setAmount(int amount) {
        setAmount(amount);
    }

    @Override
    public bool isCancelled() {
        return cancelled;
    }

    @Override
    public void setCancelled(bool cancel) {
        cancelled = cancel;
    }

    /**
     * Gets the reason for why the entity is regaining health
     *
     * @return A RegainReason detailing the reason for the entity regaining
     *     health
     */
    public RegainReason getRegainReason() {
        return regainReason;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

    /**
     * An enum to specify the type of health regaining that is occurring
     */
    public enum RegainReason {

        /**
         * When a player regains health from regenerating due to Peaceful mode
         * (difficulty=0)
         */
        REGEN,
        /**
         * When a player regains health from regenerating due to their hunger
         * being satisfied
         */
        SATIATED,
        /**
         * When a player regains health from eating consumables
         */
        EATING,
        /**
         * When an ender dragon regains health from an ender crystal
         */
        ENDER_CRYSTAL,
        /**
         * When a player is healed by a potion or spell
         */
        MAGIC,
        /**
         * When a player is healed over time by a potion or spell
         */
        MAGIC_REGEN,
        /**
         * When a wither is filling its health during spawning
         */
        WITHER_SPAWN,
        /**
         * When an entity is damaged by the Wither potion effect
         */
        WITHER,
        /**
         * Any other reason not covered by the reasons above
         */
        CUSTOM
    }
}
