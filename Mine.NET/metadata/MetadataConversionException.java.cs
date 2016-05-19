using System;

namespace Mine.NET.metadata
{
    /**
     * A MetadataConversionException is thrown any time a {@link
     * LazyMetadataValue} attempts to convert a metadata value to an inappropriate
     * data type.
     */
    public class MetadataConversionException : AggregateException
    {
        MetadataConversionException(String message) : base(message)
        {
        }
    }
}
