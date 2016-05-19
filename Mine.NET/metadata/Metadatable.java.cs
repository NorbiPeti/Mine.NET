using Mine.NET.plugin;
using System;
using System.Collections.Generic;

namespace Mine.NET.metadata
{
    /**
     * This interface is implemented by all objects that can provide metadata
     * about themselves.
     */
    public interface Metadatable
    {
        /**
         * Sets a metadata value in the implementing object's metadata store.
         *
         * @param metadataKey A unique key to identify this metadata.
         * @param newMetadataValue The metadata value to apply.
         * @throws ArgumentException If value is null, or the owning plugin
         *     is null
         */
        void setMetadata(String metadataKey, MetadataValue newMetadataValue);

        /**
         * Returns a list of previously set metadata values from the implementing
         * object's metadata store.
         *
         * @param metadataKey the unique metadata key being sought.
         * @return A list of values, one for each plugin that has set the
         *     requested value.
         */
        List<MetadataValue> getMetadata(String metadataKey);

        /**
         * Tests to see whether the implementing object contains the given
         * metadata value in its metadata store.
         *
         * @param metadataKey the unique metadata key being queried.
         * @return the existence of the metadataKey within subject.
         */
        bool hasMetadata(String metadataKey);

        /**
         * Removes the given metadata value from the implementing object's
         * metadata store.
         *
         * @param metadataKey the unique metadata key identifying the metadata to
         *     remove.
         * @param owningPlugin This plugin's metadata value will be removed. All
         *     other values will be left untouched.
         * @throws ArgumentException If plugin is null
         */
        void removeMetadata(String metadataKey, Plugin owningPlugin);
    }
}
