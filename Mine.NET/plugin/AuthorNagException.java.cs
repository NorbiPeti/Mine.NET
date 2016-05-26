using System;

namespace Mine.NET.plugin
{
    public class AuthorNagException : AggregateException
    {
        /**
         * Constructs a new AuthorNagException based on the given Exception
         *
         * @param message Brief message explaining the cause of the exception
         */
        public AuthorNagException(String message) : base(message)
        {
        }
    }
}
