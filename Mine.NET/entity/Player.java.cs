using Mine.NET.command;
using Mine.NET.map;
using Mine.NET.plugin.messaging;
using Mine.NET.scoreboard;
using System;
using System.Net;

namespace Mine.NET.entity
{
    /**
     * Represents a player, connected or not
     */
    public interface Player : HumanEntity, Conversable, CommandSender, OfflinePlayer, PluginMessageRecipient {

        /**
         * Gets the "friendly" name to display of this player. This may include
         * color.
         * <p>
         * Note that this name will not be displayed in game, only in chat and
         * places defined by plugins.
         *
         * @return the friendly name
         */
        String getDisplayName();

        /**
         * Sets the "friendly" name to display of this player. This may include
         * color.
         * <p>
         * Note that this name will not be displayed in game, only in chat and
         * places defined by plugins.
         *
         * @param name The new display name.
         */
        void setDisplayName(String name);

        /**
         * Gets the name that is shown on the player list.
         *
         * @return the player list name
         */
        String getPlayerListName();

        /**
         * Sets the name that is shown on the in-game player list.
         * <p>
         * The name cannot be longer than 16 chars, but {@link ChatColor} is
         * supported.
         * <p>
         * If the value is null, the name will be identical to {@link #getName()}.
         * <p>
         * This name is case sensitive and unique, two names with different casing
         * will appear as two different people. If a player joins afterwards with
         * a name that conflicts with a player's custom list name, the joining
         * player's player list name will have a random number appended to it (1-2
         * chars long in the default implementation). If the joining player's
         * name is 15 or 16 chars long, part of the name will be truncated at
         * the end to allow the addition of the two digits.
         *
         * @param name new player list name
         * @throws ArgumentException if the name is already used by someone
         *     else
         * @throws ArgumentException if the length of the name is too long
         */
        void setPlayerListName(String name);

        /**
         * Set the target of the player's compass.
         *
         * @param loc Location to point to
         */
        void setCompassTarget(Location loc);

        /**
         * Get the previously set compass target.
         *
         * @return location of the target
         */
        Location getCompassTarget();

        /**
         * Gets the socket address of this player
         *
         * @return the player's address
         */
        IPAddress getAddress();

        /**
         * Sends this sender a message raw
         *
         * @param message Message to be displayed
         */
        void sendRawMessage(String message);

        /**
         * Kicks player with custom kick message.
         *
         * @param message kick message
         */
        void kickPlayer(String message);

        /**
         * Says a message (or runs a command).
         *
         * @param msg message to print
         */
        void chat(String msg);

        /**
         * Makes the player perform the given command
         *
         * @param command Command to perform
         * @return true if the command was successful, otherwise false
         */
        bool performCommand(String command);

        /**
         * Returns if the player is in sneak mode
         *
         * @return true if player is in sneak mode
         */
        bool isSneaking();

        /**
         * Sets the sneak mode the player
         *
         * @param sneak true if player should appear sneaking
         */
        void setSneaking(bool sneak);

        /**
         * Gets whether the player is sprinting or not.
         *
         * @return true if player is sprinting.
         */
        bool isSprinting();

        /**
         * Sets whether the player is sprinting or not.
         *
         * @param sprinting true if the player should be sprinting
         */
        void setSprinting(bool sprinting);

        /**
         * Saves the players current location, health, inventory, motion, and
         * other information into the username.dat file, in the world/player
         * folder
         */
        void saveData();

        /**
         * Loads the players current location, health, inventory, motion, and
         * other information from the username.dat file, in the world/player
         * folder.
         * <p>
         * Note: This will overwrite the players current inventory, health,
         * motion, etc, with the state from the saved dat file.
         */
        void loadData();

        /**
         * Sets whether the player is ignored as not sleeping. If everyone is
         * either sleeping or has this flag set, then time will advance to the
         * next day. If everyone has this flag set but no one is actually in bed,
         * then nothing will happen.
         *
         * @param isSleeping Whether to ignore.
         */
        void setSleepingIgnored(bool isSleeping);

