using Mine.NET.boss;
using Mine.NET.command;
using Mine.NET.entity;
using Mine.NET.Event.block;
using Mine.NET.Event.inventory;
using Mine.NET.generator;
using Mine.NET.help;
using Mine.NET.inventory;
using Mine.NET.map;
using Mine.NET.plugin;
using Mine.NET.plugin.messaging;
using Mine.NET.scheduler;
using Mine.NET.scoreboard;
using Mine.NET.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;

namespace Mine.NET
{
    /**
    * Represents a server implementation.
    */
    public abstract class Server : PluginMessageRecipient {

        /**
         * Used for all administrative messages, such as an operator using a
         * command.
         * <p>
         * For use in {@link #broadcast(java.lang.String, java.lang.String)}.
         */
        public const String BROADCAST_CHANNEL_ADMINISTRATIVE = "bukkit.broadcast.admin";

        /**
         * Used for all announcement messages, such as informing users that a
         * player has joined.
         * <p>
         * For use in {@link #broadcast(java.lang.String, java.lang.String)}.
         */
        public const String BROADCAST_CHANNEL_USERS = "bukkit.broadcast.user";

        /**
         * Gets the name of this server implementation.
         *
         * @return name of this server implementation
         */
        public abstract String getName();

        /**
         * Gets the version string of this server implementation.
         *
         * @return version of this server implementation
         */
        public abstract String getVersion();

        /**
         * Gets the Bukkit version that this server is running.
         *
         * @return version of Bukkit
         */
        public abstract String getBukkitVersion();

        /**
         * Gets an array copy of all currently logged in players.
         * <p>
         * This method exists for legacy reasons to provide backwards
         * compatibility. It will not exist at runtime and should not be used
         * under any circumstances.
         *
         * [Obsolete] superseded by {@link #getOnlinePlayers()}
         * @return an array of Players that are currently online
         */
        [Obsolete]
        public abstract Player[] _INVALID_getOnlinePlayers();

        /**
         * Gets a view of all currently logged in players. This {@linkplain
         * Collections#unmodifiableCollection(Collection) view} is a reused
         * object, making some operations like {@link Collection#size()}
         * zero-allocation.
         * <p>
         * The collection is a view backed by the internal representation, such
         * that, changes to the internal state of the server will be reflected
         * immediately. However, the reuse of the returned collection (identity)
         * is not strictly guaranteed for future or all implementations. Casting
         * the collection, or relying on interface implementations (like {@link
         * Serializable} or {@link List}), is deprecated.
         * <p>
         * Iteration behavior is undefined outside of self-contained main-thread
         * uses. Normal and immediate iterator use without consequences that
         * affect the collection are fully supported. The effects following
         * (non-exhaustive) {@link Entity#teleport(Location) teleportation},
         * {@link Player#setHealth(double) death}, and {@link Player#kickPlayer(
         * String) kicking} are undefined. Any use of this collection from
         * asynchronous threads is unsafe.
         * <p>
         * For safe consequential iteration or mimicking the old array behavior,
         * using {@link Collection#toArray(Object[])} is recommended. For making
         * snapshots, {@link ImmutableList#copyOf(Collection)} is recommended.
         *
         * @return a view of currently online players.
         */
        public abstract List<Player> getOnlinePlayers();

        /**
         * Get the maximum amount of players which can login to this server.
         *
         * @return the amount of players this server allows
         */
        public abstract int getMaxPlayers();

        /**
         * Get the game port that the server runs on.
         *
         * @return the port number of this server
         */
        public abstract int getPort();

        /**
         * Get the view distance from this server.
         *
         * @return the view distance from this server.
         */
        public abstract int getViewDistance();

        /**
         * Get the IP that this server is bound to, or empty string if not
         * specified.
         *
         * @return the IP string that this server is bound to, otherwise empty
         *     string
         */
        public abstract String getIp();

        /**
         * Get the name of this server.
         *
         * @return the name of this server
         */
        public abstract String getServerName();

        /**
         * Get an ID of this server. The ID is a simple generally alphanumeric ID
         * that can be used for uniquely identifying this server.
         *
         * @return the ID of this server
         */
        public abstract String getServerId();

