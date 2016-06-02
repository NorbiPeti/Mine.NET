using Mine.NET.configuration.serialization;
using Mine.NET.entity;
using Mine.NET.permissions;
using System;

namespace Mine.NET
{
    public interface OfflinePlayer : ServerOperator, AnimalTamer, ConfigurationSerializable, INamedEntity
    {

        /**
         * Checks if this player is currently online
         *
         * @return true if they are online
         */
        bool isOnline();

        /**
         * Checks if this player is banned or not
         *
         * @return true if banned, otherwise false
         */
        bool isBanned();

        /**
         * Bans or unbans this player
         *
         * @param banned true if banned
         * [Obsolete] Use {@link org.bukkit.BanList#addBan(String, String, Date,
         *     String)} or {@link org.bukkit.BanList#pardon(String)} to enhance
         *     functionality
         */
        [Obsolete]
        void setBanned(bool banned);

        /**
         * Checks if this player is whitelisted or not
         *
         * @return true if whitelisted
         */
        bool isWhitelisted();

        /**
         * Sets if this player is whitelisted or not
         *
         * @param value true if whitelisted
         */
        void setWhitelisted(bool value);

        /**
         * Gets a {@link Player} object that this represents, if there is one
         * <p>
         * If the player is online, this will return that player. Otherwise,
         * it will return null.
         *
         * @return Online player
         */
        Player getPlayer();

        /**
         * Gets the first date and time that this player was witnessed on this
         * server.
         * <p>
         * If the player has never played before, this will return 0. Otherwise,
         * it will be the amount of milliseconds since midnight, January 1, 1970
         * UTC.
         *
         * @return Date of first log-in for this player, or 0
         */
        long getFirstPlayed();

        /**
         * Gets the last date and time that this player was witnessed on this
         * server.
         * <p>
         * If the player has never played before, this will return 0. Otherwise,
         * it will be the amount of milliseconds since midnight, January 1, 1970
         * UTC.
         *
         * @return Date of last log-in for this player, or 0
         */
        long getLastPlayed();

        /**
         * Checks if this player has played on this server before.
         *
         * @return True if the player has played before, otherwise false
         */
        bool hasPlayedBefore();

        /**
         * Gets the Location where the player will spawn at their bed, null if
         * they have not slept in one or their current bed spawn is invalid.
         *
         * @return Bed Spawn Location if bed exists, otherwise null.
         */
        Location getBedSpawnLocation();

    }
}
