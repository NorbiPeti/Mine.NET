namespace Mine.NET.Event.world
{
    /**
     * Thrown when a new chunk has finished being populated.
     * <p>
     * If your intent is to populate the chunk using this event, please see {@link
     * BlockPopulator}
     */
    public class ChunkPopulateEventArgs : ChunkEventArgs
    {
        public ChunkPopulateEventArgs(Chunk chunk) : base(chunk)
        {
        }
    }
}
