using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * This is called immediately after a player unregisters for a plugin channel.
     */
    public class PlayerUnregisterChannelEventArgs : PlayerChannelEventArgs
    {
        public PlayerUnregisterChannelEventArgs(Player player, String channel) :
            base(player, channel)
        {
        }
    }
}