        /**
         * Get world type (level-type setting) for default world.
         *
         * @return the value of level-type (e.g. DEFAULT, FLAT, DEFAULT_1_1)
         */
        public abstract String getWorldType();

        /**
         * Get generate-structures setting.
         *
         * @return true if structure generation is enabled, false otherwise
         */
        public abstract bool getGenerateStructures();

        /**
         * Gets whether this server allows the End or not.
         *
         * @return whether this server allows the End or not
         */
        public abstract bool getAllowEnd();

        /**
         * Gets whether this server allows the Nether or not.
         *
         * @return whether this server allows the Nether or not
         */
        public abstract bool getAllowNether();

        /**
         * Gets whether this server has a whitelist or not.
         *
         * @return whether this server has a whitelist or not
         */
        public abstract bool hasWhitelist();

        /**
         * Sets if the server is whitelisted.
         *
         * @param value true for whitelist on, false for off
         */
        public abstract void setWhitelist(bool value);

        /**
         * Gets a list of whitelisted players.
         *
         * @return a set containing all whitelisted players
         */
        public abstract HashSet<OfflinePlayer> getWhitelistedPlayers();

        /**
         * Reloads the whitelist from disk.
         */
        public abstract void reloadWhitelist();

        /**
         * Broadcast a message to all players.
         * <p>
         * This is the same as calling {@link #broadcast(java.lang.String,
         * java.lang.String)} to {@link #BROADCAST_CHANNEL_USERS}
         *
         * @param message the message
         * @return the number of players
         */
        public abstract int broadcastMessage(String message);

        /**
         * Gets the name of the update folder. The update folder is used to safely
         * update plugins at the right moment on a plugin load.
         * <p>
         * The update folder name is relative to the plugins folder.
         *
         * @return the name of the update folder
         */
        public abstract String getUpdateFolder();

        /**
         * Gets the update folder. The update folder is used to safely update
         * plugins at the right moment on a plugin load.
         *
         * @return the update folder
         */
        public abstract DirectoryInfo getUpdateFolderInfo();

        /**
         * Gets the value of the connection throttle setting.
         *
         * @return the value of the connection throttle setting
         */
        public abstract long getConnectionThrottle();

        /**
         * Gets default ticks per animal spawns value.
         * <p>
         * <b>Example Usage:</b>
         * <ul>
         * <li>A value of 1 will mean the server will attempt to spawn monsters
         *     every tick.
         * <li>A value of 400 will mean the server will attempt to spawn monsters
         *     every 400th tick.
         * <li>A value below 0 will be reset back to Minecraft's default.
         * </ul>
         * <p>
         * <b>Note:</b> If set to 0, animal spawning will be disabled. We
         * recommend using spawn-animals to control this instead.
         * <p>
         * Minecraft default: 400.
         *
         * @return the default ticks per animal spawns value
         */
        public abstract int getTicksPerAnimalSpawns();

        /**
         * Gets the default ticks per monster spawns value.
         * <p>
         * <b>Example Usage:</b>
         * <ul>
         * <li>A value of 1 will mean the server will attempt to spawn monsters
         *     every tick.
         * <li>A value of 400 will mean the server will attempt to spawn monsters
         *     every 400th tick.
         * <li>A value below 0 will be reset back to Minecraft's default.
         * </ul>
         * <p>
         * <b>Note:</b> If set to 0, monsters spawning will be disabled. We
         * recommend using spawn-monsters to control this instead.
         * <p>
         * Minecraft default: 1.
         *
         * @return the default ticks per monsters spawn value
         */
        public abstract int getTicksPerMonsterSpawns();

        /**
         * Gets a player object by the given username.
         * <p>
         * This method may not return objects for offline players.
         *
         * [Obsolete] Use {@link #getPlayer(Guid)} as player names are no longer
         *     guaranteed to be unique
         * @param name the name to look up
         * @return a player if one was found, null otherwise
         */
        [Obsolete]
        public abstract Player getPlayer(String name);

        /**
         * Gets the player with the exact given name, case insensitive.
         *
         * [Obsolete] Use {@link #getPlayer(Guid)} as player names are no longer
         *     guaranteed to be unique
         * @param name Exact name of the player to retrieve
         * @return a player object if one was found, null otherwise
         */
        [Obsolete]
        public abstract Player getPlayerExact(String name);

