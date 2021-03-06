using Mine.NET.permissions;
using System;
using System.Collections.Generic;

namespace Mine.NET.util.permissions
{
    public sealed class DefaultPermissions
    {
        private static readonly String ROOT = "craftbukkit";
        private static readonly String LEGACY_PREFIX = "craft";

        private DefaultPermissions() { }

        public static Permission registerPermission(Permission perm)
        {
            return registerPermission(perm, true);
        }

        public static Permission registerPermission(Permission perm, bool withLegacy)
        {
            Permission result = perm;

            try
            {
                Bukkit.getPluginManager().addPermission(perm);
            }
            catch (ArgumentException)
            {
                result = Bukkit.getPluginManager().getPermission(perm.getName());
            }

            if (withLegacy)
            {
                Permission legacy = new Permission(LEGACY_PREFIX + result.getName(), result.getDescription(), PermissionDefaults.FALSE);
                legacy.getChildren().Add(result.getName(), true);
                registerPermission(perm, false);
            }

            return result;
        }

        public static Permission registerPermission(Permission perm, Permission parent)
        {
            parent.getChildren().Add(perm.getName(), true);
            return registerPermission(perm);
        }

        public static Permission registerPermission(String name, String desc)
        {
            Permission perm = registerPermission(new Permission(name, desc));
            return perm;
        }

        public static Permission registerPermission(String name, String desc, Permission parent)
        {
            Permission perm = registerPermission(name, desc);
            parent.getChildren().Add(perm.getName(), true);
            return perm;
        }

        public static Permission registerPermission(String name, String desc, PermissionDefaults def)
        {
            Permission perm = registerPermission(new Permission(name, desc, def));
            return perm;
        }

        public static Permission registerPermission(String name, String desc, PermissionDefaults def, Permission parent)
        {
            Permission perm = registerPermission(name, desc, def);
            parent.getChildren().Add(perm.getName(), true);
            return perm;
        }

        public static Permission registerPermission(String name, String desc, PermissionDefaults def, Dictionary<String, bool> children)
        {
            Permission perm = registerPermission(new Permission(name, desc, def, children));
            return perm;
        }

        public static Permission registerPermission(String name, String desc, PermissionDefaults def, Dictionary<String, bool> children, Permission parent)
        {
            Permission perm = registerPermission(name, desc, def, children);
            parent.getChildren().Add(perm.getName(), true);
            return perm;
        }

        public static void registerCorePermissions()
        {
            Permission parent = registerPermission(ROOT, "Gives the user the ability to use all CraftBukkit utilities and commands");

            CommandPermissions.registerPermissions(parent);
            BroadcastPermissions.registerPermissions(parent);

            parent.recalculatePermissibles();
        }
    }
}
