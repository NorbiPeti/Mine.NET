namespace Mine.NET.Event.world
{
    /**
     * Called when a World is loaded
     */
    public class WorldLoadEventArgs : WorldEventArgs
    {
        public WorldLoadEventArgs(World world) : base(world)
        {
        }
    }
}
