namespace Mine.NET.material;

import org.bukkit.block.BlockFaces;
import org.bukkit.Materials;

/**
 * Represents a lever
 */
public class Lever : SimpleAttachableMaterialData : Redstone {
    public Lever() : base(Materials.LEVER) {
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Lever(int type) : base(type) {
    }

    public Lever(Materials type) : base(type) {
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Lever(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Lever(Materials type, readonly byte data) : base(type, data) {
    }

    /**
     * Gets the current state of this Materials, indicating if it's powered or
     * unpowered
     *
     * @return true if powered, otherwise false
     */
    public bool isPowered() {
        return (getData() & 0x8) == 0x8;
    }

    /**
     * Set this lever to be powered or not.
     *
     * @param isPowered whether the lever should be powered or not
     */
    public void setPowered(bool isPowered) {
        setData((byte) (isPowered ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    /**
     * Gets the face that this block is attached on
     *
     * @return BlockFaces attached to
     */
    public BlockFaces getAttachedFace() {
        byte data = (byte) (getData() & 0x7);

        switch (data) {
        case 0x1:
            return BlockFaces.WEST;

        case 0x2:
            return BlockFaces.EAST;

        case 0x3:
            return BlockFaces.NORTH;

        case 0x4:
            return BlockFaces.SOUTH;

        case 0x5:
        case 0x6:
            return BlockFaces.DOWN;

        case 0x0:
        case 0x7:
            return BlockFaces.UP;

        }

        return null;
    }

    /**
     * Sets the direction this lever is pointing in
     */
    public void setFacingDirection(BlockFaces face) {
        byte data = (byte) (getData() & 0x8);
        BlockFaces attach = getAttachedFace();

        if (attach == BlockFaces.DOWN) {
            switch (face) {
            case SOUTH:
            case NORTH:
                data |= 0x5;
                break;

            case EAST:
            case WEST:
                data |= 0x6;
                break;
            }
        } else if (attach == BlockFaces.UP) {
            switch (face) {
            case SOUTH:
            case NORTH:
                data |= 0x7;
                break;

            case EAST:
            case WEST:
                data |= 0x0;
                break;
            }
        } else {
            switch (face) {
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
            }
        }
        setData(data);
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
    }

    public override Lever clone() {
        return (Lever) base.clone();
    }
}
