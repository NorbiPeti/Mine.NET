package org.bukkit.event.entity;

import org.apache.commons.lang.Validate;
import org.bukkit.entity.EnderDragon;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when an EnderDragon switches controller phase.
 */
public class EnderDragonChangePhaseEvent : EntityEvent : Cancellable {

    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel;
    private readonly EnderDragon.Phase currentPhase;
    private EnderDragon.Phase newPhase;

    public EnderDragonChangePhaseEvent(EnderDragon enderDragon, EnderDragon.Phase currentPhase, EnderDragon.Phase newPhase) {
        super(enderDragon);
        this.currentPhase = currentPhase;
        this.setNewPhase(newPhase);
    }

    @Override
    public EnderDragon getEntity() {
        return (EnderDragon) entity;
    }

    /**
     * Gets the current phase that the dragon is in. This method will return null 
     * when a dragon is first spawned and hasn't yet been assigned a phase.
     * 
     * @return the current dragon phase
     */
    public EnderDragon.Phase getCurrentPhase() {
        return currentPhase;
    }

    /**
     * Gets the new phase that the dragon will switch to.
     * 
     * @return the new dragon phase
     */
    public EnderDragon.Phase getNewPhase() {
        return newPhase;
    }

    /**
     * Sets the new phase for the ender dragon.
     * 
     * @param newPhase the new dragon phase
     */
    public void setNewPhase(EnderDragon.Phase newPhase) {
        if(newPhase==null) throw new ArgumentNullException("New dragon phase cannot be null");
        this.newPhase = newPhase;
    }

    @Override
    public bool isCancelled() {
        return cancel;
    }

    @Override
    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
