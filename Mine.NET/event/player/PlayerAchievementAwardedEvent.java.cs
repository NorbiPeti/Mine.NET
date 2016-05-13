using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player earns an achievement.
     */
    public class PlayerAchievementAwardedEvent : PlayerEvent, Cancellable {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Achievement achievement;
        private bool iscancelled = false;

        public PlayerAchievementAwardedEvent(Player player, Achievement achievement) : base(player)
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

        public override HandlerList getHandlers() {
            return handlers;
        }

        public static HandlerList getHandlerList() {
            return handlers;
        }
    }
}
