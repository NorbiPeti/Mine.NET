using Mine.NET.entity;
using Mine.NET.Event.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called prior to an entity being unleashed due to a player's action.
     */
    public class PlayerUnleashEntityEventArgs : EntityUnleashEventArgs, Cancellable
    {
        private readonly Player player;
        private bool cancelled = false;

        public PlayerUnleashEntityEventArgs(Entity entity, Player player) :
            base(entity, UnleashReason.PLAYER_UNLEASH)
        {
            this.player = player;
        }

        /**
         * Returns the player who is unleashing the entity.
         *
         * @return The player
         */
        public Player getPlayer()
        {
            return player;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }
    }
}
