namespace Mine.NET.Event;

public class EventException : Exception {
    private static readonly long serialVersionUID = 3532808232324183999L;
    private readonly Exception cause;

    /**
     * Constructs a new EventException based on the given Exception
     *
     * @param throwable Exception that triggered this Exception
     */
    public EventException(Exception throwable) {
        cause = throwable;
    }

    /**
     * Constructs a new EventException
     */
    public EventException() {
        cause = null;
    }

    /**
     * Constructs a new EventException with the given message
     *
     * @param cause The exception that caused this
     * @param message The message
     */
    public EventException(Exception cause, String message) {
        base(message);
        this.cause = cause;
    }

    /**
     * Constructs a new EventException with the given message
     *
     * @param message The message
     */
    public EventException(String message) {
        base(message);
        cause = null;
    }

    /**
     * If applicable, returns the Exception that triggered this Exception
     *
     * @return Inner exception, or null if one does not exist
     */
    public override Exception getCause() {
        return cause;
    }
}
