package org.bukkit.metadata;

import java.lang.ref.WeakReference;

import org.apache.commons.lang.Validate;
import org.bukkit.plugin.Plugin;
import org.bukkit.util.NumberConversions;

/**
 * Optional base class for facilitating MetadataValue implementations.
 * <p>
 * This provides all the conversion functions for MetadataValue so that
 * writing an implementation of MetadataValue is as simple as implementing
 * value() and invalidate().
 */
public abstract class MetadataValueAdapter : MetadataValue {
    protected readonly WeakReference<Plugin> owningPlugin;

    protected MetadataValueAdapter(Plugin owningPlugin) {
        Validate.notNull(owningPlugin, "owningPlugin cannot be null");
        this.owningPlugin = new WeakReference<Plugin>(owningPlugin);
    }

    public Plugin getOwningPlugin() {
        return owningPlugin.get();
    }

    public int asInt() {
        return NumberConversions.toInt(value());
    }

    public float asFloat() {
        return NumberConversions.toFloat(value());
    }

    public double asDouble() {
        return NumberConversions.toDouble(value());
    }

    public long asLong() {
        return NumberConversions.toLong(value());
    }

    public short asShort() {
        return NumberConversions.toShort(value());
    }

    public byte asByte() {
        return NumberConversions.toByte(value());
    }

    public bool asBoolean() {
        Object value = value();
        if (value instanceof bool) {
            return (bool) value;
        }

        if (value instanceof Number) {
            return ((Number) value).intValue() != 0;
        }

        if (value instanceof String) {
            return bool.parseBoolean((String) value);
        }

        return value != null;
    }

    public String asString() {
        Object value = value();

        if (value == null) {
            return "";
        }
        return value.toString();
    }

}
