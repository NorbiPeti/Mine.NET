using System;
using System.Net;

namespace Mine.NET.Event.player
{
    /**
     * Stores details for players attempting to log in.
     * <p>
     * This event is asynchronous, and not run using main thread.
     */
    public class AsyncPlayerPreLoginEventArgs : GameEventArgs
    {
        private Result result;
        private String message;
        private readonly String name;
        private readonly IPAddress ipAddress;
        private readonly Guid uniqueId;

        public AsyncPlayerPreLoginEventArgs(String name, IPAddress ipAddress, Guid uniqueId) : base(true)
        {
            this.result = Result.ALLOWED;
            this.message = "";
            this.name = name;
            this.ipAddress = ipAddress;
            this.uniqueId = uniqueId;
        }

        /**
         * Gets the current result of the login, as an enum
         *
         * @return Current Result of the login
         */
        public Result getLoginResult()
        {
            return result;
        }

        /**
         * Sets the new result of the login, as an enum
         *
         * @param result New result to set
         */
        public void setLoginResult(Result result)
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
         * Gets the player's name.
         *
         * @return the player's name
         */
        public String getName()
        {
            return name;
        }

        /**
         * Gets the player IP address.
         *
         * @return The IP address
         */
        public IPAddress getAddress()
        {
            return ipAddress;
        }

        /**
         * Gets the player's unique ID.
         *
         * @return The unique ID
         */
        public Guid getUniqueId()
        {
            return uniqueId;
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
