using Mine.NET.entity;
using System;
using System.Net;

namespace Mine.NET.Event.player
{
    /**
     * Stores details for players attempting to log in
     */
    public class PlayerLoginEvent : PlayerEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly IPAddress address;
        private readonly String hostname;
        private Result result = Result.ALLOWED;
        private String message = "";

        /**
         * This constructor defaults message to an empty string, and result to
         * ALLOWED
         *
         * @param player The {@link Player} for this event
         * @param hostname The hostname that was used to connect to the server
         * @param address The address the player used to connect, provided for
         *     timing issues
         */
        public PlayerLoginEvent(Player player, String hostname, IPAddress address) : base(player)
        {
            this.hostname = hostname;
            this.address = address;
        }

        /**
         * This constructor pre-configures the event with a result and message
         *
         * @param player The {@link Player} for this event
         * @param hostname The hostname that was used to connect to the server
         * @param address The address the player used to connect, provided for
         *     timing issues
         * @param result The result status for this event
         * @param message The message to be displayed if result denies login
         */
        public PlayerLoginEvent(Player player, String hostname, IPAddress address, Result result, String message) :
            this(player, hostname, address)
        {
            this.result = result;
            this.message = message;
        }

        /**
         * Gets the current result of the login, as an enum
         *
         * @return Current Result of the login
         */
        public Result getResult()
        {
            return result;
        }

        /**
         * Sets the new result of the login, as an enum
         *
         * @param result New result to set
         */
        public void setResult(Result result)
        {
            this.result = result;
        }

        /**
         * Gets the current kick message that will be used if getResult() !=
         * Result.ALLOWED
         *
         * @return Current kick message
         */
        public String getKickMessage()
        {
            return message;
        }

        /**
         * Sets the kick message to display if getResult() != Result.ALLOWED
         *
         * @param message New kick message
         */
        public void setKickMessage(String message)
        {
            this.message = message;
        }

        /**
         * Gets the hostname that the player used to connect to the server, or
         * blank if unknown
         *
         * @return The hostname
         */
        public String getHostname()
        {
            return hostname;
        }

        /**
         * Allows the player to log in
         */
        public void allow()
        {
            result = Result.ALLOWED;
            message = "";
        }

        /**
         * Disallows the player from logging in, with the given reason
         *
         * @param result New result for disallowing the player
         * @param message Kick message to display to the user
         */
        public void disallow(Result result, String message)
        {
            this.result = result;
            this.message = message;
        }

        /**
         * Gets the {@link IPAddress} for the Player associated with this event.
         * This method is provided as a workaround for player.getAddress()
         * returning null during PlayerLoginEvent.
         *
         * @return The address for this player. For legacy compatibility, this may
         *     be null.
         */
        public IPAddress getAddress()
        {
            return address;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }

        /**
         * Basic kick reasons for communicating to plugins
         */
        public new enum Result
        {

            /**
             * The player is allowed to log in
             */
            ALLOWED,
            /**
             * The player is not allowed to log in, due to the server being full
             */
            KICK_FULL,
            /**
             * The player is not allowed to log in, due to them being banned
             */
            KICK_BANNED,
            /**
             * The player is not allowed to log in, due to them not being on the
             * white list
             */
            KICK_WHITELIST,
            /**
             * The player is not allowed to log in, for reasons undefined
             */
            KICK_OTHER
        }
    }
}
