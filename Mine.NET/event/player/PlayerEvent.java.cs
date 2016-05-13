using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Represents a player related event
     */
    public abstract class PlayerEvent : Event
    {
        protected Player player;

        public PlayerEvent(Player who)
        {
            player = who;
        }

        protected PlayerEvent(Player who, bool async) : base(async)
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
