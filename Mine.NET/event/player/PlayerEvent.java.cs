using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Represents a player related event
     */
    public abstract class PlayerEventArgs : GameEventArgs
    {
        protected Player player;

        public PlayerEventArgs(Player who)
        {
            player = who;
        }

        protected PlayerEventArgs(Player who, bool async) : base(async)
        {
            player = who;

        }

        /**
         * Returns the player involved in this event
         *
         * @return Player who is involved in this event
         */
        public Player getPlayer()
        {
            return player;
        }
    }
}
