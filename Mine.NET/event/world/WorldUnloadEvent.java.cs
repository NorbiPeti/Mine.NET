namespace Mine.NET.Event.world
{
    /**
     * Called when a World is unloaded
     */
    public class WorldUnloadEvent : WorldEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool iscancelled;

        public WorldUnloadEvent(World world) : base(world)
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
