using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player toggles their sneaking state
     */
    public class PlayerToggleSneakEventArgs : PlayerEventArgs, Cancellable
    {
        private bool cancel = false;

        public PlayerToggleSneakEventArgs(Player player, bool isSneaking) : base(player)
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
    }
}
