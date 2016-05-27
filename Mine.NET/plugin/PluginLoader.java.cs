using Mine.NET.Event;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Mine.NET.plugin
{
    /**
     * Represents a plugin loader, which handles direct access to specific types
     * of plugins
     */
    public interface PluginLoader
    {

        /**
         * Loads the plugin contained in the specified file
         *
         * @param file FileInfo to attempt to load
         * @return Plugin that was contained in the specified file, or null if
         *     unsuccessful
         * @throws InvalidPluginException Thrown when the specified file is not a
         *     plugin
         * @throws UnknownDependencyException If a required dependency could not
         *     be found
         */
        Plugin loadPlugin(FileInfo file, Assembly asm);

        /**
         * Returns a list of all filename filters expected by this PluginLoader
         *
         * @return The filters
         */
        Regex[] getPluginFileFilters();

        /**
         * Creates and returns registered listeners for the event classes used in
         * this listener
         *
         * @param listener The object that will handle the eventual call back
         * @param plugin The plugin to use when creating registered listeners
         * @return The registered listeners.
         */
        //Dictionary<T, HashSet<RegisteredListener>> createRegisteredListeners<T>(Listener listener, Plugin plugin) where T : Event.Event;

        /**
         * Enables the specified plugin
         * <p>
         * Attempting to enable a plugin that is already enabled will have no
         * effect
         *
         * @param plugin Plugin to enable
         */
        void enablePlugin(Plugin plugin);

        /**
         * Disables the specified plugin
         * <p>
         * Attempting to disable a plugin that is not enabled will have no effect
         *
         * @param plugin Plugin to disable
         */
        void disablePlugin(Plugin plugin);
    }
}
