namespace Mine.NET.plugin.messaging;

/**
 * Thrown if a Plugin attempts to send a message on an unregistered channel.
 */
@SuppressWarnings("serial")
public class ChannelNotRegisteredException : RuntimeException {
    public ChannelNotRegisteredException() : this("Attempted to send a plugin message through an unregistered channel.") {
    }

    public ChannelNotRegisteredException(String channel) : base("Attempted to send a plugin message through the unregistered channel `" + channel + "'.") {
    }
}
