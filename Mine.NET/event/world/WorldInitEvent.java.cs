namespace Mine.NET.Event.world
{
    /**
     * Called when a World is initializing
     */
    public class WorldInitEventArgs : WorldEventArgs
    {
        public WorldInitEventArgs(World world) : base(world)
        {
        }
    }
}
