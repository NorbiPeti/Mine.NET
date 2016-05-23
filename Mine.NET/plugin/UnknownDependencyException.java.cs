using System;

namespace Mine.NET.plugin
{
    /**
     * Thrown when attempting to load an invalid Plugin file
     */
    public class UnknownDependencyException : AggregateException
    {

        private static readonly long serialVersionUID = 5721389371901775895L;

        /** 
         * Constructs a new UnknownDependencyException based on the given
         * Exception
         *
         * @param throwable Exception that triggered this Exception
         */
        public UnknownDependencyException(Exception throwable) : base(throwable)
        {
        }

        /**
         * Constructs a new UnknownDependencyException with the given message
         *
         * @param message Brief message explaining the cause of the exception
         */
        public UnknownDependencyException(String message) : base(message)
        {
        }

        /**
         * Constructs a new UnknownDependencyException based on the given
         * Exception
         *
         * @param message Brief message explaining the cause of the exception
         * @param throwable Exception that triggered this Exception
         */
        public UnknownDependencyException(Exception throwable, String message) : base(message, throwable)
        {
        }

        /**
         * Constructs a new UnknownDependencyException
         */
        public UnknownDependencyException()
        {

        }
    }
}
