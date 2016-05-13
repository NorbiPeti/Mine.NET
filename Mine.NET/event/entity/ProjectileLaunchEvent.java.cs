using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a projectile is launched.
     */
    public class ProjectileLaunchEvent : EntityEvent<Projectile>, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;

        public ProjectileLaunchEvent(Entity what) : base(what)
        {
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            cancelled = cancel;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
