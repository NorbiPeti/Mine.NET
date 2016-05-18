namespace Mine.NET.permissions{

/**
 * Base Permissible for use in any Permissible object via proxy or extension
 */
public class PermissibleBase : Permissible {
    private ServerOperator opable = null;
    private Permissible parent = this;
    private readonly List<PermissionAttachment> attachments = new LinkedList<PermissionAttachment>();
    private readonly Dictionary<String, PermissionAttachmentInfo> permissions = new Dictionary<String, PermissionAttachmentInfo>();

    public PermissibleBase(ServerOperator opable) {
        this.opable = opable;

        if (opable is Permissible) {
            this.parent = (Permissible) opable;
        }

        recalculatePermissions();
    }

    public bool isOp() {
        if (opable == null) {
            return false;
        } else {
            return opable.isOp();
        }
    }

    public void setOp(bool value) {
        if (opable == null) {
            throw new UnsupportedOperationException("Cannot change op value as no ServerOperator is set");
        } else {
            opable.setOp(value);
        }
    }

    public bool isPermissionSet(String name) {
        if (name == null) {
            throw new ArgumentException("Permission name cannot be null");
        }

        return permissions.containsKey(name.ToLower());
    }

    public bool isPermissionSet(Permission perm) {
        if (perm == null) {
            throw new ArgumentException("Permission cannot be null");
        }

        return isPermissionSet(perm.getName());
    }

    public bool hasPermission(String inName) {
        if (inName == null) {
            throw new ArgumentException("Permission name cannot be null");
        }

        String name = inName.ToLower();

        if (isPermissionSet(name)) {
            return permissions[name].Value;
        } else {
            Permission perm = Bukkit.getServer().getPluginManager().getPermission(name);

            if (perm != null) {
                return perm.getDefault().getValue(isOp());
            } else {
                return Permission.DEFAULT_PERMISSION.getValue(isOp());
            }
        }
    }

    public bool hasPermission(Permission perm) {
        if (perm == null) {
            throw new ArgumentException("Permission cannot be null");
        }

        String name = perm.getName().ToLower();

        if (isPermissionSet(name)) {
            return permissions[name].Value;
        }
        return perm.getDefault().getValue(isOp());
    }

    public PermissionAttachment addAttachment(Plugin plugin, String name, bool value) {
        if (name == null) {
            throw new ArgumentException("Permission name cannot be null");
        } else if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        } else if (!plugin.isEnabled()) {
            throw new ArgumentException("Plugin " + plugin.getDescription().getFullName() + " is disabled");
        }

        PermissionAttachment result = addAttachment(plugin);
        result.setPermission(name, value);

        recalculatePermissions();

        return result;
    }

    public PermissionAttachment addAttachment(Plugin plugin) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        } else if (!plugin.isEnabled()) {
            throw new ArgumentException("Plugin " + plugin.getDescription().getFullName() + " is disabled");
        }

        PermissionAttachment result = new PermissionAttachment(plugin, parent);

        attachments.add(result);
        recalculatePermissions();

        return result;
    }

    public void removeAttachment(PermissionAttachment attachment) {
        if (attachment == null) {
            throw new ArgumentException("Attachment cannot be null");
        }

        if (attachments.contains(attachment)) {
            attachments.remove(attachment);
            PermissionRemovedExecutor ex = attachment.getRemovalCallback();

            if (ex != null) {
                ex.attachmentRemoved(attachment);
            }

            recalculatePermissions();
        } else {
            throw new ArgumentException("Given attachment is not part of Permissible object " + parent);
        }
    }

    public void recalculatePermissions() {
        clearPermissions();
        HashSet<Permission> defaults = Bukkit.getServer().getPluginManager().getDefaultPermissions(isOp());
        Bukkit.getServer().getPluginManager().subscribeToDefaultPerms(isOp(), parent);

        foreach (Permission perm  in  defaults) {
            String name = perm.getName().ToLower();
            permissions.Add(name, new PermissionAttachmentInfo(parent, name, null, true));
            Bukkit.getServer().getPluginManager().subscribeToPermission(name, parent);
            calculateChildPermissions(perm.getChildren(), false, null);
        }

        foreach (PermissionAttachment attachment  in  attachments) {
            calculateChildPermissions(attachment.getPermissions(), false, attachment);
        }
    }

    public synchronized void clearPermissions() {
        HashSet<String> perms = permissions.keySet();

        foreach (String name  in  perms) {
            Bukkit.getServer().getPluginManager().unsubscribeFromPermission(name, parent);
        }

        Bukkit.getServer().getPluginManager().unsubscribeFromDefaultPerms(false, parent);
        Bukkit.getServer().getPluginManager().unsubscribeFromDefaultPerms(true, parent);

        permissions.clear();
    }

    private void calculateChildPermissions(Dictionary<String, bool> children, bool invert, PermissionAttachment attachment) {
        HashSet<String> keys = children.keySet();

        foreach (String name  in  keys) {
            Permission perm = Bukkit.getServer().getPluginManager().getPermission(name);
            bool value = children[name] ^ invert;
            String lname = name.ToLower();

            permissions.Add(lname, new PermissionAttachmentInfo(parent, lname, attachment, value));
            Bukkit.getServer().getPluginManager().subscribeToPermission(name, parent);

            if (perm != null) {
                calculateChildPermissions(perm.getChildren(), !value, attachment);
            }
        }
    }

    public PermissionAttachment addAttachment(Plugin plugin, String name, bool value, int ticks) {
        if (name == null) {
            throw new ArgumentException("Permission name cannot be null");
        } else if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        } else if (!plugin.isEnabled()) {
            throw new ArgumentException("Plugin " + plugin.getDescription().getFullName() + " is disabled");
        }

        PermissionAttachment result = addAttachment(plugin, ticks);

        if (result != null) {
            result.setPermission(name, value);
        }

        return result;
    }

    public PermissionAttachment addAttachment(Plugin plugin, int ticks) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        } else if (!plugin.isEnabled()) {
            throw new ArgumentException("Plugin " + plugin.getDescription().getFullName() + " is disabled");
        }

        PermissionAttachment result = addAttachment(plugin);

        if (Bukkit.getServer().getScheduler().scheduleSyncDelayedTask(plugin, new RemoveAttachmentRunnable(result), ticks) == -1) {
            Bukkit.getServer().getLogger().log(Level.WARNING, "Could not add PermissionAttachment to " + parent + " for plugin " + plugin.getDescription().getFullName() + ": Scheduler returned -1");
            result.remove();
            return null;
        } else {
            return result;
        }
    }

    public HashSet<PermissionAttachmentInfo> getEffectivePermissions() {
        return new HashSet<PermissionAttachmentInfo>(permissions.values());
    }

    private class RemoveAttachmentRunnable : Runnable {
        private PermissionAttachment attachment;

        public RemoveAttachmentRunnable(PermissionAttachment attachment) {
            this.attachment = attachment;
        }

        public void run() {
            attachment.remove();
        }
    }
}
