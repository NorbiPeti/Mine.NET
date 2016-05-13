using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player toggles their sprinting state
     */
    public class PlayerToggleSprintEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel = false;

        public PlayerToggleSprintEvent(Player player, bool isSprinting) : base(player)
        {
            this.isSprinting = isSprinting;
        }

        /**
         * Gets whether the player is now sprinting or not.
         *
         * @return sprinting state
         */
        public bool isSprinting { get; private set; }

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
