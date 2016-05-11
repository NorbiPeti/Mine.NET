package org.bukkit.plugin;

/**
 * Represents a base {@link Plugin}
 * <p>
 * Extend this class if your plugin is not a {@link
 * org.bukkit.plugin.java.JavaPlugin}
 */
public abstract class PluginBase : Plugin {
    @Override
    public readonly int hashCode() {
        return getName().hashCode();
    }

    @Override
    public readonly bool equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj is Plugin)) {
            return false;
        }
        return getName().equals(((Plugin) obj).getName());
    }

    public readonly String getName() {
        return getDescription().getName();
    }
}
