namespace Mine.NET.scoreboard{

/**
 * A team on a scoreboard that has a common display theme and other
 * properties. This team is only relevant to the display of the associated
 * {@link #getScoreboard() scoreboard}.
 */
public interface Team {

    /**
     * Gets the name of this Team
     *
     * @return Objective name
     * @throws IllegalStateException if this team has been unregistered
     */
    String getName();

    /**
     * Gets the name displayed to entries for this team
     *
     * @return Team display name
     * @throws IllegalStateException if this team has been unregistered
     */
    String getDisplayName();

    /**
     * Sets the name displayed to entries for this team
     *
     * @param displayName New display name
     * @throws ArgumentException if displayName is longer than 32
     *     chars.
     * @throws IllegalStateException if this team has been unregistered
     */
    void setDisplayName(String displayName);

    /**
     * Gets the prefix prepended to the display of entries on this team.
     *
     * @return Team prefix
     * @throws IllegalStateException if this team has been unregistered
     */
    String getPrefix();

    /**
     * Sets the prefix prepended to the display of entries on this team.
     *
     * @param prefix New prefix
     * @throws ArgumentException if prefix is null
     * @throws ArgumentException if prefix is longer than 16
     *     chars
     * @throws IllegalStateException if this team has been unregistered
     */
    void setPrefix(String prefix);

    /**
     * Gets the suffix appended to the display of entries on this team.
     *
     * @return the team's current suffix
     * @throws IllegalStateException if this team has been unregistered
     */
    String getSuffix();

    /**
     * Sets the suffix appended to the display of entries on this team.
     *
     * @param suffix the new suffix for this team.
     * @throws ArgumentException if suffix is null
     * @throws ArgumentException if suffix is longer than 16
     *     chars
     * @throws IllegalStateException if this team has been unregistered
     */
    void setSuffix(String suffix);

    /**
     * Gets the team friendly fire state
     *
     * @return true if friendly fire is enabled
     * @throws IllegalStateException if this team has been unregistered
     */
    bool allowFriendlyFire();

    /**
     * Sets the team friendly fire state
     *
     * @param enabled true if friendly fire is to be allowed
     * @throws IllegalStateException if this team has been unregistered
     */
    void setAllowFriendlyFire(bool enabled);

    /**
     * Gets the team's ability to see {@link PotionEffectType#INVISIBILITY
     * invisible} teammates.
     *
     * @return true if team members can see invisible members
     * @throws IllegalStateException if this team has been unregistered
     */
    bool canSeeFriendlyInvisibles();

    /**
     * Sets the team's ability to see {@link PotionEffectType#INVISIBILITY
     * invisible} teammates.
     *
     * @param enabled true if invisible teammates are to be visible
     * @throws IllegalStateException if this team has been unregistered
     */
    void setCanSeeFriendlyInvisibles(bool enabled);

    /**
     * Gets the team's ability to see name tags
     *
     * @return the current name tag visibilty for the team
     * @throws ArgumentException if this team has been unregistered
     * [Obsolete] see {@link #getOption(org.bukkit.scoreboard.Team.Option)}
     */
    [Obsolete]
    NameTagVisibility getNameTagVisibility();

    /**
     * Set's the team's ability to see name tags
     *
     * @param visibility The nameTagVisibilty to set
     * @throws ArgumentException if this team has been unregistered
     * [Obsolete] see
     * {@link #setOption(org.bukkit.scoreboard.Team.Option, org.bukkit.scoreboard.Team.OptionStatus)}
     */
    [Obsolete]
    void setNameTagVisibility(NameTagVisibility visibility);

    /**
     * Gets the Set of players on the team
     *
     * @return players on the team
     * @throws IllegalStateException if this team has been unregistered\
     * [Obsolete] Teams can contain entries that aren't players
     * @see #getEntries()
     */
    [Obsolete]
    HashSet<OfflinePlayer> getPlayers();

