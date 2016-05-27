namespace Mine.NET.Event.world
{
    /**
     * An event that is called when a world's spawn changes. The world's previous
     * spawn location is included.
     */
    public class SpawnChangeEventArgs : WorldEventArgs
    {
        private readonly Location previousLocation;

        public SpawnChangeEventArgs(World world, Location previousLocation) : base(world)
        {
            this.previousLocation = previousLocation;
        }

        /**
         * Gets the previous spawn location
         *
         * @return Location that used to be spawn
         */
        public Location getPreviousLocation()
        {
            return previousLocation;
        }
    }
}
