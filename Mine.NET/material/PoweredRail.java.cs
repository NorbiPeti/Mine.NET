namespace Mine.NET.material;

import org.bukkit.Material;

/**
 * Represents a powered rail
 */
public class PoweredRail : ExtendedRails : Redstone {
    public PoweredRail() {
        base(Material.POWERED_RAIL);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PoweredRail(int type) {
        base(type);
    }

    public PoweredRail(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PoweredRail(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PoweredRail(Material type, readonly byte data) {
        base(type, data);
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

    public override PoweredRail clone() {
        return (PoweredRail) base.clone();
    }
}
