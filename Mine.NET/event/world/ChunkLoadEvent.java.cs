namespace Mine.NET.Event.world
{
    /**
     * Called when a chunk is loaded
     */
    public class ChunkLoadEvent : ChunkEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly bool newChunk;

        public ChunkLoadEvent(Chunk chunk, bool newChunk) : base(chunk)
        {
            this.newChunk = newChunk;
        }

        /**
         * Gets if this chunk was newly created or not.
         * <p>
         * Note that if this chunk is new, it will not be populated at this time.
         *
         * @return true if the chunk is new, otherwise false
         */
        public bool isNewChunk()
        {
            return newChunk;
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
