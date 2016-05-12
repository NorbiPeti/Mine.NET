package org.bukkit.material;

import org.bukkit.Material;

/**
 * Represents a pressure plate
 */
public class PressurePlate : MaterialData : PressureSensor {
    public PressurePlate() {
        base(Material.WOOD_PLATE);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PressurePlate(int type) {
        base(type);
    }

    public PressurePlate(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PressurePlate(int type, byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PressurePlate(Material type, byte data) {
        base(type, data);
    }

    public bool isPressed() {
        return getData() == 0x1;
    }

    public override string ToString() {
        return base.ToString() + (isPressed() ? " PRESSED" : "");
    }

    public override PressurePlate clone() {
        return (PressurePlate) base.clone();
    }
}
