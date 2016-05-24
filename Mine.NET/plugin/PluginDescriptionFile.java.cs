using Mine.NET.permissions;
using Mine.NET.plugin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Mine.NET.plugin
{
    /**
     * This type is the runtime-container for the information in the plugin.yml.
     * All plugins must have a respective plugin.yml. For plugins written in java
     * using the standard plugin loader, this file must be in the root of the jar
     * file.
     * <p>
     * When Bukkit loads a plugin, it needs to know some basic information about
     * it. It reads this information from a YAML file, 'plugin.yml'. This file
     * consists of a set of attributes, each defined on a new line and with no
     * indentation.
     * <p>
     * Every (almost* every) method corresponds with a specific entry in the
     * plugin.yml. These are the <b>required</b> entries for every plugin.yml:
     * <ul>
     * <li>{@link #getName()} - <code>name</code>
     * <li>{@link #getVersion()} - <code>version</code>
     * <li>{@link #getMain()} - <code>main</code>
     * </ul>
     * <p>
     * Failing to include any of these items will throw an exception and cause the
     * server to ignore your plugin.
     * <p>
     * This is a list of the possible yaml keys, with specific details included in
     * the respective method documentations:
     * <table border=1>
     * <caption>The description of the plugin.yml layout</caption>
     * <tr>
     *     <th>Node</th>
     *     <th>Method</th>
     *     <th>Summary</th>
     * </tr><tr>
     *     <td><code>name</code></td>
     *     <td>{@link #getName()}</td>
     *     <td>The unique name of plugin</td>
     * </tr><tr>
     *     <td><code>version</code></td>
     *     <td>{@link #getVersion()}</td>
     *     <td>A plugin revision identifier</td>
     * </tr><tr>
     *     <td><code>main</code></td>
     *     <td>{@link #getMain()}</td>
     *     <td>The plugin's initial class file</td>
     * </tr><tr>
     *     <td><code>author</code><br><code>authors</code></td>
     *     <td>{@link #getAuthors()}</td>
     *     <td>The plugin contributors</td>
     * </tr><tr>
     *     <td><code>description</code></td>
     *     <td>{@link #getDescription()}</td>
     *     <td>Human readable plugin summary</td>
     * </tr><tr>
     *     <td><code>website</code></td>
     *     <td>{@link #getWebsite()}</td>
     *     <td>The URL to the plugin's site</td>
     * </tr><tr>
     *     <td><code>prefix</code></td>
     *     <td>{@link #getPrefix()}</td>
     *     <td>The token to prefix plugin log entries</td>
     * </tr><tr>
     *     <td><code>database</code></td>
     *     <td>{@link #isDatabaseEnabled()}</td>
     *     <td>Indicator to enable database support</td>
     * </tr><tr>
     *     <td><code>load</code></td>
     *     <td>{@link #getLoad()}</td>
     *     <td>The phase of server-startup this plugin will load during</td>
     * </tr><tr>
     *     <td><code>depend</code></td>
     *     <td>{@link #getDepend()}</td>
     *     <td>Other required plugins</td>
     * </tr><tr>
     *     <td><code>softdepend</code></td>
     *     <td>{@link #getSoftDepend()}</td>
     *     <td>Other plugins that add functionality</td>
     * </tr><tr>
     *     <td><code>loadbefore</code></td>
     *     <td>{@link #getLoadBefore()}</td>
     *     <td>The inverse softdepend</td>
     * </tr><tr>
     *     <td><code>commands</code></td>
     *     <td>{@link #getCommands()}</td>
     *     <td>The commands the plugin will register</td>
     * </tr><tr>
     *     <td><code>permissions</code></td>
     *     <td>{@link #getPermissions()}</td>
     *     <td>The permissions the plugin will register</td>
     * </tr><tr>
     *     <td><code>default-permission</code></td>
     *     <td>{@link #getPermissionDefault()}</td>
     *     <td>The default {@link Permission#getDefault() default} permission
     *         state for defined {@link #getPermissions() permissions} the plugin
     *         will register</td>
     * </tr><tr>
     *     <td><code>awareness</code></td>
     *     <td>{@link #getAwareness()}</td>
     *     <td>The concepts that the plugin acknowledges</td>
     * </tr>
     * </table>
     * <p>
     * A plugin.yml example:<blockquote><pre>
     *name: Inferno
     *version: 1.4.1
     *description: This plugin is so 31337. You can set yourself on fire.
     *# We could place every author in the authors list, but chose not to for illustrative purposes
     *# Also, having an author distinguishes that person as the project lead, and ensures their
     *# name is displayed first
     *author: CaptainInflamo
     *authors: [Cogito, verrier, EvilSeph]
     *website: http://www.curse.com/server-mods/minecraft/myplugin
     *
     *main: com.captaininflamo.bukkit.inferno.Inferno
     *database: false
     *depend: [NewFire, FlameWire]
     *
     *commands:
     *  flagrate:
     *    description: Set yourself on fire.
     *    aliases: [combust_me, combustMe]
     *    permission: inferno.flagrate
     *    usage: Syntax error! Simply type /&lt;command&gt; to ignite yourself.
     *  burningdeaths:
     *    description: List how many times you have died by fire.
     *    aliases: [burning_deaths, burningDeaths]
     *    permission: inferno.burningdeaths
     *    usage: |
     *      /&lt;command&gt; [player]
     *      Example: /&lt;command&gt; - see how many times you have burned to death
     *      Example: /&lt;command&gt; CaptainIce - see how many times CaptainIce has burned to death
     *
     *permissions:
     *  inferno.*:
     *    description: Gives access to all Inferno commands
     *    children:
     *      inferno.flagrate: true
     *      inferno.burningdeaths: true
     *      inferno.burningdeaths.others: true
     *  inferno.flagrate:
     *    description: Allows you to ignite yourself
     *    default: true
     *  inferno.burningdeaths:
     *    description: Allows you to see how many times you have burned to death
     *    default: true
     *  inferno.burningdeaths.others:
     *    description: Allows you to see how many times others have burned to death
     *    default: op
     *    children:
     *      inferno.burningdeaths: true
     *</pre></blockquote>
     */
    public sealed class PluginDescriptionFile
    {
        /*private static readonly ThreadLocal<Yaml> YAML = new ThreadLocal<Yaml>() {
            protected Json initialValue() {
                return new Yaml(new SafeConstructor() {
                    {
                        yamlConstructors.Add(null, new AbstractConstruct() {
                            public override Object construct(Node node) { //TODO: Wat
                                if (!node.getTag().StartsWith("!@")) {
                                    // Unknown tag - will fail
                                    return SafeConstructor.undefinedConstructor.construct(node);
                                }
                                // Unknown awareness - provide a graceful substitution
                                return new PluginAwareness() {
                                    public override string ToString() {
                                        return node.ToString();
                                    }
                                };
                            }
                        });
                        foreach (PluginAwareness.Flags flag  in  PluginAwareness.Flags.values()) {
                            yamlConstructors.Add(new Tag("!@" + flag.name()), new AbstractConstruct() {
                                public override PluginAwareness.Flags construct(Node node) {
                                    return flag;
                                }
                            });
                        }
                    }
                });
            }
        };*/
        String rawName = null;
        private String name = null;
        private String main = null;
        private String classLoaderOf = null;
        private List<String> depend = new List<string>();
        private List<String> softDepend = new List<string>();
        private List<String> loadBefore = new List<string>();
        private String version = null;
        private Dictionary<String, Dictionary<String, Object>> commands = null;
        private String description = null;
        private List<String> authors = null;
        private String website = null;
        private String prefix = null;
        private bool database = false;
        private PluginLoadOrder order = PluginLoadOrder.POSTWORLD;
        private List<Permission> permissions = null;
        private Dictionary<string, object> lazyPermissions = null;
        private PermissionDefaults defaultPerm = PermissionDefaults.OP;
        private HashSet<PluginAwareness> awareness = new HashSet<PluginAwareness>();

        public PluginDescriptionFile(Stream stream)
        {
            loadMap(asMap(YAML.get().load(stream)));
        }

        /**
         * Loads a PluginDescriptionFile from the specified reader
         *
         * @param reader The reader
         * @throws InvalidDescriptionException If the PluginDescriptionFile is
         *     invalid
         */
        public PluginDescriptionFile(StreamReader reader)
        {
            loadMap(asMap(YAML.get().load(reader)));
        }

        /**
         * Creates a new PluginDescriptionFile with the given detailed
         *
         * @param pluginName Name of this plugin
         * @param pluginVersion Version of this plugin
         * @param mainClass Full location of the main class of this plugin
         */
        public PluginDescriptionFile(String pluginName, String pluginVersion, String mainClass)
        {
            name = pluginName.Replace(' ', '_');
            version = pluginVersion;
            main = mainClass;
        }

        /**
         * Gives the name of the plugin. This name is a unique identifier for
         * plugins.
         * <ul>
         * <li>Must consist of all alphanumeric chars, underscores, hyphon,
         *     and period (a-z,A-Z,0-9, _.-). Any other char will cause the
         *     plugin.yml to fail loading.
         * <li>Used to determine the name of the plugin's data folder. Data
         *     folders are placed in the ./plugins/ directory by default, but this
         *     behavior should not be relied on. {@link Plugin#getDataFolder()}
         *     should be used to reference the data folder.
         * <li>It is good practice to name your jar the same as this, for example
         *     'MyPlugin.jar'.
         * <li>Case sensitive.
         * <li>The is the token referenced in {@link #getDepend()}, {@link
         *     #getSoftDepend()}, and {@link #getLoadBefore()}.
         * <li>Using spaces in the plugin's name is deprecated.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>name</code>.
         * <p>
         * Example:<blockquote><pre>name: MyPlugin</pre></blockquote>
         *
         * @return the name of the plugin
         */
        public String getName()
        {
            return name;
        }

        /**
         * Gives the version of the plugin.
         * <ul>
         * <li>Version is an arbitrary string, however the most common format is
         *     MajorRelease.MinorRelease.Build (eg: 1.4.1).
         * <li>Typically you will increment this every time you release a new
         *     feature or bug fix.
         * <li>Displayed when a user types <code>/version PluginName</code>
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>version</code>.
         * <p>
         * Example:<blockquote><pre>version: 1.4.1</pre></blockquote>
         *
         * @return the version of the plugin
         */
        public String getVersion()
        {
            return version;
        }

        /**
         * Gives the fully qualified name of the main class for a plugin. The
         * format should follow the {@link ClassLoader#loadClass(String)} syntax
         * to successfully be resolved at runtime. For most plugins, this is the
         * class that : {@link JavaPlugin}.
         * <ul>
         * <li>This must contain the full namespace including the class file
         *     itself.
         * <li>If your namespace is <code>org.bukkit.plugin</code>, and your class
         *     file is called <code>MyPlugin</code> then this must be
         *     <code>org.bukkit.plugin.MyPlugin</code>
         * <li>No plugin can use <code>org.bukkit.</code> as a base package for
         *     <b>any class</b>, including the main class.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>main</code>.
         * <p>
         * Example:
         * <blockquote><pre>main: org.bukkit.plugin.MyPlugin</pre></blockquote>
         *
         * @return the fully qualified main class for the plugin
         */
        public String getMain()
        {
            return main;
        }

        /**
         * Gives a human-friendly description of the functionality the plugin
         * provides.
         * <ul>
         * <li>The description can have multiple lines.
         * <li>Displayed when a user types <code>/version PluginName</code>
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>description</code>.
         * <p>
         * Example:
         * <blockquote><pre>description: This plugin is so 31337. You can set yourself on fire.</pre></blockquote>
         *
         * @return description of this plugin, or null if not specified
         */
        public String getDescription()
        {
            return description;
        }

        /**
         * Gives the phase of server startup that the plugin should be loaded.
         * <ul>
         * <li>Possible values are in {@link PluginLoadOrder}.
         * <li>Defaults to {@link PluginLoadOrder#POSTWORLD}.
         * <li>Certain caveats apply to each phase.
         * <li>When different, {@link #getDepend()}, {@link #getSoftDepend()}, and
         *     {@link #getLoadBefore()} become relative in order loaded per-phase.
         *     If a plugin loads at <code>STARTUP</code>, but a dependency loads
         *     at <code>POSTWORLD</code>, the dependency will not be loaded before
         *     the plugin is loaded.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>load</code>.
         * <p>
         * Example:<blockquote><pre>load: STARTUP</pre></blockquote>
         *
         * @return the phase when the plugin should be loaded
         */
        public PluginLoadOrder getLoad()
        {
            return order;
        }

        /**
         * Gives the list of authors for the plugin.
         * <ul>
         * <li>Gives credit to the developer.
         * <li>Used in some server error messages to provide helpful feedback on
         *     who to contact when an error occurs.
         * <li>A bukkit.org forum handle or email address is recommended.
         * <li>Is displayed when a user types <code>/version PluginName</code>
         * <li><code>authors</code> must be in <a
         *     href="http://en.wikipedia.org/wiki/YAML#Lists">YAML list
         *     format</a>.
         * </ul>
         * <p>
         * In the plugin.yml, this has two entries, <code>author</code> and
         * <code>authors</code>.
         * <p>
         * Single author example:
         * <blockquote><pre>author: CaptainInflamo</pre></blockquote>
         * Multiple author example:
         * <blockquote><pre>authors: [Cogito, verrier, EvilSeph]</pre></blockquote>
         * When both are specified, author will be the first entry in the list, so
         * this example:
         * <blockquote><pre>author: Grum
         *authors:
         *- feildmaster
         *- amaranth</pre></blockquote>
         * Is equivilant to this example:
         * <pre>authors: [Grum, feildmaster, aramanth]</pre>
         *
         * @return an immutable list of the plugin's authors
         */
        public List<String> getAuthors()
        {
            return authors;
        }

        /**
         * Gives the plugin's or plugin's author's website.
         * <ul>
         * <li>A link to the Curse page that includes documentation and downloads
         *     is highly recommended.
         * <li>Displayed when a user types <code>/version PluginName</code>
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>website</code>.
         * <p>
         * Example:
         * <blockquote><pre>website: http://www.curse.com/server-mods/minecraft/myplugin</pre></blockquote>
         *
         * @return description of this plugin, or null if not specified
         */
        public String getWebsite()
        {
            return website;
        }

        /**
         * Gives if the plugin uses a database.
         * <ul>
         * <li>Using a database is non-trivial.
         * <li>Valid values include <code>true</code> and <code>false</code>
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>database</code>.
         * <p>
         * Example:
         * <blockquote><pre>database: false</pre></blockquote>
         *
         * @return if this plugin requires a database
         * @see Plugin#getDatabase()
         */
        public bool isDatabaseEnabled()
        {
            return database;
        }

        /**
         * Gives a list of other plugins that the plugin requires.
         * <ul>
         * <li>Use the value in the {@link #getName()} of the target plugin to
         *     specify the dependency.
         * <li>If any plugin listed here is not found, your plugin will fail to
         *     load at startup.
         * <li>If multiple plugins list each other in <code>depend</code>,
         *     creating a network with no individual plugin does not list another
         *     plugin in the <a
         *     href=https://en.wikipedia.org/wiki/Circular_dependency>network</a>,
         *     all plugins in that network will fail.
         * <li><code>depend</code> must be in must be in <a
         *     href="http://en.wikipedia.org/wiki/YAML#Lists">YAML list
         *     format</a>.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>depend</code>.
         * <p>
         * Example:
         * <blockquote><pre>depend:
         *- OnePlugin
         *- AnotherPlugin</pre></blockquote>
         *
         * @return immutable list of the plugin's dependencies
         */
        public List<String> getDepend()
        {
            return depend;
        }

        /**
         * Gives a list of other plugins that the plugin requires for full
         * functionality. The {@link PluginManager} will make best effort to treat
         * all entries here as if they were a {@link #getDepend() dependency}, but
         * will never fail because of one of these entries.
         * <ul>
         * <li>Use the value in the {@link #getName()} of the target plugin to
         *     specify the dependency.
         * <li>When an unresolvable plugin is listed, it will be ignored and does
         *     not affect load order.
         * <li>When a circular dependency occurs (a network of plugins depending
         *     or soft-dependending each other), it will arbitrarily choose a
         *     plugin that can be resolved when ignoring soft-dependencies.
         * <li><code>softdepend</code> must be in <a
         *     href="http://en.wikipedia.org/wiki/YAML#Lists">YAML list
         *     format</a>.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>softdepend</code>.
         * <p>
         * Example:
         * <blockquote><pre>softdepend: [OnePlugin, AnotherPlugin]</pre></blockquote>
         *
         * @return immutable list of the plugin's preferred dependencies
         */
        public List<String> getSoftDepend()
        {
            return softDepend;
        }

        /**
         * Gets the list of plugins that should consider this plugin a
         * soft-dependency.
         * <ul>
         * <li>Use the value in the {@link #getName()} of the target plugin to
         *     specify the dependency.
         * <li>The plugin should load before any other plugins listed here.
         * <li>Specifying another plugin here is strictly equivalent to having the
         *     specified plugin's {@link #getSoftDepend()} include {@link
         *     #getName() this plugin}.
         * <li><code>loadbefore</code> must be in <a
         *     href="http://en.wikipedia.org/wiki/YAML#Lists">YAML list
         *     format</a>.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>loadbefore</code>.
         * <p>
         * Example:
         * <blockquote><pre>loadbefore:
         *- OnePlugin
         *- AnotherPlugin</pre></blockquote>
         *
         * @return immutable list of plugins that should consider this plugin a
         *     soft-dependency
         */
        public List<String> getLoadBefore()
        {
            return loadBefore;
        }

        /**
         * Gives the token to prefix plugin-specific logging messages with.
         * <ul>
         * <li>This includes all messages using {@link Plugin#getLogger()}.
         * <li>If not specified, the server uses the plugin's {@link #getName()
         *     name}.
         * <li>This should clearly indicate what plugin is being logged.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>prefix</code>.
         * <p>
         * Example:<blockquote><pre>prefix: ex-why-zee</pre></blockquote>
         *
         * @return the prefixed logging token, or null if not specified
         */
        public String getPrefix()
        {
            return prefix;
        }

        /**
         * Gives the map of command-name to command-properties. Each entry in this
         * map corresponds to a single command and the respective values are the
         * properties of the command. Each property, <i>with the exception of
         * aliases</i>, can be defined at runtime using methods in {@link
         * PluginCommand} and are defined here only as a convenience.
         * <table border=1>
         * <caption>The command section's description</caption>
         * <tr>
         *     <th>Node</th>
         *     <th>Method</th>
         *     <th>Type</th>
         *     <th>Description</th>
         *     <th>Example</th>
         * </tr><tr>
         *     <td><code>description</code></td>
         *     <td>{@link PluginCommand#setDescription(String)}</td>
         *     <td>String</td>
         *     <td>A user-friendly description for a command. It is useful for
         *         documentation purposes as well as in-game help.</td>
         *     <td><blockquote><pre>description: Set yourself on fire</pre></blockquote></td>
         * </tr><tr>
         *     <td><code>aliases</code></td>
         *     <td>{@link PluginCommand#setAliases(List)}</td>
         *     <td>String or <a
         *         href="http://en.wikipedia.org/wiki/YAML#Lists">List</a> of
         *         strings</td>
         *     <td>Alternative command names, with special usefulness for commands
         *         that are already registered. <i>Aliases are not effective when
         *         defined at runtime,</i> so the plugin description file is the
         *         only way to have them properly defined.
         *         <p>
         *         Note: Command aliases may not have a colon in them.</td>
         *     <td>Single alias format:
         *         <blockquote><pre>aliases: combust_me</pre></blockquote> or
         *         multiple alias format:
         *         <blockquote><pre>aliases: [combust_me, combustMe]</pre></blockquote></td>
         * </tr><tr>
         *     <td><code>permission</code></td>
         *     <td>{@link PluginCommand#setPermission(String)}</td>
         *     <td>String</td>
         *     <td>The name of the {@link Permission} required to use the command.
         *         A user without the permission will receive the specified
         *         message (see {@linkplain
         *         PluginCommand#setPermissionMessage(String) below}), or a
         *         standard one if no specific message is defined. Without the
         *         permission node, no {@link
         *         PluginCommand#setExecutor(CommandExecutor) CommandExecutor} or
         *         {@link PluginCommand#setTabCompleter(TabCompleter)
         *         TabCompleter} will be called.</td>
         *     <td><blockquote><pre>permission: inferno.flagrate</pre></blockquote></td>
         * </tr><tr>
         *     <td><code>permission-message</code></td>
         *     <td>{@link PluginCommand#setPermissionMessage(String)}</td>
         *     <td>String</td>
         *     <td><ul>
         *         <li>Displayed to a player that attempts to use a command, but
         *             does not have the required permission. See {@link
         *             PluginCommand#getPermission() above}.
         *         <li>&lt;permission&gt; is a macro that is replaced with the
         *             permission node required to use the command.
         *         <li>Using empty quotes is a valid way to indicate nothing
         *             should be displayed to a player.
         *         </ul></td>
         *     <td><blockquote><pre>permission-message: You do not have /&lt;permission&gt;</pre></blockquote></td>
         * </tr><tr>
         *     <td><code>usage</code></td>
         *     <td>{@link PluginCommand#setUsage(String)}</td>
         *     <td>String</td>
         *     <td>This message is displayed to a player when the {@link
         *         PluginCommand#setExecutor(CommandExecutor)} {@linkplain
         *         CommandExecutor#onCommand(CommandSender,Command,String,String[])
         *         returns false}. &lt;command&gt; is a macro that is replaced
         *         the command issued.</td>
         *     <td><blockquote><pre>usage: Syntax error! Perhaps you meant /&lt;command&gt; PlayerName?</pre></blockquote>
         *         It is worth noting that to use a colon in a yaml, like
         *         <code>`usage: Usage: /god [player]'</code>, you need to
         *         <a href="http://yaml.org/spec/current.html#id2503232">surround
         *         the message with double-quote</a>:
         *         <blockquote><pre>usage: "Usage: /god [player]"</pre></blockquote></td>
         * </tr>
         * </table>
         * The commands are structured as a hiearchy of <a
         * href="http://yaml.org/spec/current.html#id2502325">nested mappings</a>.
         * The primary (top-level, no intendentation) node is
         * `<code>commands</code>', while each individual command name is
         * indented, indicating it maps to some value (in our case, the
         * properties of the table above).
         * <p>
         * Here is an example bringing together the piecemeal examples above, as
         * well as few more definitions:<blockquote><pre>
         *commands:
         *  flagrate:
         *    description: Set yourself on fire.
         *    aliases: [combust_me, combustMe]
         *    permission: inferno.flagrate
         *    permission-message: You do not have /&lt;permission&gt;
         *    usage: Syntax error! Perhaps you meant /&lt;command&gt; PlayerName?
         *  burningdeaths:
         *    description: List how many times you have died by fire.
         *    aliases:
         *    - burning_deaths
         *    - burningDeaths
         *    permission: inferno.burningdeaths
         *    usage: |
         *      /&lt;command&gt; [player]
         *      Example: /&lt;command&gt; - see how many times you have burned to death
         *      Example: /&lt;command&gt; CaptainIce - see how many times CaptainIce has burned to death
         *  # The next command has no description, aliases, etc. defined, but is still valid
         *  # Having an empty declaration is useful for defining the description, permission, and messages from a configuration dynamically
         *  apocalypse:
         *</pre></blockquote>
         * Note: Command names may not have a colon in their name.
         *
         * @return the commands this plugin will register
         */
        public Dictionary<String, Dictionary<String, Object>> getCommands()
        {
            return commands;
        }

        /**
         * Gives the list of permissions the plugin will register at runtime,
         * immediately proceding enabling. The format for defining permissions is
         * a map from permission name to properties. To represent a map without
         * any specific property, empty <a
         * href="http://yaml.org/spec/current.html#id2502702">curly-braces</a> (
         * <code>&#123;&#125;</code> ) may be used (as a null value is not
         * accepted, unlike the {@link #getCommands() commands} above).
         * <p>
         * A list of optional properties for permissions:
         * <table border=1>
         * <caption>The permission section's description</caption>
         * <tr>
         *     <th>Node</th>
         *     <th>Description</th>
         *     <th>Example</th>
         * </tr><tr>
         *     <td><code>description</code></td>
         *     <td>Plaintext (user-friendly) description of what the permission
         *         is for.</td>
         *     <td><blockquote><pre>description: Allows you to set yourself on fire</pre></blockquote></td>
         * </tr><tr>
         *     <td><code>default</code></td>
         *     <td>The default state for the permission, as defined by {@link
         *         Permission#getDefault()}. If not defined, it will be set to
         *         the value of {@link PluginDescriptionFile#getPermissionDefault()}.
         *         <p>
         *         For reference:<ul>
         *         <li><code>true</code> - Represents a positive assignment to
         *             {@link Permissible permissibles}.
         *         <li><code>false</code> - Represents no assignment to {@link
         *             Permissible permissibles}.
         *         <li><code>op</code> - Represents a positive assignment to
         *             {@link Permissible#isOp() operator permissibles}.
         *         <li><code>notop</code> - Represents a positive assignment to
         *             {@link Permissible#isOp() non-operator permissibiles}.
         *         </ul></td>
         *     <td><blockquote><pre>default: true</pre></blockquote></td>
         * </tr><tr>
         *     <td><code>children</code></td>
         *     <td>Allows other permissions to be set as a {@linkplain
         *         Permission#getChildren() relation} to the parent permission.
         *         When a parent permissions is assigned, child permissions are
         *         respectively assigned as well.
         *         <ul>
         *         <li>When a parent permission is assigned negatively, child
         *             permissions are assigned based on an inversion of their
         *             association.
         *         <li>When a parent permission is assigned positively, child
         *             permissions are assigned based on their association.
         *         </ul>
         *         <p>
         *         Child permissions may be defined in a number of ways:<ul>
         *         <li>Children may be defined as a <a
         *             href="http://en.wikipedia.org/wiki/YAML#Lists">list</a> of
         *             names. Using a list will treat all children associated
         *             positively to their parent.
         *         <li>Children may be defined as a map. Each permission name maps
         *             to either a bool (representing the association), or a
         *             nested permission definition (just as another permission).
         *             Using a nested definition treats the child as a positive
         *             association.
         *         <li>A nested permission definition must be a map of these same
         *             properties. To define a valid nested permission without
         *             defining any specific property, empty curly-braces (
         *             <code>&#123;&#125;</code> ) must be used.
         *          <li>A nested permission may carry it's own nested permissions
         *              as children, as they may also have nested permissions, and
         *              so forth. There is no direct limit to how deep the
         *              permission tree is defined.
         *         </ul></td>
         *     <td>As a list:
         *         <blockquote><pre>children: [inferno.flagrate, inferno.burningdeaths]</pre></blockquote>
         *         Or as a mapping:
         *         <blockquote><pre>children:
         *  inferno.flagrate: true
         *  inferno.burningdeaths: true</pre></blockquote>
         *         An additional example showing basic nested values can be seen
         *         <a href="doc-files/permissions-example_plugin.yml">here</a>.
         *         </td>
         * </tr>
         * </table>
         * The permissions are structured as a hiearchy of <a
         * href="http://yaml.org/spec/current.html#id2502325">nested mappings</a>.
         * The primary (top-level, no intendentation) node is
         * `<code>permissions</code>', while each individual permission name is
         * indented, indicating it maps to some value (in our case, the
         * properties of the table above).
         * <p>
         * Here is an example using some of the properties:<blockquote><pre>
         *permissions:
         *  inferno.*:
         *    description: Gives access to all Inferno commands
         *    children:
         *      inferno.flagrate: true
         *      inferno.burningdeaths: true
         *  inferno.flagate:
         *    description: Allows you to ignite yourself
         *    default: true
         *  inferno.burningdeaths:
         *    description: Allows you to see how many times you have burned to death
         *    default: true
         *</pre></blockquote>
         * Another example, with nested definitions, can be found <a
         * href="doc-files/permissions-example_plugin.yml">here</a>.
         * 
         * @return the permissions this plugin will register
         */
        public List<Permission> getPermissions()
        {
            if (permissions == null)
            {
                if (lazyPermissions == null)
                {
                    permissions = new List<Permission>();
                }
                else
                {
                    permissions = new List<Permission>(Permission.loadPermissions(lazyPermissions, "Permission node '%s' in plugin description file for " + getFullName() + " is invalid", defaultPerm));
                    lazyPermissions = null;
                }
            }
            return permissions;
        }

        /**
         * Gives the default {@link Permission#getDefault() default} state of
         * {@link #getPermissions() permissions} registered for the plugin.
         * <ul>
         * <li>If not specified, it will be {@link PermissionDefault#OP}.
         * <li>It is matched using {@link PermissionDefault#getByName(String)}
         * <li>It only affects permissions that do not define the
         *     <code>default</code> node.
         * <li>It may be any value in {@link PermissionDefault}.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>default-permission</code>.
         * <p>
         * Example:<blockquote><pre>default-permission: NOT_OP</pre></blockquote>
         *
         * @return the default value for the plugin's permissions
         */
        public PermissionDefaults getPermissionDefault()
        {
            return defaultPerm;
        }

        /**
         * Gives a set of every {@link PluginAwareness} for a plugin. An awareness
         * dictates something that a plugin developer acknowledges when the plugin
         * is compiled. Some implementions may define extra awarenesses that are
         * not included in the API. Any unrecognized
         * awareness (one unsupported or in a future version) will cause a dummy
         * object to be created instead of failing.
         * 
         * <ul>
         * <li>Currently only supports the enumerated values in {@link
         *     PluginAwareness.Flags}.
         * <li>Each awareness starts the identifier with bang-at
         *     (<code>!@</code>).
         * <li>Unrecognized (future / unimplemented) entries are quietly replaced
         *     by a generic object that : PluginAwareness.
         * <li>A type of awareness must be defined by the runtime and acknowledged
         *     by the API, effectively discluding any derived type from any
         *     plugin's classpath.
         * <li><code>awareness</code> must be in <a
         *     href="http://en.wikipedia.org/wiki/YAML#Lists">YAML list
         *     format</a>.
         * </ul>
         * <p>
         * In the plugin.yml, this entry is named <code>awareness</code>.
         * <p>
         * Example:<blockquote><pre>awareness:
         *- !@UTF8</pre></blockquote>
         * <p>
         * <b>Note:</b> Although unknown versions of some future awareness are
         * gracefully substituted, previous versions of Bukkit (ones prior to the
         * first implementation of awareness) will fail to load a plugin that
         * defines any awareness.
         *
         * @return a set containing every awareness for the plugin
         */
        public HashSet<PluginAwareness> getAwareness()
        {
            return awareness;
        }

        /**
         * Returns the name of a plugin, including the version. This method is
         * provided for convenience; it uses the {@link #getName()} and {@link
         * #getVersion()} entries.
         *
         * @return a descriptive name of the plugin and respective version
         */
        public String getFullName()
        {
            return name + " v" + version;
        }

        /**
         * @return unused
         * [Obsolete] unused
         */
        [Obsolete]
        public String getClassLoaderOf()
        {
            return classLoaderOf;
        }

        public void setDatabaseEnabled(bool database)
        {
            this.database = database;
        }

        /**
         * Saves this PluginDescriptionFile to the given writer
         *
         * @param writer Writer to output this file to
         */
        public void save(StreamWriter writer)
        {
            YAML.get().dump(saveMap(), writer);
        }

        private void loadMap(Dictionary<string, object> map)
        {
            try
            {
                name = rawName = map["name"].ToString();

                if (!Regex.Match(name, "^[A-Za-z0-9 _.-]+$").Success)
                {
                    throw new InvalidDescriptionException("name '" + name + "' contains invalid chars.");
                }
                name = name.Replace(' ', '_');
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidDescriptionException(ex, "name is not defined");
            }

            try
            {
                version = map["version"].ToString();
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidDescriptionException(ex, "version is not defined");
            }

            try
            {
                main = map["main"].ToString();
                if (main.StartsWith("org.bukkit."))
                {
                    throw new InvalidDescriptionException("main may not be within the org.bukkit namespace");
                }
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidDescriptionException(ex, "main is not defined");
            }

            if (map["commands"] != null)
            {
                Dictionary<String, Dictionary<String, Object>> commandsBuilder = new Dictionary<string, Dictionary<string, object>>();
                try
                {
                    foreach (var command in (Dictionary<string, object>)map["commands"])
                    {
                        Dictionary<String, Object> commandBuilder = new Dictionary<string, object>();
                        if (command.Value != null)
                        {
                            foreach (var commandEntry in (Dictionary<string, object>)command.Value)
                            {
                                if (commandEntry.Value is IEnumerable)
                                {
                                    // This prevents internal alias list changes
                                    List<object> commandSubList = new List<object>();
                                    foreach (Object commandSubListItem in (IEnumerable)commandEntry.Value)
                                    {
                                        if (commandSubListItem != null)
                                        {
                                            commandSubList.Add(commandSubListItem);
                                        }
                                    }
                                    commandBuilder.Add(commandEntry.Key.ToString(), commandSubList);
                                }
                                else if (commandEntry.Value != null)
                                {
                                    commandBuilder.Add(commandEntry.Key.ToString(), commandEntry.Value);
                                }
                            }
                        }
                        commandsBuilder.Add(command.Key.ToString(), commandBuilder);
                    }
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidDescriptionException(ex, "commands are of wrong type");
                }
                commands = commandsBuilder;
            }

            if (map["class-loader-of"] != null)
            {
                classLoaderOf = map["class-loader-of"].ToString();
            }

            depend = makePluginNameList(map, "depend");
            softDepend = makePluginNameList(map, "softdepend");
            loadBefore = makePluginNameList(map, "loadbefore");

            if (map["database"] != null)
            {
                try
                {
                    database = (bool)map["database"];
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidDescriptionException(ex, "database is of wrong type");
                }
            }

            if (map["website"] != null)
            {
                website = map["website"].ToString();
            }

            if (map["description"] != null)
            {
                description = map["description"].ToString();
            }

            if (map["load"] != null)
            {
                try
                {
                    order = (PluginLoadOrder)Enum.Parse(typeof(PluginLoadOrder), ((String)map["load"]).ToUpper().ReplaceAll("\\W", ""));
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidDescriptionException(ex, "load is of wrong type");
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidDescriptionException(ex, "load is not a valid choice");
                }
            }

            if (map["authors"] != null)
            {
                List<string> authorsBuilder = new List<string>();
                if (map["author"] != null)
                {
                    authorsBuilder.Add(map["author"].ToString());
                }
                try
                {
                    foreach (Object o in (IEnumerable)map["authors"])
                    {
                        authorsBuilder.Add(o.ToString());
                    }
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidDescriptionException(ex, "authors are of wrong type");
                }
                catch (NullReferenceException ex)
                {
                    throw new InvalidDescriptionException(ex, "authors are improperly defined");
                }
                authors = authorsBuilder; //TODO
            }
            else if (map["author"] != null)
            {
                authors = new List<string> { map["author"].ToString() };
            }
            else
            {
                authors = new List<string>();
            }

            if (map["default-permission"] != null)
            {
                try
                {
                    defaultPerm = PermissionDefault.getByName(map["default-permission"].ToString());
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidDescriptionException(ex, "default-permission is of wrong type");
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidDescriptionException(ex, "default-permission is not a valid choice");
                }
            }

            if (map["awareness"] is IEnumerable)
            {
                HashSet<PluginAwareness> awareness = new HashSet<PluginAwareness>();
                try
                {
                    foreach (Object o in (IEnumerable)map["awareness"])
                    {
                        awareness.Add((PluginAwareness)o);
                    }
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidDescriptionException(ex, "awareness has wrong type");
                }
                this.awareness = new HashSet<PluginAwareness>(awareness);
            }

            try
            {
                lazyPermissions = (Dictionary<string, object>)map["permissions"];
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidDescriptionException(ex, "permissions are of the wrong type");
            }

            if (map["prefix"] != null)
            {
                prefix = map["prefix"].ToString();
            }
        }

        private static List<String> makePluginNameList(Dictionary<string, object> map, String key)
        {
            Object value = map[key];
            if (value == null)
            {
                return new List<string>();
            }

            List<string> builder = new List<string>();
            try
            {
                foreach (Object entry in (IEnumerable)value)
                {
                    builder.Add(entry.ToString().Replace(' ', '_'));
                }
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidDescriptionException(ex, key + " is of wrong type");
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidDescriptionException(ex, "invalid " + key + " format");
            }
            return builder;
        }

        private Dictionary<String, Object> saveMap()
        {
            Dictionary<String, Object> map = new Dictionary<String, Object>();

            map.Add("name", name);
            map.Add("main", main);
            map.Add("version", version);
            map.Add("database", database);
            map.Add("order", order.ToString());
            map.Add("default-permission", defaultPerm.ToString());

            if (commands != null)
            {
                map.Add("command", commands);
            }
            if (depend != null)
            {
                map.Add("depend", depend);
            }
            if (softDepend != null)
            {
                map.Add("softdepend", softDepend);
            }
            if (website != null)
            {
                map.Add("website", website);
            }
            if (description != null)
            {
                map.Add("description", description);
            }

            if (authors.Count == 1)
            {
                map.Add("author", authors[0]);
            }
            else if (authors.Count > 1)
            {
                map.Add("authors", authors);
            }

            if (classLoaderOf != null)
            {
                map.Add("class-loader-of", classLoaderOf);
            }

            if (prefix != null)
            {
                map.Add("prefix", prefix);
            }

            return map;
        }

        private Dictionary<string, object> asMap(Object object_)
        {
            if (object_ is Dictionary<string, object>)
            {
                return (Dictionary<string, object>)object_;
            }
            throw new InvalidDescriptionException(object_ + " is not properly structured.");
        }

        /**
         * @return internal use
         * [Obsolete] Internal use
         */
        [Obsolete]
        public String getRawName()
        {
            return rawName;
        }
    }
}
