using System;
using System.Collections.Generic;

namespace Mine.NET.scoreboard
{
    /**
     * A scoreboard
     */
    public interface Scoreboard
    {

        /**
         * Registers an Objective on this Scoreboard
         *
         * @param name Name of the Objective
         * @param criteria Criteria for the Objective
         * @return The registered Objective
         * @throws ArgumentException if name is null
         * @throws ArgumentException if criteria is null
         * @throws ArgumentException if an objective by that name already
         *     exists
         */
        Objective registerNewObjective(String name, String criteria);

        /**
         * Gets an Objective on this Scoreboard by name
         *
         * @param name Name of the Objective
         * @return the Objective or null if it does not exist
         * @throws ArgumentException if name is null
         */
        Objective getObjective(String name);

        /**
         * Gets all Objectives of a Criteria on the Scoreboard
         *
         * @param criteria Criteria to search by
         * @return an immutable set of Objectives using the specified Criteria
         */
        HashSet<Objective> getObjectivesByCriteria(String criteria);

        /**
         * Gets all Objectives on this Scoreboard
         *
         * @return An immutable set of all Objectives on this Scoreboard
         */
        HashSet<Objective> getObjectives();

        /**
         * Gets the Objective currently displayed in a DisplaySlot on this
         * Scoreboard
         *
         * @param slot The DisplaySlot
         * @return the Objective currently displayed or null if nothing is
         *     displayed in that DisplaySlot
         * @throws ArgumentException if slot is null
         */
        Objective getObjective(DisplaySlot slot);

        /**
         * Gets all scores for a player on this Scoreboard
         *
         * @param player the player whose scores are being retrieved
         * @return immutable set of all scores tracked for the player
         * @throws ArgumentException if player is null
         * [Obsolete] Scoreboards can contain entries that aren't players
         * @see #getScores(String)
         */
        [Obsolete]
        HashSet<Score> getScores(OfflinePlayer player);

        /**
         * Gets all scores for an entry on this Scoreboard
         *
         * @param entry the entry whose scores are being retrieved
         * @return immutable set of all scores tracked for the entry
         * @throws ArgumentException if entry is null
         */
        HashSet<Score> getScores(String entry);

        /**
         * Removes all scores for a player on this Scoreboard
         *
         * @param player the player to drop all current scores for
         * @throws ArgumentException if player is null
         * [Obsolete] Scoreboards can contain entries that aren't players
         * @see #resetScores(String)
         */
        [Obsolete]
        void resetScores(OfflinePlayer player);

        /**
         * Removes all scores for an entry on this Scoreboard
         *
         * @param entry the entry to drop all current scores for
         * @throws ArgumentException if entry is null
         */
        void resetScores(String entry);

        /**
         * Gets a player's Team on this Scoreboard
         *
         * @param player the player to search for
         * @return the player's Team or null if the player is not on a team
         * @throws ArgumentException if player is null
         * [Obsolete] Scoreboards can contain entries that aren't players
         * @see #getEntryTeam(String)
         */
        [Obsolete]
        Team getPlayerTeam(OfflinePlayer player);

        /**
         * Gets a entries Team on this Scoreboard
         *
         * @param entry the entry to search for
         * @return the entries Team or null if the entry is not on a team
         * @throws ArgumentException if entry is null
         */
        Team getEntryTeam(String entry);

        /**
         * Gets a Team by name on this Scoreboard
         *
         * @param teamName Team name
         * @return the matching Team or null if no matches
         * @throws ArgumentException if teamName is null
         */
        Team getTeam(String teamName);

        /**
         * Gets all teams on this Scoreboard
         *
         * @return an immutable set of Teams
         */
        HashSet<Team> getTeams();

        /**
         * Registers a Team on this Scoreboard
         *
         * @param name Team name
         * @return registered Team
         * @throws ArgumentException if name is null
         * @throws ArgumentException if team by that name already exists
         */
        Team registerNewTeam(String name);

        /**
         * Gets all players tracked by this Scoreboard
         *
         * @return immutable set of all tracked players
         * [Obsolete] Scoreboards can contain entries that aren't players
         * @see #getEntries()
         */
        [Obsolete]
        HashSet<OfflinePlayer> getPlayers();

        /**
         * Gets all entries tracked by this Scoreboard
         *
         * @return immutable set of all tracked entries
         */
        HashSet<String> getEntries();

        /**
         * Clears any objective in the specified slot.
         *
         * @param slot the slot to remove objectives
         * @throws ArgumentException if slot is null
         */
        void clearSlot(DisplaySlot slot);
    }
}
