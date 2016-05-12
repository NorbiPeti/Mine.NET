namespace Mine.NET.material;

import org.bukkit.Material;
import org.bukkit.SandstoneType;

/**
 * Represents the different types of sandstone.
 */
public class Sandstone : MaterialData {
    public Sandstone() {
        base(Material.SANDSTONE);
    }

    public Sandstone(SandstoneType type) {
        this();
        setType(type);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Sandstone(int type) {
        base(type);
    }

    public Sandstone(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Sandstone(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Sandstone(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Gets the current type of this sandstone
     *
     * @return SandstoneType of this sandstone
     */
    public SandstoneType getType() {
        return SandstoneType.getByData(getData());
    }

    /**
     * Sets the type of this sandstone
     *
     * @param type New type of this sandstone
     */
    public void setType(SandstoneType type) {
        setData(type.getData());
    }

    public override string ToString() {
        return getType() + " " + base.ToString();
    }

    public override Sandstone clone() {
        return (Sandstone) base.clone();
    }
}
