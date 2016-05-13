using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player toggles their flying state
     */
    public class PlayerToggleFlightEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel = false;

        public PlayerToggleFlightEvent(Player player, bool isFlying) : base(player)
        {
            this.isFlying = isFlying;
        }

        /**
         * Returns whether the player is trying to start or stop flying.
         *
         * @return flying state
         */
        public bool isFlying { get; private set; }

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
