namespace Mine.NET.material;

import org.bukkit.block.BlockFace;
import org.bukkit.Material;

/**
 * Represents a button
 */
public class Button : SimpleAttachableMaterialData : Redstone {
    public Button() {
        base(Material.STONE_BUTTON);
    }

    /**
     * @param type the type
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Button(int type) {
        base(type);
    }

    public Button(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Button(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Button(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Gets the current state of this Material, indicating if it's powered or
     * unpowered
     *
     * @return true if powered, otherwise false
     */
    public bool isPowered() {
        return (getData() & 0x8) == 0x8;
    }

    /**
     * Sets the current state of this button
     *
     * @param bool
     *            whether or not the button is powered
     */
    public void setPowered(bool bool) {
        setData((byte) (bool ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    /**
     * Gets the face that this block is attached on
     *
     * @return BlockFace attached to
     */
    public BlockFace getAttachedFace() {
        byte data = (byte) (getData() & 0x7);

        switch (data) {
        case 0x0:
            return BlockFace.UP;

        case 0x1:
            return BlockFace.WEST;

        case 0x2:
            return BlockFace.EAST;

        case 0x3:
            return BlockFace.NORTH;

        case 0x4:
            return BlockFace.SOUTH;

        case 0x5:
            return BlockFace.DOWN;
        }

        return null;
    }

    /**
     * Sets the direction this button is pointing toward
     */
    public void setFacingDirection(BlockFace face) {
        byte data = (byte) (getData() & 0x8);

        switch (face) {
        case DOWN:
            data |= 0x0;
            break;

        case EAST:
            data |= 0x1;
            break;

        case WEST:
            data |= 0x2;
            break;

        case SOUTH:
            data |= 0x3;
            break;

        case NORTH:
            data |= 0x4;
            break;

        case UP:
            data |= 0x5;
            break;
        }

        setData(data);
    }

    public override string ToString() {
        return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
    }

    public override Button clone() {
        return (Button) base.clone();
    }
}
