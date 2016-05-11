package org.bukkit.material;

import org.bukkit.Material;

/**
 * Represents a powered rail
 */
public class PoweredRail extends ExtendedRails implements Redstone {
    public PoweredRail() {
        super(Material.POWERED_RAIL);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PoweredRail(int type) {
        super(type);
    }

    public PoweredRail(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PoweredRail(int type, readonly byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PoweredRail(Material type, readonly byte data) {
        super(type, data);
    }

    public bool isPowered() {
        return (getData() & 0x8) == 0x8;
    }

    /**
     * Set whether this PoweredRail should be powered or not.
     *
     * @param isPowered whether or not the rail is powered
     */
    public void setPowered(bool isPowered) {
        setData((byte) (isPowered ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    @Override
    public PoweredRail clone() {
        return (PoweredRail) super.clone();
    }
}
