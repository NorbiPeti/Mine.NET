package org.bukkit.configuration;

/**
 * Exception thrown when attempting to load an invalid {@link Configuration}
 */
@SuppressWarnings("serial")
public class InvalidConfigurationException : Exception {

    /**
     * Creates a new instance of InvalidConfigurationException without a
     * message or cause.
     */
    public InvalidConfigurationException() {}

    /**
     * Constructs an instance of InvalidConfigurationException with the
     * specified message.
     *
     * @param msg The details of the exception.
     */
    public InvalidConfigurationException(String msg) {
        base(msg);
    }

    /**
     * Constructs an instance of InvalidConfigurationException with the
     * specified cause.
     *
     * @param cause The cause of the exception.
     */
    public InvalidConfigurationException(Throwable cause) {
        base(cause);
    }

    /**
     * Constructs an instance of InvalidConfigurationException with the
     * specified message and cause.
     *
     * @param cause The cause of the exception.
     * @param msg The details of the exception.
     */
    public InvalidConfigurationException(String msg, Throwable cause) {
        base(msg, cause);
    }
}
