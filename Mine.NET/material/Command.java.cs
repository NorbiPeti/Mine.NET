package org.bukkit.material;

import org.bukkit.Material;

/**
 * Represents a command block
 */
public class Command : MaterialData : Redstone {
    public Command() {
        base(Material.COMMAND);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Command(int type) {
        base(type);
    }

    public Command(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Command(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Command(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Gets the current state of this Material, indicating if it's powered or
     * unpowered
     *
     * @return true if powered, otherwise false
     */
    public bool isPowered() {
        return (getData() & 1) != 0;
    }

    /**
     * Sets the current state of this Material
     *
     * @param bool
     *            whether or not the command block is powered
     */
    public void setPowered(bool bool) {
        setData((byte) (bool ? (getData() | 1) : (getData() & -2)));
    }

    public override string ToString() {
        return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
    }

    public override Command clone() {
        return (Command) base.clone();
    }
}