        /**
         * Attempts to match any players with the given name, and returns a list
         * of all possibly matches.
         * <p>
         * This list is not sorted in any particular order. If an exact match is
         * found, the returned list will only contain a single result.
         *
         * [Obsolete] Use {@link #getPlayer(Guid)} as player names are no longer
         *     guaranteed to be unique
         * @param name the (partial) name to match
         * @return list of all possible players
         */
        [Obsolete]
        public abstract List<Player> matchPlayer(String name);

        /**
         * Gets the player with the given Guid.
         *
         * @param id Guid of the player to retrieve
         * @return a player object if one was found, null otherwise
         */
        public abstract Player getPlayer(Guid id);

        /**
         * Gets the plugin manager for interfacing with plugins.
         *
         * @return a plugin manager for this Server instance
         */
        public abstract PluginManager getPluginManager();

        /**
         * Gets the scheduler for managing scheduled events.
         *
         * @return a scheduling service for this server
         */
        public abstract BukkitScheduler getScheduler();

        /**
         * Gets a services manager.
         *
         * @return s services manager
         */
        public abstract ServicesManager getServicesManager();

        /**
         * Gets a list of all worlds on this server.
         *
         * @return a list of worlds
         */
        public abstract List<World> getWorlds();

        /**
         * Creates or loads a world with the given name using the specified
         * options.
         * <p>
         * If the world is already loaded, it will just return the equivalent of
         * getWorld(creator.name()).
         *
         * @param creator the options to use when creating the world
         * @return newly created or loaded world
         */
        public abstract World createWorld(WorldCreator creator);

        /**
         * Unloads a world with the given name.
         *
         * @param name Name of the world to unload
         * @param save whether to save the chunks before unloading
         * @return true if successful, false otherwise
         */
        public abstract bool unloadWorld(String name, bool save);

        /**
         * Unloads the given world.
         *
         * @param world the world to unload
         * @param save whether to save the chunks before unloading
         * @return true if successful, false otherwise
         */
        public abstract bool unloadWorld(World world, bool save);

        /**
         * Gets the world with the given name.
         *
         * @param name the name of the world to retrieve
         * @return a world with the given name, or null if none exists
         */
        public abstract World getWorld(String name);

        /**
         * Gets the world from the given Unique ID.
         *
         * @param uid a unique-id of the world to retrieve
         * @return a world with the given Unique ID, or null if none exists
         */
        public abstract World getWorld(Guid uid);

        /**
         * Gets the map from the given item ID.
         *
         * @param id the id of the map to get
         * @return a map view if it exists, or null otherwise
         * [Obsolete] Magic value
         */
        [Obsolete]
        public abstract MapView getMap(short id);

        /**
         * Create a new map with an automatically assigned ID.
         *
         * @param world the world the map will belong to
         * @return a newly created map view
         */
        public abstract MapView createMap(World world);

        /**
         * Reloads the server, refreshing settings and plugin information.
         */
        public abstract void reload();

        /**
         * Returns the primary logger associated with this server instance.
         *
         * @return Logger associated with this server
         */
        public abstract Logger getLogger();

        /**
         * Gets a {@link PluginCommand} with the given name or alias.
         *
         * @param name the name of the command to retrieve
         * @return a plugin command if found, null otherwise
         */
        public abstract PluginCommand getPluginCommand(String name);

        /**
         * Writes loaded players to disk.
         */
        public abstract void savePlayers();

        /**
         * Dispatches a command on this server, and executes it if found.
         *
         * @param sender the apparent sender of the command
         * @param commandLine the command + arguments. Example: <code>test abc
         *     123</code>
         * @return returns false if no target is found
         * @throws CommandException thrown when the executor for the given command
         *     fails with an unhandled exception
         */
        public abstract bool dispatchCommand(CommandSender sender, String commandLine);

        /**
         * Populates a given {@link ServerConfig} with values attributes to this
         * server.
         *
         * @param config the server config to populate
         */
        public abstract DataMine CreateDatabase();

        /**
         * Adds a recipe to the crafting manager.
         *
         * @param recipe the recipe to add
         * @return true if the recipe was added, false if it wasn't for some
         *     reason
         */
        public abstract bool addRecipe(Recipe recipe);