        /**
         * Returns whether the player is sleeping ignored.
         *
         * @return Whether player is ignoring sleep.
         */
        bool isSleepingIgnored();

        /**
         * Play a note for a player at a location. This requires a note block
         * at the particular location (as far as the client is concerned). This
         * will not work without a note block. This will not work with cake.
         *
         * @param loc The location of a note block.
         * @param instrument The instrument ID.
         * @param note The note ID.
         * [Obsolete] Magic value
         */
        [Obsolete]
        void playNote(Location loc, byte instrument, byte note);

        /**
         * Play a note for a player at a location. This requires a note block
         * at the particular location (as far as the client is concerned). This
         * will not work without a note block. This will not work with cake.
         *
         * @param loc The location of a note block
         * @param instrument The instrument
         * @param note The note
         */
        void playNote(Location loc, Instrument instrument, Note note);


        /**
         * Play a sound for a player at the location.
         * <p>
         * This function will fail silently if Location or Sound are null.
         *
         * @param location The location to play the sound
         * @param sound The sound to play
         * @param volume The volume of the sound
         * @param pitch The pitch of the sound
         */
        void playSound(Location location, Sound sound, float volume, float pitch);

        /**
         * Play a sound for a player at the location.
         * <p>
         * This function will fail silently if Location or Sound are null. No
         * sound will be heard by the player if their client does not have the
         * respective sound for the value passed.
         *
         * @param location the location to play the sound
         * @param sound the internal sound name to play
         * @param volume the volume of the sound
         * @param pitch the pitch of the sound
         */
        void playSound(Location location, String sound, float volume, float pitch);

        /**
         * Plays an effect to just this player.
         *
         * @param loc the location to play the effect at
         * @param effect the {@link Effect}
         * @param data a data bit needed for some effects
         * [Obsolete] Magic value
         */
        [Obsolete]
        void playEffect(Location loc, Effect effect, int data);

        /**
         * Plays an effect to just this player.
         *
         * @param <T> the data based based on the type of the effect
         * @param loc the location to play the effect at
         * @param effect the {@link Effect}
         * @param data a data bit needed for some effects
         */
        void playEffect<T>(Location loc, Effect effect, T data);

        /**
         * Send a block change. This fakes a block change packet for a user at a
         * certain location. This will not actually change the world in any way.
         *
         * @param loc The location of the changed block
         * @param Materials The new block
         * @param data The block data
         * [Obsolete] Magic value
         */
        [Obsolete]
        void sendBlockChange(Location loc, Materials Materials, byte data);

        /**
         * Send a chunk change. This fakes a chunk change packet for a user at a
         * certain location. The updated cuboid must be entirely within a single
         * chunk. This will not actually change the world in any way.
         * <p>
         * At least one of the dimensions of the cuboid must be even. The size of
         * the data buffer must be 2.5*sx*sy*sz and formatted in accordance with
         * the Packet51 format.
         *
         * @param loc The location of the cuboid
         * @param sx The x size of the cuboid
         * @param sy The y size of the cuboid
         * @param sz The z size of the cuboid
         * @param data The data to be sent
         * @return true if the chunk change packet was sent
         * [Obsolete] Magic value
         */
        [Obsolete]
        bool sendChunkChange(Location loc, int sx, int sy, int sz, byte[] data);

        /**
         * Send a block change. This fakes a block change packet for a user at a
         * certain location. This will not actually change the world in any way.
         *
         * @param loc The location of the changed block
         * @param Materials The new block ID
         * @param data The block data
         * [Obsolete] Magic value
         */
        [Obsolete]
        void sendBlockChange(Location loc, int Materials, byte data);

