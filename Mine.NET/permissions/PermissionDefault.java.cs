using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mine.NET.permissions {

    /**
     * Represents the possible default values for permissions
     */
    public enum PermissionDefaults {
        TRUE,
        FALSE,
        OP,
        NOT_OP
    }

    public static class PermissionDefault
    {
        private readonly static Dictionary<string, PermissionDefaults> lookup = new Dictionary<string, PermissionDefaults>
        {
            { "true", PermissionDefaults.TRUE },
            { "false", PermissionDefaults.FALSE },
            { "op", PermissionDefaults.OP },
            { "isop", PermissionDefaults.OP },
            { "operator", PermissionDefaults.OP },
            { "isoperator", PermissionDefaults.OP },
            { "admin", PermissionDefaults.OP },
            { "isadmin", PermissionDefaults.OP },
            { "!op", PermissionDefaults.NOT_OP },
            { "notop", PermissionDefaults.NOT_OP },
            { "!operator", PermissionDefaults.NOT_OP },
            { "notoperator", PermissionDefaults.NOT_OP },
            { "!admin", PermissionDefaults.NOT_OP },
            { "notadmin", PermissionDefaults.NOT_OP }
        };

        /**
         * Calculates the value of this PermissionDefault for the given operator
         * value
         *
         * @param op If the target is op
         * @return True if the default should be true, or false
         */
        public static bool getValue(PermissionDefaults pd, bool op) {
            switch (pd) {
                case PermissionDefaults.TRUE:
                    return true;
                case PermissionDefaults.FALSE:
                    return false;
                case PermissionDefaults.OP:
                    return op;
                case PermissionDefaults.NOT_OP:
                    return !op;
                default:
                    return false;
            }
        }

        /**
         * Looks up a PermissionDefault by name
         *
         * @param name Name of the default
         * @return Specified value, or null if not found
         */
        public static PermissionDefaults getByName(String name) {
            return lookup[Regex.Replace(name.ToLower(), "[^a-z!]", "")];
        }
    }
}
