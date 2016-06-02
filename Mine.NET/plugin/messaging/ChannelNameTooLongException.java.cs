using System;

namespace Mine.NET.plugin.messaging
{
    /**
     * Thrown if a Plugin Channel is too long.
     */
    public class ChannelNameTooLongException : AggregateException
    {
        public ChannelNameTooLongException() : base("Attempted to send a Plugin Message to a channel that was too large. The maximum length a channel may be is " + Messenger.MAX_CHANNEL_SIZE + " chars.")
        {
        }

        public ChannelNameTooLongException(String channel) : base("Attempted to send a Plugin Message to a channel that was too large. The maximum length a channel may be is " + Messenger.MAX_CHANNEL_SIZE + " chars (attempted " + channel.Length + " - '" + channel + ".")
        {
        }
    }
}
