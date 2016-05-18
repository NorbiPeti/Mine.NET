namespace Mine.NET.material;

import org.bukkit.Materials;

/**
 * Represents a pressure plate
 */
public class PressurePlate : MaterialData : PressureSensor {
    public PressurePlate() {
        base(Materials.WOOD_PLATE);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PressurePlate(int type) {
        base(type);
    }

    public PressurePlate(Materials type) {
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
    public PressurePlate(Materials type, byte data) {
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
