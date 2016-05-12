package org.bukkit.event.entity;

import org.bukkit.entity.LivingEntity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Sent when an entity's gliding status is toggled with an Elytra.
 * Examples of when this event would be called:
 * <ul>
 *     <li>Player presses the jump key while in midair and using an Elytra</li>
 *     <li>Player lands on ground while they are gliding (with an Elytra)</li>
 * </ul>
 * This can be visually estimated by the animation in which a player turns horizontal.
 */
public class EntityToggleGlideEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();

    private bool cancel = false;
    private readonly bool isGliding;

    public EntityToggleGlideEvent(LivingEntity who, readonly bool isGliding) {
        base(who);
        this.isGliding = isGliding;
    }

    public override bool isCancelled() {
        return cancel;
    }

    public override void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public bool isGliding() {
        return isGliding;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

}
