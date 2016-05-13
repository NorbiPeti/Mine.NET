using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a splash potion hits an area
     */
    public class LingeringPotionSplashEvent : ProjectileHitEvent<LingeringPotion>, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;

        public LingeringPotionSplashEvent(ThrownPotion potion, AreaEffectCloud entity) : base(potion)
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

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public new static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