        /**
         * Send a sign change. This fakes a sign change packet for a user at
         * a certain location. This will not actually change the world in any way.
         * This method will use a sign at the location's block or a faked sign
         * sent via {@link #sendBlockChange(org.bukkit.Location, int, byte)} or
         * {@link #sendBlockChange(org.bukkit.Location, org.bukkit.Materials, byte)}.
         * <p>
         * If the client does not have a sign at the given location it will
         * display an error message to the user.
         *
         * @param loc the location of the sign
         * @param lines the new text on the sign or null to clear it
         * @throws ArgumentException if location is null
         * @throws ArgumentException if lines is non-null and has a length less than 4
         */
        void sendSignChange(Location loc, String[] lines);

        /**
         * Render a map and send it to the player in its entirety. This may be
         * used when streaming the map in the normal manner is not desirable.
         *
         * @param map The map to be sent
         */
        void sendMap(MapView map);

        /**
         * Forces an update of the player's entire inventory.
         *
         * [Obsolete] This method should not be relied upon as it is a temporary
         *     work-around for a larger, more complicated issue.
         */
        [Obsolete]
        void updateInventory();

        /**
         * Awards the given achievement and any parent achievements that the
         * player does not have.
         *
         * @param achievement Achievement to award
         * @throws ArgumentException if achievement is null
         */
        void awardAchievement(Achievement achievement);

        /**
         * Removes the given achievement and any children achievements that the
         * player has.
         *
         * @param achievement Achievement to remove
         * @throws ArgumentException if achievement is null
         */
        void removeAchievement(Achievement achievement);

        /**
         * Gets whether this player has the given achievement.
         *
         * @param achievement the achievement to check
         * @return whether the player has the achievement
         * @throws ArgumentException if achievement is null
         */
        bool hasAchievement(Achievement achievement);

        /**
         * Increments the given statistic for this player.
         * <p>
         * This is equivalent to the following code:
         * <code>incrementStatistic(Statistic, 1)</code>
         *
         * @param statistic Statistic to increment
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if the statistic requires an
         *     additional parameter
         */
        void incrementStatistic(Statistic statistic);

        /**
         * Decrements the given statistic for this player.
         * <p>
         * This is equivalent to the following code:
         * <code>decrementStatistic(Statistic, 1)</code>
         *
         * @param statistic Statistic to decrement
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if the statistic requires an
         *     additional parameter
         */
        void decrementStatistic(Statistic statistic);

        /**
         * Increments the given statistic for this player.
         *
         * @param statistic Statistic to increment
         * @param amount Amount to increment this statistic by
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if amount is negative
         * @throws ArgumentException if the statistic requires an
         *     additional parameter
         */
        void incrementStatistic(Statistic statistic, int amount);

        /**
         * Decrements the given statistic for this player.
         *
         * @param statistic Statistic to decrement
         * @param amount Amount to decrement this statistic by
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if amount is negative
         * @throws ArgumentException if the statistic requires an
         *     additional parameter
         */
        void decrementStatistic(Statistic statistic, int amount);

        /**
         * Sets the given statistic for this player.
         *
         * @param statistic Statistic to set
         * @param newValue The value to set this statistic to
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if newValue is negative
         * @throws ArgumentException if the statistic requires an
         *     additional parameter
         */
        void setStatistic(Statistic statistic, int newValue);

        /**
         * Gets the value of the given statistic for this player.
         *
         * @param statistic Statistic to check
         * @return the value of the given statistic
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if the statistic requires an
         *     additional parameter
         */
        int getStatistic(Statistic statistic);

