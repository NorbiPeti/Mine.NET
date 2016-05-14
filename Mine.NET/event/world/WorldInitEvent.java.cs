namespace Mine.NET.Event.world
{
    /**
     * Called when a World is initializing
     */
    public class WorldInitEvent : WorldEvent
    {
        private static readonly HandlerList handlers = new HandlerList();

        public WorldInitEvent(World world) : base(world)
        {
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
