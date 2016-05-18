namespace Mine.NET.plugin;

/**
 * Thrown when attempting to load an invalid PluginDescriptionFile
 */
public class InvalidDescriptionException : Exception {
    private static readonly long serialVersionUID = 5721389122281775896L;

    /**
     * Constructs a new InvalidDescriptionException based on the given
     * Exception
     *
     * @param message Brief message explaining the cause of the exception
     * @param cause Exception that triggered this Exception
     */
    public InvalidDescriptionException(Exception cause, readonly String message) : base(message, cause) {
    }

    /**
     * Constructs a new InvalidDescriptionException based on the given
     * Exception
     *
     * @param cause Exception that triggered this Exception
     */
    public InvalidDescriptionException(Exception cause) : base("Invalid plugin.yml", cause) {
    }

    /**
     * Constructs a new InvalidDescriptionException with the given message
     *
     * @param message Brief message explaining the cause of the exception
     */
    public InvalidDescriptionException(String message) : base(message) {
    }

    /**
     * Constructs a new InvalidDescriptionException
     */
    public InvalidDescriptionException() : base("Invalid plugin.yml") {
    }
}
