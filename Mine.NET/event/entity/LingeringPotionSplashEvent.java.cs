namespace Mine.NET.event.entity;

import java.util.List;
import java.util.Collection;
import java.util.Map;

import org.apache.commons.lang.Validate;
import org.bukkit.entity.AreaEffectCloud;
import org.bukkit.entity.LingeringPotion;
import org.bukkit.entity.LivingEntity;
import org.bukkit.entity.ThrownPotion;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a splash potion hits an area
 */
public class LingeringPotionSplashEvent : ProjectileHitEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private readonly AreaEffectCloud entity;

    public LingeringPotionSplashEvent(ThrownPotion potion, readonly AreaEffectCloud entity) {
        base(potion);
        this.entity = entity;
    }

    public override LingeringPotion getEntity() {
        return (LingeringPotion) base.getEntity();
    }

    /**
     * Gets the AreaEffectCloud spawned
     *
     * @return The spawned AreaEffectCloud
     */
    public AreaEffectCloud getAreaEffectCloud() {
        return entity;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        cancelled = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
