using Mine.NET.permissions;
using System;

namespace Mine.NET.util.permissions
{
    public sealed class BroadcastPermissions
    {
        private static readonly String ROOT = "bukkit.broadcast";
        private static readonly String PREFIX = ROOT + ".";

        private BroadcastPermissions() { }

        public static Permission registerPermissions(Permission parent)
        {
            Permission broadcasts = DefaultPermissions.registerPermission(ROOT, "Allows the user to receive all broadcast messages", parent);

            DefaultPermissions.registerPermission(PREFIX + "admin", "Allows the user to receive administrative broadcasts", PermissionDefaults.OP, broadcasts);
            DefaultPermissions.registerPermission(PREFIX + "user", "Allows the user to receive user broadcasts", PermissionDefaults.TRUE, broadcasts);

            broadcasts.recalculatePermissibles();

            return broadcasts;
        }
    }
}
