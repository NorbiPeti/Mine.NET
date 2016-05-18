namespace Mine.NET.material;

import org.bukkit.Materials;

/**
 * Represents a furnace or dispenser, two types of directional containers
 */
public class FurnaceAndDispenser : DirectionalContainer {

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public FurnaceAndDispenser(int type) {
        base(type);
    }

    public FurnaceAndDispenser(Materials type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public FurnaceAndDispenser(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public FurnaceAndDispenser(Materials type, readonly byte data) {
        base(type, data);
    }

    public override FurnaceAndDispenser clone() {
        return (FurnaceAndDispenser) base.clone();
    }
}
