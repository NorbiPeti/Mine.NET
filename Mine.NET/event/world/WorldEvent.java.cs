namespace Mine.NET.Event.world
{
    /**
     * Represents events within a world
     */
    public abstract class WorldEventArgs : GameEventArgs
    {
        private readonly World world;

        public WorldEventArgs(World world)
        {
            this.world = world;
        }

        /**
         * Gets the world primarily involved with this event
         *
         * @return World which caused this event
         */
        public World getWorld()
        {
            return world;
        }
    }
}
