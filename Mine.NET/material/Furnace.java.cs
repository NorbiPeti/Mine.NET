namespace Mine.NET.material;

import org.bukkit.Materials;
import org.bukkit.block.BlockFaces;

/**
 * Represents a furnace.
 */
public class Furnace : FurnaceAndDispenser {

    public Furnace() {
        base(Materials.FURNACE);
    }

    /**
     * Instantiate a furnace facing in a particular direction.
     *
     * @param direction the direction the furnace's "opening" is facing
     */
    public Furnace(BlockFaces direction) {
        this();
        setFacingDirection(direction);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Furnace(int type) {
        base(type);
    }

    public Furnace(Materials type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Furnace(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Furnace(Materials type, readonly byte data) {
        base(type, data);
    }

    public override Furnace clone() {
        return (Furnace) base.clone();
    }
}
