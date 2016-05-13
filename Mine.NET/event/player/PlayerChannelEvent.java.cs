using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * This event is called after a player registers or unregisters a new plugin
     * channel.
     */
    public abstract class PlayerChannelEvent : PlayerEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly String channel;

        public PlayerChannelEvent(Player player, String channel) : base(player)
        {
            this.channel = channel;
        }

        public String getChannel()
        {
            return channel;
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
