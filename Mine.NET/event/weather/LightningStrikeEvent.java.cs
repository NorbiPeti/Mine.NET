using Mine.NET.entity;

namespace Mine.NET.Event.weather
{
    /**
     * Stores data for lightning striking
     */
    public class LightningStrikeEvent : WeatherEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool canceled;
        private readonly LightningStrike bolt;

        public LightningStrikeEvent(World world, LightningStrike bolt) : base(world)
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
