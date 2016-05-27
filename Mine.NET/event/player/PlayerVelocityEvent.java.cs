using Mine.NET.entity;
using Mine.NET.util;

namespace Mine.NET.Event.player
{
    /**
     * Called when the velocity of a player changes.
     */
    public class PlayerVelocityEventArgs : PlayerEventArgs, Cancellable
    {
        private bool cancel = false;
        private Vector velocity;

        public PlayerVelocityEventArgs(Player player, Vector velocity) : base(player)
        {
            this.velocity = velocity;
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
         * Gets the velocity vector that will be sent to the player
         *
         * @return Vector the player will get
         */
        public Vector getVelocity()
        {
            return velocity;
        }

        /**
         * Sets the velocity vector that will be sent to the player
         *
         * @param velocity The velocity vector that will be sent to the player
         */
        public void setVelocity(Vector velocity)
        {
            this.velocity = velocity;
        }
    }
}