        /**
         * Get a list of all recipes for a given item. The stack size is ignored
         * in comparisons. If the durability is -1, it will match any data value.
         *
         * @param result the item to match against recipe results
         * @return a list of recipes with the given result
         */
        public abstract List<Recipe> getRecipesFor(ItemStack result);

        /**
         * Get an iterator through the list of crafting recipes.
         *
         * @return an iterator
         */
        public abstract IEnumerator<Recipe> recipeIterator();

        /**
         * Clears the list of crafting recipes.
         */
        public abstract void clearRecipes();

        /**
         * Resets the list of crafting recipes to the default.
         */
        public abstract void resetRecipes();

        /**
         * Gets a list of command aliases defined in the server properties.
         *
         * @return a map of aliases to command names
         */
        public abstract Dictionary<String, String[]> getCommandAliases();

        /**
         * Gets the radius, in blocks, around each worlds spawn point to protect.
         *
         * @return spawn radius, or 0 if none
         */
        public abstract int getSpawnRadius();

        /**
         * Sets the radius, in blocks, around each worlds spawn point to protect.
         *
         * @param value new spawn radius, or 0 if none
         */
        public abstract void setSpawnRadius(int value);

        /**
         * Gets whether the Server is in online mode or not.
         *
         * @return true if the server authenticates clients, false otherwise
         */
        public abstract bool getOnlineMode();

        /**
         * Gets whether this server allows flying or not.
         *
         * @return true if the server allows flight, false otherwise
         */
        public abstract bool getAllowFlight();

        /**
         * Gets whether the server is in hardcore mode or not.
         *
         * @return true if the server mode is hardcore, false otherwise
         */
        public abstract bool isHardcore();

        /**
         * Gets whether to use vanilla (false) or exact behaviour (true).
         *
         * <ul>
         * <li>Vanilla behaviour: check for collisions and move the player if
         *     needed.
         * <li>Exact behaviour: spawn players exactly where they should be.
         * </ul>
         *
         * @return true if exact location locations are used for spawning, false
         *     for vanilla collision detection or otherwise
         *
         * [Obsolete] non standard and unused feature.
         */
        [Obsolete]
        public abstract bool useExactLoginLocation();

        /**
         * Shutdowns the server, stopping everything.
         */
        public abstract void shutdown();

        /**
         * Broadcasts the specified message to every user with the given
         * permission name.
         *
         * @param message message to broadcast
         * @param permission the required permission {@link Permissible
         *     permissibles} must have to receive the broadcast
         * @return number of message recipients
         */
        public abstract int broadcast(String message, String permission);

        /**
         * Gets the player by the given name, regardless if they are offline or
         * online.
         * <p>
         * This method may involve a blocking web request to get the Guid for the
         * given name.
         * <p>
         * This will return an object even if the player does not exist. To this
         * method, all players will exist.
         *
         * [Obsolete] Persistent storage of users should be by Guid as names are no longer
         *             unique past a single session.
         * @param name the name the player to retrieve
         * @return an offline player
         * @see #getOfflinePlayer(java.util.Guid)
         */
        [Obsolete]
        public abstract OfflinePlayer getOfflinePlayer(String name);

        /**
         * Gets the player by the given Guid, regardless if they are offline or
         * online.
         * <p>
         * This will return an object even if the player does not exist. To this
         * method, all players will exist.
         *
         * @param id the Guid of the player to retrieve
         * @return an offline player
         */
        public abstract OfflinePlayer getOfflinePlayer(Guid id);

        /**
         * Gets a set containing all current IPs that are banned.
         *
         * @return a set containing banned IP addresses
         */
        public abstract HashSet<String> getIPBans();

        /**
         * Bans the specified address from the server.
         *
         * @param address the IP address to ban
         */
        public abstract void banIP(String address);

        /**
         * Unbans the specified address from the server.
         *
         * @param address the IP address to unban
         */
        public abstract void unbanIP(String address);

        /**
         * Gets a set containing all banned players.
         *
         * @return a set containing banned players
         */
        public abstract HashSet<OfflinePlayer> getBannedPlayers();

        /**
         * Gets a ban list for the supplied type.
         * <p>
         * Bans by name are no longer supported and this method will return
         * null when trying to request them. The replacement is bans by Guid.
         *
         * @param type the type of list to fetch, cannot be null
         * @return a ban list of the specified type
         */
        public abstract BanList getBanList(BanListType type);

