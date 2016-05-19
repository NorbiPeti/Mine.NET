using System;

namespace Mine.NET.metadata
{
    /**
     * A MetadataEvaluationException is thrown any time a {@link
     * LazyMetadataValue} fails to evaluate its value due to an exception. The
     * originating exception will be included as this exception's cause.
     */
    public class MetadataEvaluationException : AggregateException
    {
        public MetadataEvaluationException(Exception cause) : base(cause)
        {
        }
    }
}