        /**
         * Increments the given statistic for this player for the given Materials.
         * <p>
         * This is equivalent to the following code:
         * <code>incrementStatistic(Statistic, Materials, 1)</code>
         *
         * @param statistic Statistic to increment
         * @param Materials Materials to offset the statistic with
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if Materials is null
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void incrementStatistic(Statistic statistic, Materials Materials);

        /**
         * Decrements the given statistic for this player for the given Materials.
         * <p>
         * This is equivalent to the following code:
         * <code>decrementStatistic(Statistic, Materials, 1)</code>
         *
         * @param statistic Statistic to decrement
         * @param Materials Materials to offset the statistic with
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if Materials is null
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void decrementStatistic(Statistic statistic, Materials Materials);

        /**
         * Gets the value of the given statistic for this player.
         *
         * @param statistic Statistic to check
         * @param Materials Materials offset of the statistic
         * @return the value of the given statistic
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if Materials is null
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        int getStatistic(Statistic statistic, Materials Materials);

        /**
         * Increments the given statistic for this player for the given Materials.
         *
         * @param statistic Statistic to increment
         * @param Materials Materials to offset the statistic with
         * @param amount Amount to increment this statistic by
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if Materials is null
         * @throws ArgumentException if amount is negative
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void incrementStatistic(Statistic statistic, Materials Materials, int amount);

        /**
         * Decrements the given statistic for this player for the given Materials.
         *
         * @param statistic Statistic to decrement
         * @param Materials Materials to offset the statistic with
         * @param amount Amount to decrement this statistic by
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if Materials is null
         * @throws ArgumentException if amount is negative
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void decrementStatistic(Statistic statistic, Materials Materials, int amount);

        /**
         * Sets the given statistic for this player for the given Materials.
         *
         * @param statistic Statistic to set
         * @param Materials Materials to offset the statistic with
         * @param newValue The value to set this statistic to
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if Materials is null
         * @throws ArgumentException if newValue is negative
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void setStatistic(Statistic statistic, Materials Materials, int newValue);

        /**
         * Increments the given statistic for this player for the given entity.
         * <p>
         * This is equivalent to the following code:
         * <code>incrementStatistic(Statistic, EntityType, 1)</code>
         *
         * @param statistic Statistic to increment
         * @param entityType EntityType to offset the statistic with
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if entityType is null
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void incrementStatistic(Statistic statistic, EntityType entityType);

        /**
         * Decrements the given statistic for this player for the given entity.
         * <p>
         * This is equivalent to the following code:
         * <code>decrementStatistic(Statistic, EntityType, 1)</code>
         *
         * @param statistic Statistic to decrement
         * @param entityType EntityType to offset the statistic with
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if entityType is null
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void decrementStatistic(Statistic statistic, EntityType entityType);

        /**
         * Gets the value of the given statistic for this player.
         *
         * @param statistic Statistic to check
         * @param entityType EntityType offset of the statistic
         * @return the value of the given statistic
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if entityType is null
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        int getStatistic(Statistic statistic, EntityType entityType);

        /**
         * Increments the given statistic for this player for the given entity.
         *
         * @param statistic Statistic to increment
         * @param entityType EntityType to offset the statistic with
         * @param amount Amount to increment this statistic by
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if entityType is null
         * @throws ArgumentException if amount is negative
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void incrementStatistic(Statistic statistic, EntityType entityType, int amount);

        /**
         * Decrements the given statistic for this player for the given entity.
         *
         * @param statistic Statistic to decrement
         * @param entityType EntityType to offset the statistic with
         * @param amount Amount to decrement this statistic by
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if entityType is null
         * @throws ArgumentException if amount is negative
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void decrementStatistic(Statistic statistic, EntityType entityType, int amount);

        /**
         * Sets the given statistic for this player for the given entity.
         *
         * @param statistic Statistic to set
         * @param entityType EntityType to offset the statistic with
         * @param newValue The value to set this statistic to
         * @throws ArgumentException if statistic is null
         * @throws ArgumentException if entityType is null
         * @throws ArgumentException if newValue is negative
         * @throws ArgumentException if the given parameter is not valid
         *     for the statistic
         */
        void setStatistic(Statistic statistic, EntityType entityType, int newValue);

