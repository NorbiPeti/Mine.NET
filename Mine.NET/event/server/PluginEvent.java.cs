using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * Used for plugin enable and disable events
     */
    public abstract class PluginEvent : ServerEvent
    {
        private readonly Plugin plugin;

        public PluginEvent(Plugin plugin)
        {
            this.plugin = plugin;
        }

        /**
         * Gets the plugin involved in this event
         *
         * @return Plugin for this event
         */
        public Plugin getPlugin()
        {
            return plugin;
        }
    }
}
