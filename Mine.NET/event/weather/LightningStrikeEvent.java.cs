using Mine.NET.entity;

namespace Mine.NET.Event.weather
{
    /**
     * Stores data for lightning striking
     */
    public class LightningStrikeEventArgs : WeatherEventArgs, Cancellable
    {
        private bool canceled;
        private readonly LightningStrike bolt;

        public LightningStrikeEventArgs(World world, LightningStrike bolt) : base(world)
        {
            this.bolt = bolt;
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
         * Gets the bolt which is striking the earth.
         *
         * @return lightning entity
         */
        public LightningStrike getLightning()
        {
            return bolt;
        }
    }
}