        /**
         * Sets the current time on the player's client. When relative is true the
         * player's time will be kept to its world time with the
         * specified offset.
         * <p>
         * When using non relative time the player's time will stay fixed at the
         * specified time parameter. It's up to the caller to continue updating
         * the player's time. To restore player time to normal use
         * resetPlayerTime().
         *
         * @param time The current player's perceived time or the player's time
         *     offset from the server time.
         * @param relative When true the player time is kept relative to its world
         *     time.
         */
        void setPlayerTime(long time, bool relative);

        /**
         * Returns the player's current timestamp.
         *
         * @return The player's time
         */
        long getPlayerTime();

        /**
         * Returns the player's current time offset relative to server time, or
         * the current player's fixed time if the player's time is absolute.
         *
         * @return The player's time
         */
        long getPlayerTimeOffset();

        /**
         * Returns true if the player's time is relative to the server time,
         * otherwise the player's time is absolute and will not change its current
         * time unless done so with setPlayerTime().
         *
         * @return true if the player's time is relative to the server time.
         */
        bool isPlayerTimeRelative();

        /**
         * Restores the normal condition where the player's time is
         * with the server time.
         * <p>
         * Equivalent to calling setPlayerTime(0, true).
         */
        void resetPlayerTime();

        /**
         * Sets the type of weather the player will see.  When used, the weather
         * status of the player is locked until {@link #resetPlayerWeather()} is
         * used.
         *
         * @param type The WeatherType enum type the player should experience
         */
        void setPlayerWeather(WeatherType type);

        /**
         * Returns the type of weather the player is currently experiencing.
         *
         * @return The WeatherType that the player is currently experiencing or
         *     null if player is seeing server weather.
         */
        WeatherType getPlayerWeather();

        /**
         * Restores the normal condition where the player's weather is controlled
         * by server conditions.
         */
        void resetPlayerWeather();

        /**
         * Gives the player the amount of experience specified.
         *
         * @param amount Exp amount to give
         */
        void giveExp(int amount);

        /**
         * Gives the player the amount of experience levels specified. Levels can
         * be taken by specifying a negative amount.
         *
         * @param amount amount of experience levels to give or take
         */
        void giveExpLevels(int amount);

        /**
         * Gets the players current experience points towards the next level.
         * <p>
         * This is a percentage value. 0 is "no progress" and 1 is "next level".
         *
         * @return Current experience points
         */
        float getExp();

        /**
         * Sets the players current experience points towards the next level
         * <p>
         * This is a percentage value. 0 is "no progress" and 1 is "next level".
         *
         * @param exp New experience points
         */
        void setExp(float exp);

        /**
         * Gets the players current experience level
         *
         * @return Current experience level
         */
        int getLevel();

        /**
         * Sets the players current experience level
         *
         * @param level New experience level
         */
        void setLevel(int level);

        /**
         * Gets the players total experience points
         *
         * @return Current total experience points
         */
        int getTotalExperience();

        /**
         * Sets the players current experience level
         *
         * @param exp New experience level
         */
        void setTotalExperience(int exp);

        /**
         * Gets the players current exhaustion level.
         * <p>
         * Exhaustion controls how fast the food level drops. While you have a
         * certain amount of exhaustion, your saturation will drop to zero, and
         * then your food will drop to zero.
         *
         * @return Exhaustion level
         */
        float getExhaustion();

        /**
         * Sets the players current exhaustion level
         *
         * @param value Exhaustion level
         */
        void setExhaustion(float value);

        /**
         * Gets the players current saturation level.
         * <p>
         * Saturation is a buffer for food level. Your food level will not drop if
         * you are saturated {@literal >} 0.
         *
         * @return Saturation level
         */
        float getSaturation();

        /**
         * Sets the players current saturation level
         *
         * @param value Saturation level
         */
        void setSaturation(float value);

        /**
         * Gets the players current food level
         *
         * @return Food level
         */
        int getFoodLevel();

        /**
         * Sets the players current food level
         *
         * @param value New food level
         */
        void setFoodLevel(int value);

