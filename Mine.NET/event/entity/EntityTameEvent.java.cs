namespace Mine.NET.event.entity;

import org.bukkit.entity.AnimalTamer;
import org.bukkit.entity.LivingEntity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Thrown when a LivingEntity is tamed
 */
public class EntityTameEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private readonly AnimalTamer owner;

    public EntityTameEvent(LivingEntity entity, readonly AnimalTamer owner) {
        base(entity);
        this.owner = owner;
    }

    public override LivingEntity getEntity() {
        return (LivingEntity) entity;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        cancelled = cancel;
    }

    /**
     * Gets the owning AnimalTamer
     *
     * @return the owning AnimalTamer
     */
    public AnimalTamer getOwner() {
        return owner;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
