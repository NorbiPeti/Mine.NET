namespace Mine.NET.material;

import org.bukkit.Materials;
import org.bukkit.block.BlockFaces;

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
        base(type);
    }

    public SimpleAttachableMaterialData(int type, BlockFaces direction) {
        this(type);
        setFacingDirection(direction);
    }

    public SimpleAttachableMaterialData(Materials type, BlockFaces direction) {
        this(type);
        setFacingDirection(direction);
    }

    public SimpleAttachableMaterialData(Materials type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SimpleAttachableMaterialData(int type, byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SimpleAttachableMaterialData(Materials type, byte data) {
        base(type, data);
    }

    public BlockFaces getFacing() {
        BlockFaces attachedFace = getAttachedFace();
        return attachedFace == null ? null : attachedFace.getOppositeFace();
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing();
    }

    public override SimpleAttachableMaterialData clone() {
        return (SimpleAttachableMaterialData) base.clone();
    }
}
