using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player switches to another world.
     */
    public class PlayerChangedWorldEventArgs : PlayerEventArgs
    {
        private readonly World from;

        public PlayerChangedWorldEventArgs(Player player, World from) : base(player)
        {
            this.from = from;
        }

        /**
         * Gets the world the player is switching from.
         *
         * @return  player's previous world
         */
        public World getFrom()
        {
            return from;
        }
    }
}
