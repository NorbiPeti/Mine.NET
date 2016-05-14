namespace Mine.NET.Event.weather
{
    /**
     * Stores data for weather changing in a world
     */
    public class WeatherChangeEvent : WeatherEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool canceled;
        private readonly bool to;

        public WeatherChangeEvent(World world, bool to) : base(world)
        {
            this.to = to;
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
         * Gets the state of weather that the world is being set to
         *
         * @return true if the weather is being set to raining, false otherwise
         */
        public bool toWeatherState()
        {
            return to;
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
