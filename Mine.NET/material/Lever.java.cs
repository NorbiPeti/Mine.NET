package org.bukkit.material;

import org.bukkit.block.BlockFace;
import org.bukkit.Material;

/**
 * Represents a lever
 */
public class Lever : SimpleAttachableMaterialData : Redstone {
    public Lever() {
        base(Material.LEVER);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Lever(int type) {
        base(type);
    }

    public Lever(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Lever(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Lever(Material type, readonly byte data) {
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
     * @return BlockFace attached to
     */
    public BlockFace getAttachedFace() {
        byte data = (byte) (getData() & 0x7);

        switch (data) {
        case 0x1:
            return BlockFace.WEST;

        case 0x2:
            return BlockFace.EAST;

        case 0x3:
            return BlockFace.NORTH;

        case 0x4:
            return BlockFace.SOUTH;

        case 0x5:
        case 0x6:
            return BlockFace.DOWN;

        case 0x0:
        case 0x7:
            return BlockFace.UP;

        }

        return null;
    }

    /**
     * Sets the direction this lever is pointing in
     */
    public void setFacingDirection(BlockFace face) {
        byte data = (byte) (getData() & 0x8);
        BlockFace attach = getAttachedFace();

        if (attach == BlockFace.DOWN) {
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
        } else if (attach == BlockFace.UP) {
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