        /**
         * Sets the Location where the player will spawn at their bed.
         *
         * @param location where to set the respawn location
         */
        void setBedSpawnLocation(Location location);

        /**
         * Sets the Location where the player will spawn at their bed.
         *
         * @param location where to set the respawn location
         * @param force whether to forcefully set the respawn location even if a
         *     valid bed is not present
         */
        void setBedSpawnLocation(Location location, bool force);

        /**
         * Determines if the Player is allowed to fly via jump key double-tap like
         * in creative mode.
         *
         * @return True if the player is allowed to fly.
         */
        bool getAllowFlight();

        /**
         * Sets if the Player is allowed to fly via jump key double-tap like in
         * creative mode.
         *
         * @param flight If flight should be allowed.
         */
        void setAllowFlight(bool flight);

        /**
         * Hides a player from this player
         *
         * @param player Player to hide
         */
        void hidePlayer(Player player);

        /**
         * Allows this player to see a player that was previously hidden
         *
         * @param player Player to show
         */
        void showPlayer(Player player);

        /**
         * Checks to see if a player has been hidden from this player
         *
         * @param player Player to check
         * @return True if the provided player is not being hidden from this
         *     player
         */
        bool canSee(Player player);

        /**
         * Checks to see if this player is currently flying or not.
         *
         * @return True if the player is flying, else false.
         */
        bool isFlying();

        /**
         * Makes this player start or stop flying.
         *
         * @param value True to fly.
         */
        void setFlying(bool value);

        /**
         * Sets the speed at which a client will fly. Negative values indicate
         * reverse directions.
         *
         * @param value The new speed, from -1 to 1.
         * @throws ArgumentException If new speed is less than -1 or
         *     greater than 1
         */
        void setFlySpeed(float value);

        /**
         * Sets the speed at which a client will walk. Negative values indicate
         * reverse directions.
         *
         * @param value The new speed, from -1 to 1.
         * @throws ArgumentException If new speed is less than -1 or
         *     greater than 1
         */
        void setWalkSpeed(float value);

        /**
         * Gets the current allowed speed that a client can fly.
         *
         * @return The current allowed speed, from -1 to 1
         */
        float getFlySpeed();

        /**
         * Gets the current allowed speed that a client can walk.
         *
         * @return The current allowed speed, from -1 to 1
         */
        float getWalkSpeed();

        /**
         * Request that the player's client download and switch texture packs.
         * <p>
         * The player's client will download the new texture pack asynchronously
         * in the background, and will automatically switch to it once the
         * download is complete. If the client has downloaded and cached the same
         * texture pack in the past, it will perform a quick timestamp check over
         * the network to determine if the texture pack has changed and needs to
         * be downloaded again. When this request is sent for the very first time
         * from a given server, the client will first display a confirmation GUI
         * to the player before proceeding with the download.
         * <p>
         * Notes:
         * <ul>
         * <li>Players can disable server textures on their client, in which
         *     case this method will have no affect on them.
         * <li>There is no concept of resetting texture packs back to default
         *     within Minecraft, so players will have to relog to do so.
         * </ul>
         *
         * @param url The URL from which the client will download the texture
         *     pack. The string must contain only US-ASCII chars and should
         *     be encoded as per RFC 1738.
         * @throws ArgumentException Thrown if the URL is null.
         * @throws ArgumentException Thrown if the URL is too long.
         * [Obsolete] Minecraft no longer uses textures packs. Instead you
         *     should use {@link #setResourcePack(String)}.
         */
        [Obsolete]
        void setTexturePack(String url);

