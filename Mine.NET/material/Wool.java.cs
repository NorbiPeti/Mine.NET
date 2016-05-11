package org.bukkit.material;

import org.bukkit.DyeColor;
import org.bukkit.Material;

/**
 * Represents a Wool/Cloth block
 */
public class Wool : MaterialData : Colorable {
    public Wool() {
        base(Material.WOOL);
    }

    public Wool(DyeColor color) {
        this();
        setColor(color);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Wool(int type) {
        base(type);
    }

    public Wool(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Wool(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Wool(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Gets the current color of this dye
     *
     * @return DyeColor of this dye
     */
    public DyeColor getColor() {
        return DyeColor.getByWoolData(getData());
    }

    /**
     * Sets the color of this dye
     *
     * @param color New color of this dye
     */
    public void setColor(DyeColor color) {
        setData(color.getWoolData());
    }

    public override string ToString() {
        return getColor() + " " + base.toString();
    }

    @Override
    public Wool clone() {
        return (Wool) base.clone();
    }
}
