using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * Called when a plugin is disabled.
     */
    public class PluginDisableEventArgs : PluginEventArgs
    {
        public PluginDisableEventArgs(Plugin plugin) : base(plugin)
        {
        }
    }
}
