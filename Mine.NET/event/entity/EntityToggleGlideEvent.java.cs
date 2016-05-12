using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
/**
 * Sent when an entity's gliding status is toggled with an Elytra.
 * Examples of when this event would be called:
 * <ul>
 *     <li>Player presses the jump key while in midair and using an Elytra</li>
 *     <li>Player lands on ground while they are gliding (with an Elytra)</li>
 * </ul>
 * This can be visually estimated by the animation in which a player turns horizontal.
 */
public class EntityToggleGlideEvent : EntityEvent, Cancellable {
    private static readonly HandlerList handlers = new HandlerList();

    private bool cancel = false;

        public EntityToggleGlideEvent(LivingEntity who, bool isGliding) : base(who)
        {
            this.isGliding = isGliding;
        }

    public override bool isCancelled() {
        return cancel;
    }

    public override void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public bool isGliding { get; private set; }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

}
}
