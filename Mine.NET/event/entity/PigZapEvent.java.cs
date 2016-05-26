using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Stores data for pigs being zapped
     */
    public class PigZapEventArgs : EntityEventArgs<Pig>, Cancellable
    {
        private bool canceled;
        private readonly PigZombie pigzombie;
        private readonly LightningStrike bolt;

        public PigZapEventArgs(Pig pig, LightningStrike bolt, PigZombie pigzombie) : base(pig)
        {
            this.bolt = bolt;
            this.pigzombie = pigzombie;
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
         * Gets the bolt which is striking the pig.
         *
         * @return lightning entity
         */
        public LightningStrike getLightning()
        {
            return bolt;
        }

        /**
         * Gets the zombie pig that will replace the pig, provided the event is
         * not cancelled first.
         *
         * @return resulting entity
         */
        public PigZombie getPigZombie()
        {
            return pigzombie;
        }
    }
}
