using System;
using System.Text;

namespace Mine.NET.plugin
{
    /**
     * The PluginLogger class is a modified {@link Logger} that prepends all
     * logging calls with the name of the plugin doing the logging. The API for
     * PluginLogger is exactly the same as {@link Logger}.
     *
     * @see Logger
     */
    public class PluginLogger : Logger
    {
        private String pluginName;

        /**
         * Creates a new PluginLogger that extracts the name from a plugin.
         *
         * @param context A reference to the plugin
         */
        public PluginLogger(Plugin context) : base(context.Name)
        {
            String prefix = context.Prefix;
            pluginName = prefix != null ? new StringBuilder().Append("[").Append(prefix).Append("] ").ToString() : "[" + context.Name + "] ";
        }
    }
}
