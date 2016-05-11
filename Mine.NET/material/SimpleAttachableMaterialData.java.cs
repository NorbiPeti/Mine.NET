package org.bukkit.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Simple utility class for attachable MaterialData subclasses
 */
public abstract class SimpleAttachableMaterialData : MaterialData : Attachable {

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SimpleAttachableMaterialData(int type) {
        super(type);
    }

    public SimpleAttachableMaterialData(int type, BlockFace direction) {
        this(type);
        setFacingDirection(direction);
    }

    public SimpleAttachableMaterialData(Material type, BlockFace direction) {
        this(type);
        setFacingDirection(direction);
    }

    public SimpleAttachableMaterialData(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SimpleAttachableMaterialData(int type, byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SimpleAttachableMaterialData(Material type, byte data) {
        super(type, data);
    }

    public BlockFace getFacing() {
        BlockFace attachedFace = getAttachedFace();
        return attachedFace == null ? null : attachedFace.getOppositeFace();
    }

    public override string ToString() {
        return super.toString() + " facing " + getFacing();
    }

    @Override
    public SimpleAttachableMaterialData clone() {
        return (SimpleAttachableMaterialData) super.clone();
    }
}
