package org.bukkit.event.entity;

import org.bukkit.entity.Entity;

/**
 * Called when an entity causes another entity to combust.
 */
public class EntityCombustByEntityEvent : EntityCombustEvent {
    private readonly Entity combuster;

    public EntityCombustByEntityEvent(Entity combuster, readonly Entity combustee, readonly int duration) {
        super(combustee, duration);
        this.combuster = combuster;
    }

    /**
     * Get the entity that caused the combustion event.
     *
     * @return the Entity that set the combustee alight.
     */
    public Entity getCombuster() {
        return combuster;
    }
}
