using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when the GameMode of the player is changed.
     */
    public class PlayerGameModeChangeEventArgs : PlayerEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly GameMode newGameMode;

        public PlayerGameModeChangeEventArgs(Player player, GameMode newGameMode) : base(player)
        {
            this.newGameMode = newGameMode;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }

        /**
         * Gets the GameMode the player is switched to.
         *
         * @return  player's new GameMode
         */
        public GameMode getNewGameMode()
        {
            return newGameMode;
        }
    }
}
