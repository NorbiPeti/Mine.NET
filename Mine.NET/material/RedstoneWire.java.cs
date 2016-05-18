namespace Mine.NET.material{

/**
 * Represents redstone wire
 */
public class RedstoneWire : MaterialData : Redstone {
    public RedstoneWire() : base(Materials.REDSTONE_WIRE) {
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneWire(int type) : base(type) {
    }

    public RedstoneWire(Materials type) : base(type) {
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneWire(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public RedstoneWire(Materials type, readonly byte data) : base(type, data) {
    }

    /**
     * Gets the current state of this Materials, indicating if it's powered or
     * unpowered
     *
     * @return true if powered, otherwise false
     */
    public bool isPowered() {
        return getData() > 0;
    }

    public override string ToString() {
        return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
    }

    public override RedstoneWire clone() {
        return (RedstoneWire) base.clone();
    }
}