        /**
         * Gets a set containing all player operators.
         *
         * @return a set containing player operators
         */
        public abstract HashSet<OfflinePlayer> getOperators();

        /**
         * Gets the default {@link GameMode} for new players.
         *
         * @return the default game mode
         */
        public abstract GameMode getDefaultGameMode();

        /**
         * Sets the default {@link GameMode} for new players.
         *
         * @param mode the new game mode
         */
        public abstract void setDefaultGameMode(GameMode mode);

        /**
         * Gets a {@link ConsoleCommandSender} that may be used as an input source
         * for this server.
         *
         * @return a console command sender
         */
        public abstract ConsoleCommandSender getConsoleSender();

        /**
         * Gets the folder that contains all of the various {@link World}s.
         *
         * @return folder that contains all worlds
         */
        public abstract DirectoryInfo getWorldContainer();

        /**
         * Gets every player that has ever played on this server.
         *
         * @return an array containing all previous players
         */
        public abstract OfflinePlayer[] getOfflinePlayers();

        /**
         * Gets the {@link Messenger} responsible for this server.
         *
         * @return messenger responsible for this server
         */
        public abstract Messenger getMessenger();

        /**
         * Gets the {@link HelpMap} providing help topics for this server.
         *
         * @return a help map for this server
         */
        public abstract HelpMap getHelpMap();

        /**
         * Creates an empty inventory of the specified type. If the type is {@link
         * InventoryType#CHEST}, the new inventory has a size of 27; otherwise the
         * new inventory has the normal size for its type.
         *
         * @param owner the holder of the inventory, or null to indicate no holder
         * @param type the type of inventory to create
         * @return a new inventory
         */
        public abstract Inventory createInventory(InventoryHolder owner, InventoryType type);

        /**
         * Creates an empty inventory with the specified type and title. If the type
         * is {@link InventoryType#CHEST}, the new inventory has a size of 27;
         * otherwise the new inventory has the normal size for its type.<br>
         * It should be noted that some inventory types do not support titles and
         * may not render with said titles on the Minecraft client.
         *
         * @param owner The holder of the inventory; can be null if there's no holder.
         * @param type The type of inventory to create.
         * @param title The title of the inventory, to be displayed when it is viewed.
         * @return The new inventory.
         */
        public abstract Inventory createInventory(InventoryHolder owner, InventoryType type, String title);

        /**
         * Creates an empty inventory of type {@link InventoryType#CHEST} with the
         * specified size.
         *
         * @param owner the holder of the inventory, or null to indicate no holder
         * @param size a multiple of 9 as the size of inventory to create
         * @return a new inventory
         * @throws ArgumentException if the size is not a multiple of 9
         */
        public abstract Inventory createInventory(InventoryHolder owner, int size);

        /**
         * Creates an empty inventory of type {@link InventoryType#CHEST} with the
         * specified size and title.
         *
         * @param owner the holder of the inventory, or null to indicate no holder
         * @param size a multiple of 9 as the size of inventory to create
         * @param title the title of the inventory, displayed when inventory is
         *     viewed
         * @return a new inventory
         * @throws ArgumentException if the size is not a multiple of 9
         */
        public abstract Inventory createInventory(InventoryHolder owner, int size, String title);

        /**
         * Gets user-specified limit for number of monsters that can spawn in a
         * chunk.
         *
         * @return the monster spawn limit
         */
        public abstract int getMonsterSpawnLimit();

        /**
         * Gets user-specified limit for number of animals that can spawn in a
         * chunk.
         *
         * @return the animal spawn limit
         */
        public abstract int getAnimalSpawnLimit();

        /**
         * Gets user-specified limit for number of water animals that can spawn in
         * a chunk.
         *
         * @return the water animal spawn limit
         */
        public abstract int getWaterAnimalSpawnLimit();

        /**
         * Gets user-specified limit for number of ambient mobs that can spawn in
         * a chunk.
         *
         * @return the ambient spawn limit
         */
        public abstract int getAmbientSpawnLimit();

