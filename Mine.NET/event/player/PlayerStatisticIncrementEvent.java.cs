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
    public class PlayerStatisticIncrementEventArgs : PlayerEventArgs, Cancellable
    {
        protected readonly Statistic statistic;
        private readonly int initialValue;
        private readonly int newValue;
        private bool iscancelled = false;
        private readonly EntityTypes entityType;
        private readonly Materials material;

        public PlayerStatisticIncrementEventArgs(Player player, Statistic statistic, int initialValue, int newValue) : base(player)
        {
            this.statistic = statistic;
            this.initialValue = initialValue;
            this.newValue = newValue;
        }

        public PlayerStatisticIncrementEventArgs(Player player, Statistic statistic, int initialValue, int newValue, EntityTypes entityType) : base(player)
        {
            this.statistic = statistic;
            this.initialValue = initialValue;
            this.newValue = newValue;
            this.entityType = entityType;
        }

        public PlayerStatisticIncrementEventArgs(Player player, Statistic statistic, int initialValue, int newValue, Materials Materials) : base(player)
        {
            this.statistic = statistic;
            this.initialValue = initialValue;
            this.newValue = newValue;
            this.material = Materials;
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
        public EntityTypes getEntityType()
        {
            return entityType;
        }

        /**
         * Gets the Materials if {@link #getStatistic() getStatistic()} is a block
         * or item statistic otherwise returns null.
         *
         * @return the Materials of the statistic
         */
        public Materials getMaterial()
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
    }
}
