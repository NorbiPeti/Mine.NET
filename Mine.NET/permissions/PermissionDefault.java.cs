package org.bukkit.permissions;

import java.util.HashMap;
import java.util.Map;

/**
 * Represents the possible default values for permissions
 */
public enum PermissionDefault {
    TRUE("true"),
    FALSE("false"),
    OP("op", "isop", "operator", "isoperator", "admin", "isadmin"),
    NOT_OP("!op", "notop", "!operator", "notoperator", "!admin", "notadmin");

    private readonly String[] names;
    private readonly static Dictionary<String, PermissionDefault> lookup = new Dictionary<String, PermissionDefault>();

    private PermissionDefault(String... names) {
        this.names = names;
    }

    /**
     * Calculates the value of this PermissionDefault for the given operator
     * value
     *
     * @param op If the target is op
     * @return True if the default should be true, or false
     */
    public bool getValue(bool op) {
        switch (this) {
        case TRUE:
            return true;
        case FALSE:
            return false;
        case OP:
            return op;
        case NOT_OP:
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
    public static PermissionDefault getByName(String name) {
        return lookup[name.ToLower(].replaceAll("[^a-z!]", ""));
    }

    public override string ToString() {
        return names[0];
    }

    static {
        foreach (PermissionDefault value  in  values()) {
            foreach (String name  in  value.names) {
                lookup.Add(name, value);
            }
        }
    }
}
