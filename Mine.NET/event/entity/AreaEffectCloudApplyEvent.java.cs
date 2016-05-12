package org.bukkit.event.entity;

import java.util.List;
import java.util.Collection;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.Validate;
import org.bukkit.entity.AreaEffectCloud;
import org.bukkit.entity.LivingEntity;
import org.bukkit.entity.ThrownPotion;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a lingering potion applies it's effects. Happens
 * once every 5 ticks
 */
public class AreaEffectCloudApplyEvent : EntityEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly List<LivingEntity> affectedEntities;

    public AreaEffectCloudApplyEvent(AreaEffectCloud entity, readonly List<LivingEntity> affectedEntities) {
        base(entity);
        this.affectedEntities = affectedEntities;
    }

    public override AreaEffectCloud getEntity() {
        return (AreaEffectCloud) entity;
    }

    /**
     * Retrieves a mutable list of the effected entities
     * <p>
     * It is important to note that not every entity in this list
     * is guaranteed to be effected.  The cloud may die during the
     * application of its effects due to the depletion of {@link AreaEffectCloud#getDurationOnUse()}
     * or {@link AreaEffectCloud#getRadiusOnUse()}
     *
     * @return the affected entity list
     */
    public List<LivingEntity> getAffectedEntities() {
        return affectedEntities;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
