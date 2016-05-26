using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * Called when a plugin is enabled.
     */
    public class PluginEnableEventArgs : PluginEventArgs
    {
        private static readonly HandlerList handlers = new HandlerList();

        public PluginEnableEventArgs(Plugin plugin) : base(plugin)
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
