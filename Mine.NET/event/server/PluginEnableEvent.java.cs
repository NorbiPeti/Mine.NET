using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * Called when a plugin is enabled.
     */
    public class PluginEnableEventArgs : PluginEventArgs
    {
        public PluginEnableEventArgs(Plugin plugin) : base(plugin)
        {
        }
    }
}
