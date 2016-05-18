namespace Mine.NET.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFaces;

/**
 * Represents stairs.
 */
public class Stairs : MaterialData : Directional {

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Stairs(int type) {
        base(type);
    }

    public Stairs(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Stairs(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Stairs(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * @return the direction the stairs ascend towards
     */
    public BlockFaces getAscendingDirection() {
        byte data = getData();

        switch (data & 0x3) {
        case 0x0:
        default:
            return BlockFaces.EAST;

        case 0x1:
            return BlockFaces.WEST;

        case 0x2:
            return BlockFaces.SOUTH;

        case 0x3:
            return BlockFaces.NORTH;
        }
    }

    /**
     * @return the direction the stairs descend towards
     */
    public BlockFaces getDescendingDirection() {
        return getAscendingDirection().getOppositeFace();
    }

    /**
     * Set the direction the stair part of the block is facing
     */
    public void setFacingDirection(BlockFaces face) {
        byte data;

        switch (face) {
        case NORTH:
            data = 0x3;
            break;

        case SOUTH:
            data = 0x2;
            break;

        case EAST:
        default:
            data = 0x0;
            break;

        case WEST:
            data = 0x1;
            break;
        }

        setData((byte) ((getData() & 0xC) | data));
    }

    /**
     * @return the direction the stair part of the block is facing
     */
    public BlockFaces getFacing() {
        return getDescendingDirection();
    }

    /**
     * Test if step is inverted
     *
     * @return true if inverted (top half), false if normal (bottom half)
     */
    public bool isInverted() {
        return ((getData() & 0x4) != 0);
    }

    /**
     * Set step inverted state
     *
     * @param inv - true if step is inverted (top half), false if step is
     *     normal (bottom half)
     */
    public void setInverted(bool inv) {
        int dat = getData() & 0x3;
        if (inv) {
            dat |= 0x4;
        }
        setData((byte) dat);
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing() + (isInverted()?" inverted":"");
    }

    public override Stairs clone() {
        return (Stairs) base.clone();
    }
}
