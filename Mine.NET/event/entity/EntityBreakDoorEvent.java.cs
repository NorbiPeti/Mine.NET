package org.bukkit.event.entity;

import org.bukkit.Material;
import org.bukkit.block.Block;
import org.bukkit.entity.Entity;
import org.bukkit.entity.LivingEntity;

/**
 * Called when an {@link Entity} breaks a door
 * <p>
 * Cancelling the event will cause the event to be delayed
 */
public class EntityBreakDoorEvent : EntityChangeBlockEvent {
    public EntityBreakDoorEvent(LivingEntity entity, readonly Block targetBlock) {
        base(entity, targetBlock, Material.AIR, (byte) 0);
    }

    @Override
    public LivingEntity getEntity() {
        return (LivingEntity) entity;
    }
}
