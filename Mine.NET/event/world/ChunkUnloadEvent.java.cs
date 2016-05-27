namespace Mine.NET.Event.world
{
    /**
     * Called when a chunk is unloaded
     */
    public class ChunkUnloadEventArgs : ChunkEventArgs, Cancellable
    {
        private bool cancel = false;

        public ChunkUnloadEventArgs(Chunk chunk) : base(chunk)
        {
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }
    }
}
