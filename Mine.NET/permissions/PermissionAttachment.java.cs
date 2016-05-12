package org.bukkit.permissions;

import java.util.LinkedHashMap;
import java.util.Map;
import org.bukkit.plugin.Plugin;

/**
 * Holds information about a permission attachment on a {@link Permissible}
 * object
 */
public class PermissionAttachment {
    private PermissionRemovedExecutor removed;
    private readonly Dictionary<String, bool> permissions = new LinkedHashMap<String, bool>();
    private readonly Permissible permissible;
    private readonly Plugin plugin;

    public PermissionAttachment(Plugin plugin, Permissible Permissible) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        } else if (!plugin.isEnabled()) {
            throw new ArgumentException("Plugin " + plugin.getDescription().getFullName() + " is disabled");
        }

        this.permissible = Permissible;
        this.plugin = plugin;
    }

    /**
     * Gets the plugin responsible for this attachment
     *
     * @return Plugin responsible for this permission attachment
     */
    public Plugin getPlugin() {
        return plugin;
    }

    /**
     * Sets an object to be called for when this attachment is removed from a
     * {@link Permissible}. May be null.
     *
     * @param ex Object to be called when this is removed
     */
    public void setRemovalCallback(PermissionRemovedExecutor ex) {
        removed = ex;
    }

    /**
     * Gets the class that was previously set to be called when this
     * attachment was removed from a {@link Permissible}. May be null.
     *
     * @return Object to be called when this is removed
     */
    public PermissionRemovedExecutor getRemovalCallback() {
        return removed;
    }

    /**
     * Gets the Permissible that this is attached to
     *
     * @return Permissible containing this attachment
     */
    public Permissible getPermissible() {
        return permissible;
    }

    /**
     * Gets a copy of all set permissions and values contained within this
     * attachment.
     * <p>
     * This map may be modified but will not affect the attachment, as it is a
     * copy.
     *
     * @return Copy of all permissions and values expressed by this attachment
     */
    public Dictionary<String, bool> getPermissions() {
        return new LinkedHashMap<String, bool>(permissions);
    }

    /**
     * Sets a permission to the given value, by its fully qualified name
     *
     * @param name Name of the permission
     * @param value New value of the permission
     */
    public void setPermission(String name, bool value) {
        permissions.Add(name.toLowerCase(), value);
        permissible.recalculatePermissions();
    }

    /**
     * Sets a permission to the given value
     *
     * @param perm Permission to set
     * @param value New value of the permission
     */
    public void setPermission(Permission perm, bool value) {
        setPermission(perm.getName(), value);
    }

    /**
     * Removes the specified permission from this attachment.
     * <p>
     * If the permission does not exist in this attachment, nothing will
     * happen.
     *
     * @param name Name of the permission to remove
     */
    public void unsetPermission(String name) {
        permissions.remove(name.toLowerCase());
        permissible.recalculatePermissions();
    }

    /**
     * Removes the specified permission from this attachment.
     * <p>
     * If the permission does not exist in this attachment, nothing will
     * happen.
     *
     * @param perm Permission to remove
     */
    public void unsetPermission(Permission perm) {
        unsetPermission(perm.getName());
    }

    /**
     * Removes this attachment from its registered {@link Permissible}
     *
     * @return true if the permissible was removed successfully, false if it
     *     did not exist
     */
    public bool remove() {
        try {
            permissible.removeAttachment(this);
            return true;
        } catch (ArgumentException ex) {
            return false;
        }
    }
}
