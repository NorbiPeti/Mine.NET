using Mine.NET.permissions;
using Mine.NET.plugin;
using System;
using System.Collections.Generic;

namespace Mine.NET
{
    /**
     * Represents an object that may be assigned permissions
     */
    public interface Permissible : ServerOperator
    {

        /**
         * Checks if this object contains an override for the specified
         * permission, by fully qualified name
         *
         * @param name Name of the permission
         * @return true if the permission is set, otherwise false
         */
        bool isPermissionSet(String name);

        /**
         * Checks if this object contains an override for the specified {@link
         * Permission}
         *
         * @param perm Permission to check
         * @return true if the permission is set, otherwise false
         */
        bool isPermissionSet(Permission perm);

        /**
         * Gets the value of the specified permission, if set.
         * <p>
         * If a permission override is not set on this object, the default value
         * of the permission will be returned.
         *
         * @param name Name of the permission
         * @return Value of the permission
         */
        bool hasPermission(String name);

        /**
         * Gets the value of the specified permission, if set.
         * <p>
         * If a permission override is not set on this object, the default value
         * of the permission will be returned
         *
         * @param perm Permission to get
         * @return Value of the permission
         */
        bool hasPermission(Permission perm);

        /**
         * Adds a new {@link PermissionAttachment} with a single permission by
         * name and value
         *
         * @param plugin Plugin responsible for this attachment, may not be null
         *     or disabled
         * @param name Name of the permission to attach
         * @param value Value of the permission
         * @return The PermissionAttachment that was just created
         */
        PermissionAttachment addAttachment(Plugin plugin, String name, bool value);

        /**
         * Adds a new empty {@link PermissionAttachment} to this object
         *
         * @param plugin Plugin responsible for this attachment, may not be null
         *     or disabled
         * @return The PermissionAttachment that was just created
         */
        PermissionAttachment addAttachment(Plugin plugin);

        /**
         * Temporarily adds a new {@link PermissionAttachment} with a single
         * permission by name and value
         *
         * @param plugin Plugin responsible for this attachment, may not be null
         *     or disabled
         * @param name Name of the permission to attach
         * @param value Value of the permission
         * @param ticks Amount of ticks to automatically remove this attachment
         *     after
         * @return The PermissionAttachment that was just created
         */
        PermissionAttachment addAttachment(Plugin plugin, String name, bool value, int ticks);

        /**
         * Temporarily adds a new empty {@link PermissionAttachment} to this
         * object
         *
         * @param plugin Plugin responsible for this attachment, may not be null
         *     or disabled
         * @param ticks Amount of ticks to automatically remove this attachment
         *     after
         * @return The PermissionAttachment that was just created
         */
        PermissionAttachment addAttachment(Plugin plugin, int ticks);

        /**
         * Removes the given {@link PermissionAttachment} from this object
         *
         * @param attachment Attachment to remove
         * @throws ArgumentException Thrown when the specified attachment
         *     isn't part of this object
         */
        void removeAttachment(PermissionAttachment attachment);

        /**
         * Recalculates the permissions for this object, if the attachments have
         * changed values.
         * <p>
         * This should very rarely need to be called from a plugin.
         */
        void recalculatePermissions();

        /**
         * Gets a set containing all of the permissions currently in effect by
         * this object
         *
         * @return Set of currently effective permissions
         */
        HashSet<PermissionAttachmentInfo> getEffectivePermissions();
    }
}
