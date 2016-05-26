using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * This event is called after a player registers or unregisters a new plugin
     * channel.
     */
    public abstract class PlayerChannelEventArgs : PlayerEventArgs
    {
        private readonly String channel;

        public PlayerChannelEventArgs(Player player, String channel) : base(player)
        {
            this.channel = channel;
        }

        public String getChannel()
        {
            return channel;
        }
    }
}
