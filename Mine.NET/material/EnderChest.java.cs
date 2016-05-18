namespace Mine.NET.material;

import org.bukkit.Materials;
import org.bukkit.block.BlockFaces;

/**
 * Represents an ender chest
 */
public class EnderChest : DirectionalContainer {

    public EnderChest() {
        base(Materials.ENDER_CHEST);
    }

    /**
     * Instantiate an ender chest facing in a particular direction.
     *
     * @param direction the direction the ender chest's lid opens towards
     */
    public EnderChest(BlockFaces direction) {
        this();
        setFacingDirection(direction);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public EnderChest(int type) {
        base(type);
    }

    public EnderChest(Materials type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public EnderChest(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public EnderChest(Materials type, readonly byte data) {
        base(type, data);
    }

    public override EnderChest clone() {
        return (EnderChest) base.clone();
    }
}
