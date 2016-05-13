using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player toggles their sneaking state
     */
    public class PlayerToggleSneakEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel = false;

        public PlayerToggleSneakEvent(Player player, bool isSneaking) : base(player)
        {
            this.isSneaking = isSneaking;
        }

        /**
         * Returns whether the player is now sneaking or not.
         *
         * @return sneaking state
         */
        public bool isSneaking { get; private set; }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
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
