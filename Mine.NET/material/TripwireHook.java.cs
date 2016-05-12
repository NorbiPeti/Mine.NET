package org.bukkit.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Represents the tripwire hook
 */
public class TripwireHook : SimpleAttachableMaterialData : Redstone {

    public TripwireHook() {
        base(Material.TRIPWIRE_HOOK);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TripwireHook(int type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TripwireHook(int type, readonly byte data) {
        base(type, data);
    }

    public TripwireHook(BlockFace dir) {
        this();
        setFacingDirection(dir);
    }

    /**
     * Test if tripwire is connected
     *
     * @return true if connected, false if not
     */
    public bool isConnected() {
        return (getData() & 0x4) != 0;
    }

    /**
     * Set tripwire connection state
     *
     * @param connected - true if connected, false if not
     */
    public void setConnected(bool connected) {
        int dat = getData() & (0x8 | 0x3);
        if (connected) {
            dat |= 0x4;
        }
        setData((byte) dat);
    }

    /**
     * Test if hook is currently activated
     *
     * @return true if activated, false if not
     */
    public bool isActivated() {
        return (getData() & 0x8) != 0;
    }

    /**
     * Set hook activated state
     *
     * @param act - true if activated, false if not
     */
    public void setActivated(bool act) {
        int dat = getData() & (0x4 | 0x3);
        if (act) {
            dat |= 0x8;
        }
        setData((byte) dat);
    }

    public void setFacingDirection(BlockFace face) {
        int dat = getData() & 0xC;
        switch (face) {
        case WEST:
            dat |= 0x1;
            break;
        case NORTH:
            dat |= 0x2;
            break;
        case EAST:
            dat |= 0x3;
            break;
        case SOUTH:
        default:
            break;
        }
        setData((byte) dat);
    }

    public BlockFace getAttachedFace() {
        switch (getData() & 0x3) {
        case 0:
            return BlockFace.NORTH;
        case 1:
            return BlockFace.EAST;
        case 2:
            return BlockFace.SOUTH;
        case 3:
            return BlockFace.WEST;
        }
        return null;
    }

    public bool isPowered() {
        return isActivated();
    }

    public override TripwireHook clone() {
        return (TripwireHook) base.clone();
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing() + (isActivated()?" Activated":"") + (isConnected()?" Connected":"");
    }
}
