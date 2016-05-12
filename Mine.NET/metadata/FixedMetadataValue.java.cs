package org.bukkit.metadata;

import org.bukkit.plugin.Plugin;

import java.util.concurrent.Callable;

/**
 * A FixedMetadataValue is a special case metadata item that contains the same
 * value forever after initialization. Invalidating a FixedMetadataValue has
 * no effect.
 * <p>
 * This class : LazyMetadataValue for historical reasons, even though it
 * overrides all the implementation methods. it is possible that in the future
 * that the inheritance hierarchy may change.
 */
public class FixedMetadataValue : LazyMetadataValue {

    /**
     * Store the internal value that is represented by this fixed value.
     */
    private readonly Object internalValue;

    /**
     * Initializes a FixedMetadataValue with an Object
     *
     * @param owningPlugin the {@link Plugin} that created this metadata value
     * @param value the value assigned to this metadata value
     */
    public FixedMetadataValue(Plugin owningPlugin, readonly Object value) {
        base(owningPlugin);
        this.internalValue = value;
    }

    public override void invalidate() {

    }

    public override Object value() {
        return internalValue;
    }
}
