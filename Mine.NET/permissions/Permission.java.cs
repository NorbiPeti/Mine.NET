using Mine.NET.plugin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET.permissions
{
    /**
     * Represents a unique permission that may be attached to a {@link
     * Permissible}
     */
    public class Permission {
        public static readonly PermissionDefaults DEFAULT_PERMISSION = PermissionDefaults.OP;

        private readonly String name;
        private readonly Dictionary<String, bool> children = new Dictionary<String, bool>();
        private PermissionDefaults defaultValue = DEFAULT_PERMISSION;
        private String description;

        public Permission(String name) : this(name, null, DEFAULT_PERMISSION, null) {
        }

        public Permission(String name, String description) : this(name, description, DEFAULT_PERMISSION, null) {
        }

        public Permission(String name, PermissionDefaults defaultValue) : this(name, null, defaultValue, null) {
        }

        public Permission(String name, String description, PermissionDefaults defaultValue) : this(name, description, defaultValue, null) {
        }

        public Permission(String name, Dictionary<String, bool> children) : this(name, null, DEFAULT_PERMISSION, children) {
        }

        public Permission(String name, String description, Dictionary<String, bool> children) : this(name, description, DEFAULT_PERMISSION, children) {
        }

        public Permission(String name, PermissionDefaults defaultValue, Dictionary<String, bool> children) : this(name, null, defaultValue, children) {
        }

        public Permission(String name, String description, PermissionDefaults defaultValue, Dictionary<String, bool> children) {
            if (name == null) throw new ArgumentNullException("Name cannot be null");
            this.name = name;
            this.description = (description == null) ? "" : description;

            this.defaultValue = defaultValue;

            if (children != null) {
                this.children.Concat(children).ToDictionary(kv => kv.Key, kv => kv.Value);
            }

            recalculatePermissibles();
        }

        /**
         * Returns the unique fully qualified name of this Permission
         *
         * @return Fully qualified name
         */
        public String getName() {
            return name;
        }

        /**
         * Gets the children of this permission.
         * <p>
         * If you change this map in any form, you must call {@link
         * #recalculatePermissibles()} to recalculate all {@link Permissible}s
         *
         * @return Permission children
         */
        public Dictionary<String, bool> getChildren() {
            return children;
        }

        /**
         * Gets the default value of this permission.
         *
         * @return Default value of this permission.
         */
        public PermissionDefaults getDefault() {
            return defaultValue;
        }

        /**
         * Sets the default value of this permission.
         * <p>
         * This will not be saved to disk, and is a temporary operation until the
         * server reloads permissions. Changing this default will cause all {@link
         * Permissible}s that contain this permission to recalculate their
         * permissions
         *
         * @param value The new default to set
         */
        public void setDefault(PermissionDefaults value) {
            defaultValue = value;
            recalculatePermissibles();
        }

        /**
         * Gets a brief description of this permission, if set
         *
         * @return Brief description of this permission
         */
        public String getDescription() {
            return description;
        }

        /**
         * Sets the description of this permission.
         * <p>
         * This will not be saved to disk, and is a temporary operation until the
         * server reloads permissions.
         *
         * @param value The new description to set
         */
        public void setDescription(String value) {
            if (value == null) {
                description = "";
            } else {
                description = value;
            }
        }

        /**
         * Gets a set containing every {@link Permissible} that has this
         * permission.
         * <p>
         * This set cannot be modified.
         *
         * @return Set containing permissibles with this permission
         */
        public HashSet<Permissible> getPermissibles() {
            return Bukkit.getServer().getPluginManager().getPermissionSubscriptions(name);
        }

        /**
         * Recalculates all {@link Permissible}s that contain this permission.
         * <p>
         * This should be called after modifying the children, and is
         * automatically called after modifying the default value
         */
        public void recalculatePermissibles() {
            HashSet<Permissible> perms = getPermissibles();

            Bukkit.getServer().getPluginManager().recalculatePermissionDefaults(this);

            foreach (Permissible p in perms) {
                p.recalculatePermissions();
            }
        }

        /**
         * Adds this permission to the specified parent permission.
         * <p>
         * If the parent permission does not exist, it will be created and
         * registered.
         *
         * @param name Name of the parent permission
         * @param value The value to set this permission to
         * @return Parent permission it created or loaded
         */
        public Permission addParent(String name, bool value) {
            PluginManager pm = Bukkit.getServer().getPluginManager();
            String lname = name.ToLower();

            Permission perm = pm.getPermission(lname);

            if (perm == null) {
                perm = new Permission(lname);
                pm.addPermission(perm);
            }

            addParent(perm, value);

            return perm;
        }

        /**
         * Adds this permission to the specified parent permission.
         *
         * @param perm Parent permission to register with
         * @param value The value to set this permission to
         */
        public void addParent(Permission perm, bool value) {
            perm.getChildren().Add(getName(), value);
            perm.recalculatePermissibles();
        }

        /**
         * Loads a list of Permissions from a map of data, usually used from
         * retrieval from a yaml file.
         * <p>
         * The data may contain a list of name:data, where the data contains the
         * following keys:
         * <ul>
         * <li>default: bool true or false. If not specified, false.
         * <li>children: {@code Dictionary<String, bool>} of child permissions. If not
         *     specified, empty list.
         * <li>description: Short string containing a very small description of
         *     this description. If not specified, empty string.
         * </ul>
         *
         * @param data Map of permissions
         * @param error An error message to show if a permission is invalid.
         * @param def Default permission value to use if missing
         * @return Permission object
         */
        public static List<Permission> loadPermissions(Dictionary<object, Dictionary<string, object>> data, String error, PermissionDefaults def)
        {
            List<Permission> result = new List<Permission>();

            foreach (KeyValuePair<object, Dictionary<string, object>> entry in data) {
                try {
                    result.Add(Permission.loadPermission(entry.Key.ToString(), entry.Value, def, result));
                } catch (Exception ex) {
                    Bukkit.getServer().getLogger().Severe(String.Format(error, entry.Key) + ex);
                }
            }
            return result;
        }

        /**
         * Loads a Permission from a map of data, usually used from retrieval from
         * a yaml file.
         * <p>
         * The data may contain the following keys:
         * <ul>
         * <li>default: bool true or false. If not specified, false.
         * <li>children: {@code Dictionary<String, bool>} of child permissions. If not
         *     specified, empty list.
         * <li>description: Short string containing a very small description of
         *     this description. If not specified, empty string.
         * </ul>
         *
         * @param name Name of the permission
         * @param data Map of keys
         * @return Permission object
         */
        public static Permission loadPermission(String name, Dictionary<String, Object> data) {
            return loadPermission(name, data, DEFAULT_PERMISSION, null);
        }

        /**
         * Loads a Permission from a map of data, usually used from retrieval from
         * a yaml file.
         * <p>
         * The data may contain the following keys:
         * <ul>
         * <li>default: bool true or false. If not specified, false.
         * <li>children: {@code Dictionary<String, bool>} of child permissions. If not
         *     specified, empty list.
         * <li>description: Short string containing a very small description of
         *     this description. If not specified, empty string.
         * </ul>
         *
         * @param name Name of the permission
         * @param data Map of keys
         * @param def Default permission value to use if not set
         * @param output A list to append any created child-Permissions to, may be null
         * @return Permission object
         */
        public static Permission loadPermission(String name, Dictionary<string, object> data, PermissionDefaults def, List<Permission> output) {
            if (name == null) throw new ArgumentNullException("Name cannot be null");
            if (data == null) throw new ArgumentNullException("Data cannot be null");

            String desc = null;
            Dictionary<String, bool> children = null;

            if (data["default"] != null) {
                PermissionDefaults value = PermissionDefault.getByName(data["default"].ToString());
                def = value;
            }

            if (data["children"] != null) {
                Object childrenNode = data["children"];
                if (childrenNode is IEnumerable)
                {
                    children = new Dictionary<String, bool>();
                    foreach (Object child in (IEnumerable)childrenNode)
                    {
                        if (child != null)
                        {
                            children.Add(child.ToString(), true);
                        }
                    }
                }
                else if (childrenNode is Dictionary<object, object>) //TODO
                {
                    children = extractChildren((Dictionary<object, object>)childrenNode, name, def, output);
                }
                else
                {
                    throw new ArgumentException("'children' key is of wrong type");
                }
            }

            if (data["description"] != null) {
                desc = data["description"].ToString();
            }

            return new Permission(name, desc, def, children);
        }

        private static Dictionary<String, bool> extractChildren(Dictionary<object, object> input, String name, PermissionDefaults def, List<Permission> output) {
            Dictionary<String, bool> children = new Dictionary<string, bool>();

            foreach (KeyValuePair<object, object> entry in input) {
                if ((entry.Value is bool)) {
                    children.Add(entry.Key.ToString(), (bool)entry.Value);
                } else if ((entry.Value is Dictionary<string, object>)) {
                    try {
                        Permission perm = loadPermission(entry.Key.ToString(), (Dictionary <string, object>) entry.Value, def, output);
                        children.Add(perm.getName(), true);

                        if (output != null) {
                            output.Add(perm);
                        }
                    } catch (Exception ex) {
                        throw new ArgumentException("Permission node '" + entry.Key.ToString() + "' in child of " + name + " is invalid", ex);
                    }
                } else {
                    throw new ArgumentException("Child '" + entry.Key.ToString() + "' contains invalid value");
                }
            }

            return children;
        }
    }
}
