package org.bukkit.event.entity;

import org.bukkit.entity.Entity;
import org.bukkit.entity.LivingEntity;

/**
 * Called when an Entity targets a {@link LivingEntity} and can only target
 * LivingEntity's.
 */
public class EntityTargetLivingEntityEvent : EntityTargetEvent{
    public EntityTargetLivingEntityEvent(Entity entity, readonly LivingEntity target, readonly TargetReason reason) {
        base(entity, target, reason);
    }

    public LivingEntity getTarget() {
        return (LivingEntity) base.getTarget();
    }

    /**
     * Set the Entity that you want the mob to target.
     * <p>
     * It is possible to be null, null will cause the entity to be
     * target-less.
     * <p>
     * Must be a LivingEntity, or null.
     *
     * @param target The entity to target
     */
    public void setTarget(Entity target) {
        if (target == null || target is LivingEntity) {
            base.setTarget(target);
        }
    }
}
