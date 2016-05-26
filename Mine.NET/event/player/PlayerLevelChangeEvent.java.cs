using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a players level changes
     */
    public class PlayerLevelChangeEventArgs : PlayerEventArgs
    {
        private readonly int oldLevel;
        private readonly int newLevel;

        public PlayerLevelChangeEventArgs(Player player, int oldLevel, int newLevel) : base(player)
        {
            this.oldLevel = oldLevel;
            this.newLevel = newLevel;
        }

        /**
         * Gets the old level of the player
         *
         * @return The old level of the player
         */
        public int getOldLevel()
        {
            return oldLevel;
        }

        /**
         * Gets the new level of the player
         *
         * @return The new (current) level of the player
         */
        public int getNewLevel()
        {
            return newLevel;
        }
    }
}
