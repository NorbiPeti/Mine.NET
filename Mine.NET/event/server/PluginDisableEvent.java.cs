using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * Called when a plugin is disabled.
     */
    public class PluginDisableEvent : PluginEvent
    {
        private static readonly HandlerList handlers = new HandlerList();

        public PluginDisableEvent(Plugin plugin) : base(plugin)
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
