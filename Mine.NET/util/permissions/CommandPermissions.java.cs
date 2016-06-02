using Mine.NET.permissions;
using System;

namespace Mine.NET.util.permissions
{
    public sealed class CommandPermissions
    {
        private static readonly String ROOT = "bukkit.command";
        private static readonly String PREFIX = ROOT + ".";

        private CommandPermissions() { }

        public static Permission registerPermissions(Permission parent)
        {
            Permission commands = DefaultPermissions.registerPermission(ROOT, "Gives the user the ability to use all CraftBukkit commands", parent);

            DefaultPermissions.registerPermission(PREFIX + "help", "Allows the user to view the vanilla help menu", PermissionDefaults.TRUE, commands);
            DefaultPermissions.registerPermission(PREFIX + "plugins", "Allows the user to view the list of plugins running on this server", PermissionDefaults.TRUE, commands);
            DefaultPermissions.registerPermission(PREFIX + "reload", "Allows the user to reload the server settings", PermissionDefaults.OP, commands);
            DefaultPermissions.registerPermission(PREFIX + "version", "Allows the user to view the version of the server", PermissionDefaults.TRUE, commands);

            commands.recalculatePermissibles();
            return commands;
        }
    }
}
