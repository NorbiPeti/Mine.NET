using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player leaves a server
     */
    public class PlayerQuitEventArgs : PlayerEventArgs
    {
        private String quitMessage;

        public PlayerQuitEventArgs(Player who, String quitMessage) : base(who)
        {
            this.quitMessage = quitMessage;
        }

        /**
         * Gets the quit message to send to all online players
         *
         * @return string quit message
         */
        public String getQuitMessage()
        {
            return quitMessage;
        }

        /**
         * Sets the quit message to send to all online players
         *
         * @param quitMessage quit message
         */
        public void setQuitMessage(String quitMessage)
        {
            this.quitMessage = quitMessage;
        }
    }
}