        /**
         * Request that the player's client download and switch resource packs.
         * <p>
         * The player's client will download the new resource pack asynchronously
         * in the background, and will automatically switch to it once the
         * download is complete. If the client has downloaded and cached the same
         * resource pack in the past, it will perform a quick timestamp check
         * over the network to determine if the resource pack has changed and
         * needs to be downloaded again. When this request is sent for the very
         * first time from a given server, the client will first display a
         * confirmation GUI to the player before proceeding with the download.
         * <p>
         * Notes:
         * <ul>
         * <li>Players can disable server resources on their client, in which
         *     case this method will have no affect on them.
         * <li>There is no concept of resetting resource packs back to default
         *     within Minecraft, so players will have to relog to do so.
         * </ul>
         *
         * @param url The URL from which the client will download the resource
         *     pack. The string must contain only US-ASCII chars and should
         *     be encoded as per RFC 1738.
         * @throws ArgumentException Thrown if the URL is null.
         * @throws ArgumentException Thrown if the URL is too long. The
         *     length restriction is an implementation specific arbitrary value.
         */
        void setResourcePack(String url);

        /**
         * Gets the Scoreboard displayed to this player
         *
         * @return The current scoreboard seen by this player
         */
        Scoreboard getScoreboard();

        /**
         * Sets the player's visible Scoreboard.
         *
         * @param scoreboard New Scoreboard for the player
         * @throws ArgumentException if scoreboard is null
         * @throws ArgumentException if scoreboard was not created by the
         *     {@link org.bukkit.scoreboard.ScoreboardManager scoreboard manager}
         * @throws InvalidOperationException if this is a player that is not logged
         *     yet or has logged out
         */
        void setScoreboard(Scoreboard scoreboard);

        /**
         * Gets if the client is displayed a 'scaled' health, that is, health on a
         * scale from 0-{@link #getHealthScale()}.
         *
         * @return if client health display is scaled
         * @see Player#setHealthScaled(bool)
         */
        bool isHealthScaled();

        /**
         * Sets if the client is displayed a 'scaled' health, that is, health on a
         * scale from 0-{@link #getHealthScale()}.
         * <p>
         * Displayed health follows a simple formula <code>displayedHealth =
         * getHealth() / getMaxHealth() * getHealthScale()</code>.
         *
         * @param scale if the client health display is scaled
         */
        void setHealthScaled(bool scale);

        /**
         * Sets the number to scale health to for the client; this will also
         * {@link #setHealthScaled(bool) setHealthScaled(true)}.
         * <p>
         * Displayed health follows a simple formula <code>displayedHealth =
         * getHealth() / getMaxHealth() * getHealthScale()</code>.
         *
         * @param scale the number to scale health to
         * @throws ArgumentException if scale is &lt;0
         * @throws ArgumentException if scale is {@link Double#NaN}
         * @throws ArgumentException if scale is too high
         */
        void setHealthScale(double scale);

        /**
         * Gets the number that health is scaled to for the client.
         *
         * @return the number that health would be scaled to for the client if
         *     HealthScaling is set to true
         * @see Player#setHealthScale(double)
         * @see Player#setHealthScaled(bool)
         */
        double getHealthScale();

        /**
         * Gets the entity which is followed by the camera when in
         * {@link GameMode#SPECTATOR}.
         *
         * @return the followed entity, or null if not in spectator mode or not
         * following a specific entity.
         */
        Entity getSpectatorTarget();

        /**
         * Sets the entity which is followed by the camera when in
         * {@link GameMode#SPECTATOR}.
         *
         * @param entity the entity to follow or null to reset
         * @throws InvalidOperationException if the player is not in
         * {@link GameMode#SPECTATOR}
         */
        void setSpectatorTarget(Entity entity);

        /**
         * Sends a title and a subtitle message to the player. If either of these
         * values are null, they will not be sent and the display will remain
         * unchanged. If they are empty strings, the display will be updated as
         * such. If the strings contain a new line, only the first line will be
         * sent.
         *
         * @param title Title text
         * @param subtitle Subtitle text
         * [Obsolete] API subject to change
         */
        [Obsolete]
        void sendTitle(String title, String subtitle);

