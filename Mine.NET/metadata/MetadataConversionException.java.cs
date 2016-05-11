package org.bukkit.metadata;

/**
 * A MetadataConversionException is thrown any time a {@link
 * LazyMetadataValue} attempts to convert a metadata value to an inappropriate
 * data type.
 */
@SuppressWarnings("serial")
public class MetadataConversionException : RuntimeException {
    MetadataConversionException(String message) {
        base(message);
    }
}
