using System;

namespace Mine.NET.plugin
{
    /**
     * Thrown when a plugin attempts to interact with the server when it is not
     * enabled
     */
    public class IllegalPluginAccessException : AggregateException
    {

        /**
         * Creates a new instance of <code>IllegalPluginAccessException</code>
         * without detail message.
         */
        public IllegalPluginAccessException() { }

        /**
         * Constructs an instance of <code>IllegalPluginAccessException</code>
         * with the specified detail message.
         *
         * @param msg the detail message.
         */
        public IllegalPluginAccessException(String msg) : base(msg)
        {
        }
    }
}