        /**
         * Checks the current thread against the expected primary thread for the
         * server.
         * <p>
         * <b>Note:</b> this method should not be used to indicate the current
         * state of the runtime. A current thread matching the main
         * thread indicates that it is, but a mismatch <b>does not
         * preclude</b> the same assumption.
         *
         * @return true if the current thread matches the expected primary thread,
         *     false otherwise
         */
        public abstract bool isPrimaryThread();

        /**
         * Gets the message that is displayed on the server list.
         *
         * @return the servers MOTD
         */
        public abstract String getMotd();

        /**
         * Gets the default message that is displayed when the server is stopped.
         *
         * @return the shutdown message
         */
        public abstract String getShutdownMessage();

        /**
         * Gets the current warning state for the server.
         *
         * @return the configured warning state
         */
        public abstract WarningState getWarningState();

        /**
         * Gets the instance of the item factory (for {@link ItemMeta}).
         *
         * @return the item factory
         * @see ItemFactory
         */
        public abstract ItemFactory getItemFactory();

        /**
         * Gets the instance of the scoreboard manager.
         * <p>
         * This will only exist after the first world has loaded.
         *
         * @return the scoreboard manager or null if no worlds are loaded.
         */
        public abstract ScoreboardManager getScoreboardManager();

        /**
         * Gets an instance of the server's default server-icon.
         *
         * @return the default server-icon; null values may be used by the
         *     implementation to indicate no defined icon, but this behavior is
         *     not guaranteed
         */
        public abstract CachedServerIcon getServerIcon();

        /**
         * Loads an image from a file, and returns a cached image for the specific
         * server-icon.
         * <p>
         * Size and type are implementation defined. An incompatible file is
         * guaranteed to throw an implementation-defined {@link Exception}.
         *
         * @param file the file to load the from
         * @throws ArgumentException if image is null
         * @throws Exception if the image does not meet current server server-icon
         *     specifications
         * @return a cached server-icon that can be used for a {@link
         *     ServerListPingEvent#setServerIcon(CachedServerIcon)}
         */
        public abstract CachedServerIcon loadServerIcon(FileInfo file);

        /**
         * Creates a cached server-icon for the specific image.
         * <p>
         * Size and type are implementation defined. An incompatible file is
         * guaranteed to throw an implementation-defined {@link Exception}.
         *
         * @param image the image to use
         * @throws ArgumentException if image is null
         * @throws Exception if the image does not meet current server
         *     server-icon specifications
         * @return a cached server-icon that can be used for a {@link
         *     ServerListPingEvent#setServerIcon(CachedServerIcon)}
         */
        public abstract CachedServerIcon loadServerIcon(Image image);

        /**
         * Set the idle kick timeout. Any players idle for the specified amount of
         * time will be automatically kicked.
         * <p>
         * A value of 0 will disable the idle kick timeout.
         *
         * @param threshold the idle timeout in minutes
         */
        public abstract void setIdleTimeout(int threshold);

        /**
         * Gets the idle kick timeout.
         *
         * @return the idle timeout in minutes
         */
        public abstract int getIdleTimeout();

        /**
         * Create a ChunkData for use in a generator.
         * 
         * See {@link ChunkGenerator#generateChunkData(org.bukkit.World, java.util.Random, int, int, org.bukkit.generator.ChunkGenerator.BiomeGrid)}
         * 
         * @param world the world to create the ChunkData for
         * @return a new ChunkData for the world
         * 
         */
        public abstract ChunkGenerator.ChunkData createChunkData(World world);

        /**
         * Creates a boss bar instance to display to players. The progress
         * defaults to 1.0
         *
         * @param title the title of the boss bar
         * @param color the color of the boss bar
         * @param style the style of the boss bar
         * @param flags an optional list of flags to set on the boss bar
         * @return the created boss bar
         */
        public abstract BossBar createBossBar(String title, BarColor color, BarStyle style, params BarFlag[] flags);

        /**
         * @see UnsafeValues
         * @return the unsafe values instance
         */
        [Obsolete]
        public abstract UnsafeValues getUnsafe();

        public abstract void sendPluginMessage(Plugin source, string channel, byte[] message);
        public abstract HashSet<string> getListeningPluginChannels();

        public event EventHandler<BlockBreakEventArgs> BlockBreakEvent;
        public event EventHandler<BlockBurnEventArgs> BlockBurnEvent;
    }
}
