using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player toggles their sprinting state
     */
    public class PlayerToggleSprintEventArgs : PlayerEventArgs, Cancellable
    {
        private bool cancel = false;

        public PlayerToggleSprintEventArgs(Player player, bool isSprinting) : base(player)
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
    }
}
