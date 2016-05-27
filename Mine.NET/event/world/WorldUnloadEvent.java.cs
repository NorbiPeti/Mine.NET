namespace Mine.NET.Event.world
{
    /**
     * Called when a World is unloaded
     */
    public class WorldUnloadEventArgs : WorldEventArgs, Cancellable
    {
        private bool iscancelled;

        public WorldUnloadEventArgs(World world) : base(world)
        {
        }

        public bool isCancelled()
        {
            return this.iscancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.iscancelled = cancel;
        }
    }
}
