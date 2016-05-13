using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a projectile hits an object
     */
    public class ProjectileHitEvent<T> : EntityEvent<T> where T : Entity
    {
        private static readonly HandlerList handlers = new HandlerList();

        public ProjectileHitEvent(Projectile projectile) : base(projectile)
        {
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
