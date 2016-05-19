namespace Mine.NET.plugin.messaging;

/**
 * Thrown if a plugin attempts to register for a reserved channel (such as
 * "REGISTER")
 */
@SuppressWarnings("serial")
public class ReservedChannelException : AggregateException {
    public ReservedChannelException() : this("Attempted to register for a reserved channel name.") {
    }

    public ReservedChannelException(String name) {
        base("Attempted to register for a reserved channel name ('" + name + "')");
    }
}
