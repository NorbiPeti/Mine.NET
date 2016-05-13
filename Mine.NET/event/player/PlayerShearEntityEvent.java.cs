using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player shears an entity
     */
    public class PlayerShearEntityEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel;
        private readonly Entity what;

        public PlayerShearEntityEvent(Player who, Entity what) : base(who)
        {
            this.cancel = false;
            this.what = what;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Gets the entity the player is shearing
         *
         * @return the entity the player is shearing
         */
        public Entity getEntity()
        {
            return what;
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
