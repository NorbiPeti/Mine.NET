package org.bukkit.material;

import org.bukkit.Material;

/**
 * Represents a redstone torch
 */
public class RedstoneTorch extends Torch implements Redstone {
    public RedstoneTorch() {
        super(Material.REDSTONE_TORCH_ON);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneTorch(int type) {
        super(type);
    }

    public RedstoneTorch(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneTorch(int type, final byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneTorch(Material type, final byte data) {
        super(type, data);
    }

    /**
     * Gets the current state of this Material, indicating if it's powered or
     * unpowered
     *
     * @return true if powered, otherwise false
     */
    public boolean isPowered() {
        return getItemType() == Material.REDSTONE_TORCH_ON;
    }

    @Override
    public String toString() {
        return super.toString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
    }

    @Override
    public RedstoneTorch clone() {
        return (RedstoneTorch) super.clone();
    }
}
