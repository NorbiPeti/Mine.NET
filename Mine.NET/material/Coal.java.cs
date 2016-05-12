package org.bukkit.material;

import org.bukkit.CoalType;
import org.bukkit.Material;

/**
 * Represents the different types of coals.
 */
public class Coal : MaterialData {
    public Coal() {
        base(Material.COAL);
    }

    public Coal(CoalType type) {
        this();
        setType(type);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Coal(int type) {
        base(type);
    }

    public Coal(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Coal(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Coal(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Gets the current type of this coal
     *
     * @return CoalType of this coal
     */
    public CoalType getType() {
        return CoalType.getByData(getData());
    }

    /**
     * Sets the type of this coal
     *
     * @param type New type of this coal
     */
    public void setType(CoalType type) {
        setData(type.getData());
    }

    public override string ToString() {
        return getType() + " " + base.ToString();
    }

    public override Coal clone() {
        return (Coal) base.clone();
    }
}
