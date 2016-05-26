using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a projectile hits an object
     */
    public class ProjectileHitEventArgs<T> : EntityEventArgs<T> where T : Entity
    {
        public ProjectileHitEventArgs(Projectile projectile) : base(projectile)
        {
        }
    }
}
