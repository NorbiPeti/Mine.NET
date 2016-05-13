using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * This is called immediately after a player registers for a plugin channel.
     */
    public class PlayerRegisterChannelEvent : PlayerChannelEvent
    {

        public PlayerRegisterChannelEvent(Player player, String channel) :
            base(player, channel)
        {
        }
    }
}