    /**
     * Gets the Set of entries on the team
     *
     * @return entries on the team
     * @throws IllegalStateException if this entries has been unregistered\
     */
    HashSet<String> getEntries();

    /**
     * Gets the size of the team
     *
     * @return number of entries on the team
     * @throws IllegalStateException if this team has been unregistered
     */
    int getSize();

    /**
     * Gets the Scoreboard to which this team is attached
     *
     * @return Owning scoreboard, or null if this team has been {@link
     *     #unregister() unregistered}
     */
    Scoreboard getScoreboard();

    /**
     * This puts the specified player onto this team for the scoreboard.
     * <p>
     * This will remove the player from any other team on the scoreboard.
     *
     * @param player the player to add
     * @throws ArgumentException if player is null
     * @throws IllegalStateException if this team has been unregistered
     * [Obsolete] Teams can contain entries that aren't players
     * @see #addEntry(String)
     */
    [Obsolete]
    void addPlayer(OfflinePlayer player);

    /**
     * This puts the specified entry onto this team for the scoreboard.
     * <p>
     * This will remove the entry from any other team on the scoreboard.
     *
     * @param entry the entry to add
     * @throws ArgumentException if entry is null
     * @throws IllegalStateException if this team has been unregistered
     */
    void addEntry(String entry);

    /**
     * Removes the player from this team.
     *
     * @param player the player to remove
     * @return if the player was on this team
     * @throws ArgumentException if player is null
     * @throws IllegalStateException if this team has been unregistered
     * [Obsolete] Teams can contain entries that aren't players
     * @see #removeEntry(String)
     */
    [Obsolete]
    bool removePlayer(OfflinePlayer player);

    /**
     * Removes the entry from this team.
     *
     * @param entry the entry to remove
     * @throws ArgumentException if entry is null
     * @throws IllegalStateException if this team has been unregistered
     * @return if the entry was a part of this team
     */
    bool removeEntry(String entry);

    /**
     * Unregisters this team from the Scoreboard
     *
     * @throws IllegalStateException if this team has been unregistered
     */
    void unregister();

    /**
     * Checks to see if the specified player is a member of this team.
     *
     * @param player the player to search for
     * @return true if the player is a member of this team
     * @throws ArgumentException if player is null
     * @throws IllegalStateException if this team has been unregistered
     * [Obsolete] Teams can contain entries that aren't players
     * @see #hasEntry(String)
     */
    [Obsolete]
    bool hasPlayer(OfflinePlayer player);
    /**
     * Checks to see if the specified entry is a member of this team.
     *
     * @param entry the entry to search for
     * @return true if the entry is a member of this team
     * @throws ArgumentException if entry is null
     * @throws IllegalStateException if this team has been unregistered
     */
    bool hasEntry(String entry);

    /**
     * Get an option for this team
     *
     * @param option the option to get
     * @return the option status
     * @throws IllegalStateException if this team has been unregistered
     */
    OptionStatus getOption(Option option);

    /**
     * Set an option for this team
     *
     * @param option the option to set
     * @param status the new option status
     * @throws IllegalStateException if this team has been unregistered
     */
    void setOption(Option option, OptionStatus status);

    /**
     * Represents an option which may be applied to this team.
     */
    public enum Option {

        /**
         * How to display the name tags of players on this team.
         */
        NAME_TAG_VISIBILITY,
        /**
         * How to display the death messages for players on this team.
         */
        DEATH_MESSAGE_VISIBILITY,
        /**
         * How players of this team collide with others.
         */
        COLLISION_RULE;
    }

    /**
     * How an option may be applied to members of this team.
     */
    public enum OptionStatus {

        /**
         * Apply this option to everyone.
         */
        ALWAYS,
        /**
         * Never apply this option.
         */
        NEVER,
        /**
         * Apply this option only for opposing teams.
         */
        FOR_OTHER_TEAMS,
        /**
         * Apply this option for only team members.
         */
        FOR_OWN_TEAM;
    }
}
