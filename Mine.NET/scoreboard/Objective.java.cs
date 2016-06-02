using System;

namespace Mine.NET.scoreboard
{
    /**
     * An objective on a scoreboard that can show scores specific to entries. This
     * objective is only relevant to the display of the associated {@link
     * #getScoreboard() scoreboard}.
     */
    public interface Objective
    {

        /**
         * Gets the name of this Objective
         *
         * @return this objective'ss name
         * @throws InvalidOperationException if this objective has been unregistered
         */
        String getName();

        /**
         * Gets the name displayed to players for this objective
         *
         * @return this objective's display name
         * @throws InvalidOperationException if this objective has been unregistered
         */
        String getDisplayName();

        /**
         * Sets the name displayed to players for this objective.
         *
         * @param displayName Display name to set
         * @throws InvalidOperationException if this objective has been unregistered
         * @throws ArgumentException if displayName is null
         * @throws ArgumentException if displayName is longer than 32
         *     chars.
         */
        void setDisplayName(String displayName);

        /**
         * Gets the criteria this objective tracks.
         *
         * @return this objective's criteria
         * @throws InvalidOperationException if this objective has been unregistered
         */
        String getCriteria();

        /**
         * Gets if the objective's scores can be modified directly by a plugin.
         *
         * @return true if scores are modifiable
         * @throws InvalidOperationException if this objective has been unregistered
         * @see Criterias#HEALTH
         */
        bool isModifiable();

        /**
         * Gets the scoreboard to which this objective is attached.
         *
         * @return Owning scoreboard, or null if it has been {@link #unregister()
         *     unregistered}
         */
        Scoreboard getScoreboard();

        /**
         * Unregisters this objective from the {@link Scoreboard scoreboard.}
         *
         * @throws InvalidOperationException if this objective has been unregistered
         */
        void unregister();

        /**
         * Sets this objective to display on the specified slot for the
         * scoreboard, removing it from any other display slot.
         *
         * @param slot display slot to change, or null to not display
         * @throws InvalidOperationException if this objective has been unregistered
         */
        void setDisplaySlot(DisplaySlot slot);

        /**
         * Gets the display slot this objective is displayed at.
         *
         * @return the display slot for this objective, or null if not displayed
         * @throws InvalidOperationException if this objective has been unregistered
         */
        DisplaySlot getDisplaySlot();

        /**
         * Gets a player's Score for an Objective on this Scoreboard
         *
         * @param player Player for the Score
         * @return Score tracking the Objective and player specified
         * @throws ArgumentException if player is null
         * @throws InvalidOperationException if this objective has been unregistered
         * [Obsolete] Scoreboards can contain entries that aren't players
         * @see #getScore(String)
         */
        [Obsolete]
        Score getScore(OfflinePlayer player);

        /**
         * Gets an entry's Score for an Objective on this Scoreboard.
         *
         * @param entry Entry for the Score
         * @return Score tracking the Objective and entry specified
         * @throws ArgumentException if entry is null
         * @throws InvalidOperationException if this objective has been unregistered
         */
        Score getScore(String entry);
    }
}