        /**
         * Resets the title displayed to the player.
         * [Obsolete] API subject to change.
         */
        [Obsolete]
        void resetTitle();


        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location.
         *
         * @param particle the particle to spawn
         * @param location the location to spawn at
         * @param count the number of particles
         */
        void spawnParticle(Particle particle, Location location, int count);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location.
         *
         * @param particle the particle to spawn
         * @param x the position on the x axis to spawn at
         * @param y the position on the y axis to spawn at
         * @param z the position on the z axis to spawn at
         * @param count the number of particles
         */
        void spawnParticle(Particle particle, double x, double y, double z, int count);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location.
         *
         * @param particle the particle to spawn
         * @param location the location to spawn at
         * @param count the number of particles
         * @param data the data to use for the particle or null,
         *             the type of this depends on {@link Particle#getDataType()}
         */
        void spawnParticle<T>(Particle particle, Location location, int count, T data);


        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location.
         *
         * @param particle the particle to spawn
         * @param x the position on the x axis to spawn at
         * @param y the position on the y axis to spawn at
         * @param z the position on the z axis to spawn at
         * @param count the number of particles
         * @param data the data to use for the particle or null,
         *             the type of this depends on {@link Particle#getDataType()}
         */
        void spawnParticle<T>(Particle particle, double x, double y, double z, int count, T data);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param location the location to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         */
        void spawnParticle(Particle particle, Location location, int count, double offsetX, double offsetY, double offsetZ);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param x the position on the x axis to spawn at
         * @param y the position on the y axis to spawn at
         * @param z the position on the z axis to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         */
        void spawnParticle(Particle particle, double x, double y, double z, int count, double offsetX, double offsetY, double offsetZ);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param location the location to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         * @param data the data to use for the particle or null,
         *             the type of this depends on {@link Particle#getDataType()}
         */
        void spawnParticle<T>(Particle particle, Location location, int count, double offsetX, double offsetY, double offsetZ, T data);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param x the position on the x axis to spawn at
         * @param y the position on the y axis to spawn at
         * @param z the position on the z axis to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         * @param data the data to use for the particle or null,
         *             the type of this depends on {@link Particle#getDataType()}
         */
        void spawnParticle<T>(Particle particle, double x, double y, double z, int count, double offsetX, double offsetY, double offsetZ, T data);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param location the location to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         * @param extra the extra data for this particle, depends on the
         *              particle used (normally speed)
         */
        void spawnParticle(Particle particle, Location location, int count, double offsetX, double offsetY, double offsetZ, double extra);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param x the position on the x axis to spawn at
         * @param y the position on the y axis to spawn at
         * @param z the position on the z axis to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         * @param extra the extra data for this particle, depends on the
         *              particle used (normally speed)
         */
        void spawnParticle(Particle particle, double x, double y, double z, int count, double offsetX, double offsetY, double offsetZ, double extra);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param location the location to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         * @param extra the extra data for this particle, depends on the
         *              particle used (normally speed)
         * @param data the data to use for the particle or null,
         *             the type of this depends on {@link Particle#getDataType()}
         */
        void spawnParticle<T>(Particle particle, Location location, int count, double offsetX, double offsetY, double offsetZ, double extra, T data);

        /**
         * Spawns the particle (the number of times specified by count)
         * at the target location. The position of each particle will be
         * randomized positively and negatively by the offset parameters
         * on each axis.
         *
         * @param particle the particle to spawn
         * @param x the position on the x axis to spawn at
         * @param y the position on the y axis to spawn at
         * @param z the position on the z axis to spawn at
         * @param count the number of particles
         * @param offsetX the maximum random offset on the X axis
         * @param offsetY the maximum random offset on the Y axis
         * @param offsetZ the maximum random offset on the Z axis
         * @param extra the extra data for this particle, depends on the
         *              particle used (normally speed)
         * @param data the data to use for the particle or null,
         *             the type of this depends on {@link Particle#getDataType()}
         */
        void spawnParticle<T>(Particle particle, double x, double y, double z, int count, double offsetX, double offsetY, double offsetZ, double extra, T data);

    }
}
