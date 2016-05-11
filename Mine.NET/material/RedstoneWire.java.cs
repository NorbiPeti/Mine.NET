package org.bukkit.material;

import org.bukkit.Material;

/**
 * Represents redstone wire
 */
public class RedstoneWire : MaterialData : Redstone {
    public RedstoneWire() {
        super(Material.REDSTONE_WIRE);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneWire(int type) {
        super(type);
    }

    public RedstoneWire(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneWire(int type, readonly byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneWire(Material type, readonly byte data) {
        super(type, data);
    }

    /**
     * Gets the current state of this Material, indicating if it's powered or
     * unpowered
     *
     * @return true if powered, otherwise false
     */
    public bool isPowered() {
        return getData() > 0;
    }

    public override string ToString() {
        return super.toString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
    }

    @Override
    public RedstoneWire clone() {
        return (RedstoneWire) super.clone();
    }
}
