namespace Mine.NET.Event.weather
{
    /**
     * Stores data for weather changing in a world
     */
    public class WeatherChangeEventArgs : WeatherEventArgs, Cancellable
    {
        private bool canceled;
        private readonly bool to;

        public WeatherChangeEventArgs(World world, bool to) : base(world)
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
    }
}
