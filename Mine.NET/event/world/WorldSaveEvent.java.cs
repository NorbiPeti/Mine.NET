namespace Mine.NET.Event.world
{
    /**
     * Called when a World is saved.
     */
    public class WorldSaveEventArgs : WorldEventArgs
    {
        public WorldSaveEventArgs(World world) : base(world)
        {
        }
    }
}
