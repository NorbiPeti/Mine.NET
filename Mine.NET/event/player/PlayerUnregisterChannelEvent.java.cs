using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * This is called immediately after a player unregisters for a plugin channel.
     */
    public class PlayerUnregisterChannelEvent : PlayerChannelEvent
    {

        public PlayerUnregisterChannelEvent(Player player, String channel) :
            base(player, channel)
        {
        }
    }
}
