using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player earns an achievement.
     */
    public class PlayerAchievementAwardedEventArgs : PlayerEventArgs, Cancellable {
        private readonly Achievement achievement;
        private bool iscancelled = false;

        public PlayerAchievementAwardedEventArgs(Player player, Achievement achievement) : base(player)
        {
            this.achievement = achievement;
        }

        /**
         * Gets the Achievement being awarded.
         *
         * @return the achievement being awarded
         */
        public Achievement getAchievement() {
            return achievement;
        }

        public bool isCancelled() {
            return iscancelled;
        }

        public void setCancelled(bool cancel) {
            this.iscancelled = cancel;
        }
    }
}
