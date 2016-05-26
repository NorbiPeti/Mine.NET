using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a projectile is launched.
     */
    public class ProjectileLaunchEventArgs : EntityEventArgs<Projectile>, Cancellable
    {
        private bool cancelled;

        public ProjectileLaunchEventArgs(Entity what) : base(what)
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
    }
}
