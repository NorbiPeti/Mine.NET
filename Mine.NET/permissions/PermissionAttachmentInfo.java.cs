namespace Mine.NET.permissions;

/**
 * Holds information on a permission and which {@link PermissionAttachment}
 * provides it
 */
public class PermissionAttachmentInfo {
    private readonly Permissible permissible;
    private readonly String permission;
    private readonly PermissionAttachment attachment;
    private readonly bool value;

    public PermissionAttachmentInfo(Permissible permissible, String permission, PermissionAttachment attachment, bool value) {
        if (permissible == null) {
            throw new ArgumentException("Permissible may not be null");
        } else if (permission == null) {
            throw new ArgumentException("Permissions may not be null");
        }

        this.permissible = permissible;
        this.permission = permission;
        this.attachment = attachment;
        this.value = value;
    }

    /**
     * Gets the permissible this is attached to
     *
     * @return Permissible this permission is for
     */
    public Permissible getPermissible() {
        return permissible;
    }

    /**
     * Gets the permission being set
     *
     * @return Name of the permission
     */
    public String getPermission() {
        return permission;
    }

    /**
     * Gets the attachment providing this permission. This may be null for
     * default permissions (usually parent permissions).
     *
     * @return Attachment
     */
    public PermissionAttachment getAttachment() {
        return attachment;
    }

    /**
     * Gets the value of this permission
     *
     * @return Value of the permission
     */
    public bool getValue() {
        return value;
    }
}
