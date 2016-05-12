namespace Mine.NET.event.entity;

import org.bukkit.entity.Projectile;
import org.bukkit.event.HandlerList;

/**
 * Called when a projectile hits an object
 */
public class ProjectileHitEvent : EntityEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public ProjectileHitEvent(Projectile projectile) {
        base(projectile);
    }

    public override Projectile getEntity() {
        return (Projectile) entity;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

}
