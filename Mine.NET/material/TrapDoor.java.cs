package org.bukkit.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Represents a trap door
 */
public class TrapDoor : SimpleAttachableMaterialData : Openable {
    public TrapDoor() {
        super(Material.TRAP_DOOR);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TrapDoor(int type) {
        super(type);
    }

    public TrapDoor(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TrapDoor(int type, readonly byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TrapDoor(Material type, readonly byte data) {
        super(type, data);
    }

    public bool isOpen() {
        return ((getData() & 0x4) == 0x4);
    }

    public void setOpen(bool isOpen) {
        byte data = getData();

        if (isOpen) {
            data |= 0x4;
        } else {
            data &= ~0x4;
        }

        setData(data);
    }

    /**
     * Test if trapdoor is inverted
     *
     * @return true if inverted (top half), false if normal (bottom half)
     */
    public bool isInverted() {
        return ((getData() & 0x8) != 0);
    }

    /**
     * Set trapdoor inverted state
     *
     * @param inv - true if inverted (top half), false if normal (bottom half)
     */
    public void setInverted(bool inv) {
        int dat = getData() & 0x7;
        if (inv) {
            dat |= 0x8;
        }
        setData((byte) dat);
    }

    public BlockFace getAttachedFace() {
        byte data = (byte) (getData() & 0x3);

        switch (data) {
            case 0x0:
                return BlockFace.SOUTH;

            case 0x1:
                return BlockFace.NORTH;

            case 0x2:
                return BlockFace.EAST;

            case 0x3:
                return BlockFace.WEST;
        }

        return null;

    }

    public void setFacingDirection(BlockFace face) {
        byte data = (byte) (getData() & 0xC);

        switch (face) {
            case SOUTH:
                data |= 0x1;
                break;
            case WEST:
                data |= 0x2;
                break;
            case EAST:
                data |= 0x3;
                break;
        }

        setData(data);
    }

    public override string ToString() {
        return (isOpen() ? "OPEN " : "CLOSED ") + super.toString() + " with hinges set " + getAttachedFace() + (isInverted() ? " inverted" : "");
    }

    @Override
    public TrapDoor clone() {
        return (TrapDoor) super.clone();
    }
}
