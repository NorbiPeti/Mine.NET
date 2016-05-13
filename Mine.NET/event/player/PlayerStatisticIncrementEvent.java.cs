using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player statistic is incremented.
     * <p>
     * This event is not called for {@link org.bukkit.Statistic#PLAY_ONE_TICK} or
     * movement based statistics.
     *
     */
    public class PlayerStatisticIncrementEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        protected readonly Statistic statistic;
        private readonly int initialValue;
        private readonly int newValue;
        private bool iscancelled = false;
        private readonly EntityType entityType;
        private readonly Material material;

        public PlayerStatisticIncrementEvent(Player player, Statistic statistic, int initialValue, int newValue) : base(player)
        {
            this.statistic = statistic;
            this.initialValue = initialValue;
            this.newValue = newValue;
            this.entityType = null;
            this.material = null;
        }

        public PlayerStatisticIncrementEvent(Player player, Statistic statistic, int initialValue, int newValue, EntityType entityType) : base(player)
        {
            this.statistic = statistic;
            this.initialValue = initialValue;
            this.newValue = newValue;
            this.entityType = entityType;
            this.material = null;
        }

        public PlayerStatisticIncrementEvent(Player player, Statistic statistic, int initialValue, int newValue, Material material) : base(player)
        {
            this.statistic = statistic;
            this.initialValue = initialValue;
            this.newValue = newValue;
            this.entityType = null;
            this.material = material;
        }

        /**
         * Gets the statistic that is being incremented.
         *
         * @return the incremented statistic
         */
        public Statistic getStatistic()
        {
            return statistic;
        }

        /**
         * Gets the previous value of the statistic.
         *
         * @return the previous value of the statistic
         */
        public int getPreviousValue()
        {
            return initialValue;
        }

        /**
         * Gets the new value of the statistic.
         *
         * @return the new value of the statistic
         */
        public int getNewValue()
        {
            return newValue;
        }

        /**
         * Gets the EntityType if {@link #getStatistic() getStatistic()} is an
         * entity statistic otherwise returns null.
         *
         * @return the EntityType of the statistic
         */
        public EntityType getEntityType()
        {
            return entityType;
        }

        /**
         * Gets the Material if {@link #getStatistic() getStatistic()} is a block
         * or item statistic otherwise returns null.
         *
         * @return the Material of the statistic
         */
        public Material getMaterial()
        {
            return material;
        }

        public bool isCancelled()
        {
            return iscancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.iscancelled = cancel;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
