package org.bukkit.material;

import org.bukkit.Material;

/**
 * Represents a pressure plate
 */
public class PressurePlate : MaterialData : PressureSensor {
    public PressurePlate() {
        super(Material.WOOD_PLATE);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PressurePlate(int type) {
        super(type);
    }

    public PressurePlate(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PressurePlate(int type, byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PressurePlate(Material type, byte data) {
        super(type, data);
    }

    public bool isPressed() {
        return getData() == 0x1;
    }

    public override string ToString() {
        return super.toString() + (isPressed() ? " PRESSED" : "");
    }

    @Override
    public PressurePlate clone() {
        return (PressurePlate) super.clone();
    }
}
