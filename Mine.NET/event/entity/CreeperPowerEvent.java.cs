using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a Creeper is struck by lightning.
     * <p>
     * If a Creeper Power event is cancelled, the Creeper will not be powered.
     */
    public class CreeperPowerEventArgs : EntityEventArgs<Creeper>, Cancellable
    {
        private bool canceled;
        private readonly PowerCause cause;
        private LightningStrike bolt;

        public CreeperPowerEventArgs(Creeper creeper, LightningStrike bolt, PowerCause cause) : this(creeper, cause)
        {
            this.bolt = bolt;
        }

        public CreeperPowerEventArgs(Creeper creeper, PowerCause cause) : base(creeper)
        {

            this.cause = cause;
        }

        public bool isCancelled()
        {
            return canceled;
        }

        public void setCancelled(bool cancel)
        {
            canceled = cancel;
        }

        /**
         * Gets the lightning bolt which is striking the Creeper.
         *
         * @return The Entity for the lightning bolt which is striking the Creeper
         */
        public LightningStrike getLightning()
        {
            return bolt;
        }

        /**
         * Gets the cause of the creeper being (un)powered.
         *
         * @return A PowerCause value detailing the cause of change in power.
         */
        public PowerCause getCause()
        {
            return cause;
        }

        /**
         * An enum to specify the cause of the change in power
         */
        public enum PowerCause
        {

            /**
             * Power change caused by a lightning bolt
             * <p>
             * Powered state: true
             */
            LIGHTNING,
            /**
             * Power change caused by something else (probably a plugin)
             * <p>
             * Powered state: true
             */
            SET_ON,
            /**
             * Power change caused by something else (probably a plugin)
             * <p>
             * Powered state: false
             */
            SET_OFF
        }
    }
}
