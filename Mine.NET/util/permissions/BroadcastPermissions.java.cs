package org.bukkit.util.permissions;

import org.bukkit.permissions.Permission;
import org.bukkit.permissions.PermissionDefault;

public sealed class BroadcastPermissions {
    private static readonly String ROOT = "bukkit.broadcast";
    private static readonly String PREFIX = ROOT + ".";

    private BroadcastPermissions() {}

    public static Permission registerPermissions(Permission parent) {
        Permission broadcasts = DefaultPermissions.registerPermission(ROOT, "Allows the user to receive all broadcast messages", parent);

        DefaultPermissions.registerPermission(PREFIX + "admin", "Allows the user to receive administrative broadcasts", PermissionDefault.OP, broadcasts);
        DefaultPermissions.registerPermission(PREFIX + "user", "Allows the user to receive user broadcasts", PermissionDefault.TRUE, broadcasts);

        broadcasts.recalculatePermissibles();

        return broadcasts;
    }
}
