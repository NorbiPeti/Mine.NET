using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Represents a player animation event
     */
    public class PlayerAnimationEventArgs : PlayerEventArgs, Cancellable {
        private readonly PlayerAnimationType animationType;
        private bool iscancelled = false;

        /**
         * Construct a new PlayerAnimation event
         *
         * @param player The player instance
         */
        public PlayerAnimationEventArgs(Player player) : base(player)
        {
            // Only supported animation type for now:
            animationType = PlayerAnimationType.ARM_SWING;
        }

        /**
         * Get the type of this animation event
         *
         * @return the animation type
         */
        public PlayerAnimationType getAnimationType() {
            return animationType;
        }

        public bool isCancelled() {
            return this.iscancelled;
        }

        public void setCancelled(bool cancel) {
            this.iscancelled = cancel;
        }
    }
}
