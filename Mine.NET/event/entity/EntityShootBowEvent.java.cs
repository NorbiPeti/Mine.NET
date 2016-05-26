using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a LivingEntity shoots a bow firing an arrow
     */
    public class EntityShootBowEventArgs : EntityEventArgs<LivingEntity>, Cancellable
    {
        private readonly ItemStack bow;
        private Entity projectile;
        private readonly float force;
        private bool cancelled;

        public EntityShootBowEventArgs(LivingEntity shooter, ItemStack bow, Projectile projectile, float force) :
                base(shooter)
        {
            this.bow = bow;
            this.projectile = projectile;
            this.force = force;
        }

        /**
         * Gets the bow ItemStack used to fire the arrow.
         *
         * @return the bow involved in this event
         */
        public ItemStack getBow()
        {
            return bow;
        }

        /**
         * Gets the projectile which will be launched by this event
         *
         * @return the launched projectile
         */
        public Entity getProjectile()
        {
            return projectile;
        }

        /**
         * Replaces the projectile which will be launched
         *
         * @param projectile the new projectile
         */
        public void setProjectile(Entity projectile)
        {
            this.projectile = projectile;
        }

        /**
         * Gets the force the arrow was launched with
         *
         * @return bow shooting force, up to 1.0
         */
        public float getForce()
        {
            return force;
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
