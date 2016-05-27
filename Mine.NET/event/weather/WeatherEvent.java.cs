namespace Mine.NET.Event.weather
{
    /**
     * Represents a Weather-related event
     */
    public abstract class WeatherEventArgs : GameEventArgs
    {
        protected World world;

        public WeatherEventArgs(World where)
        {
            world = where;
        }

        /**
         * Returns the World where this event is occurring
         *
         * @return World this event is occurring in
         */
        public World getWorld()
        {
            return world;
        }
    }
}
