using System;

namespace Mine.NET
{
    /**
     * Thrown when an unhandled exception occurs during the execution of a Command
     */
    public class CommandException : Exception
    {

        /**
         * Creates a new instance of <code>CommandException</code> without detail
         * message.
         */
        public CommandException() { }

        /**
         * Constructs an instance of <code>CommandException</code> with the
         * specified detail message.
         *
         * @param msg the detail message.
         */
        public CommandException(String msg) : base(msg)
        {
        }

        public CommandException(String msg, Exception cause) : base(msg, cause)
        {
        }
    }
}
