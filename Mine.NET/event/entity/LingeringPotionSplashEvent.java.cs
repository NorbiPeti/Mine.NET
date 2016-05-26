using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a splash potion hits an area
     */
    public class LingeringPotionSplashEventArgs : ProjectileHitEventArgs<LingeringPotion>, Cancellable
    {
        private bool cancelled;

        public LingeringPotionSplashEventArgs(ThrownPotion potion, AreaEffectCloud entity) : base(potion)
        {
            this.entity = entity;
        }

        /**
         * Gets the AreaEffectCloud spawned
         *
         * @return The spawned AreaEffectCloud
         */
        public AreaEffectCloud getAreaEffectCloud()
        {
            return (AreaEffectCloud)entity;
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
