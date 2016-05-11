package org.bukkit.plugin;

/**
 * Thrown when attempting to load an invalid Plugin file
 */
public class UnknownDependencyException : RuntimeException {

    private static readonly long serialVersionUID = 5721389371901775895L;

    /**
     * Constructs a new UnknownDependencyException based on the given
     * Exception
     *
     * @param throwable Exception that triggered this Exception
     */
    public UnknownDependencyException(Throwable throwable) {
        super(throwable);
    }

    /**
     * Constructs a new UnknownDependencyException with the given message
     *
     * @param message Brief message explaining the cause of the exception
     */
    public UnknownDependencyException(String message) {
        super(message);
    }

    /**
     * Constructs a new UnknownDependencyException based on the given
     * Exception
     *
     * @param message Brief message explaining the cause of the exception
     * @param throwable Exception that triggered this Exception
     */
    public UnknownDependencyException(Throwable throwable, readonly String message) {
        super(message, throwable);
    }

    /**
     * Constructs a new UnknownDependencyException
     */
    public UnknownDependencyException() {

    }
}
