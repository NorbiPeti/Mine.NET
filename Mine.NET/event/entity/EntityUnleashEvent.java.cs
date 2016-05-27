using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called immediately prior to an entity being unleashed.
     */
    public class EntityUnleashEventArgs : EntityEventArgs<Entity>
    {
        private readonly UnleashReason reason;

        public EntityUnleashEventArgs(Entity entity, UnleashReason reason) : base(entity)
        {
            this.reason = reason;
        }

        /**
         * Returns the reason for the unleashing.
         *
         * @return The reason
         */
        public UnleashReason getReason()
        {
            return reason;
        }

        public enum UnleashReason
        {
            /**
             * When the entity's leashholder has died or logged out, and so is
             * unleashed
             */
            HOLDER_GONE,
            /**
             * When the entity's leashholder attempts to unleash it
             */
            PLAYER_UNLEASH,
            /**
             * When the entity's leashholder is more than 10 blocks away
             */
            DISTANCE,
            UNKNOWN
        }
    }
}
