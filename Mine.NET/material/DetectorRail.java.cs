package org.bukkit.material;

import org.bukkit.Material;

/**
 * Represents a detector rail
 */
public class DetectorRail : ExtendedRails : PressureSensor {
    public DetectorRail() {
        base(Material.DETECTOR_RAIL);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public DetectorRail(int type) {
        base(type);
    }

    public DetectorRail(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public DetectorRail(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public DetectorRail(Material type, readonly byte data) {
        base(type, data);
    }

    public bool isPressed() {
        return (getData() & 0x8) == 0x8;
    }

    public void setPressed(bool isPressed) {
        setData((byte) (isPressed ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    public override DetectorRail clone() {
        return (DetectorRail) base.clone();
    }
}
