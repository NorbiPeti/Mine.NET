using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player joins a server
     */
    public class PlayerJoinEvent : PlayerEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private String joinMessage;

        public PlayerJoinEvent(Player playerJoined, String joinMessage) : base(playerJoined)
        {
            this.joinMessage = joinMessage;
        }

        /**
         * Gets the join message to send to all online players
         *
         * @return string join message
         */
        public String getJoinMessage()
        {
            return joinMessage;
        }

        /**
         * Sets the join message to send to all online players
         *
         * @param joinMessage join message
         */
        public void setJoinMessage(String joinMessage)
        {
            this.joinMessage = joinMessage;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
