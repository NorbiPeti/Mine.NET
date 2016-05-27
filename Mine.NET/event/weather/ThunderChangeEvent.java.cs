namespace Mine.NET.Event.weather
{
    /**
     * Stores data for thunder state changing in a world
     */
    public class ThunderChangeEventArgs : WeatherEventArgs, Cancellable
    {
        private bool canceled;
        private readonly bool to;

        public ThunderChangeEventArgs(World world, bool to) : base(world)
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
         * Gets the state of thunder that the world is being set to
         *
         * @return true if the weather is being set to thundering, false otherwise
         */
        public bool toThunderState()
        {
            return to;
        }
    }
}
    