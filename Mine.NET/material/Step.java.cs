package org.bukkit.material;

import java.util.List;
import java.util.List;

import org.bukkit.Material;

/**
 * Represents the different types of steps.
 */
public class Step : TexturedMaterial {
    private static readonly List<Material> textures = new List<Material>();
    static {
        textures.add(Material.STONE);
        textures.add(Material.SANDSTONE);
        textures.add(Material.WOOD);
        textures.add(Material.COBBLESTONE);
        textures.add(Material.BRICK);
        textures.add(Material.SMOOTH_BRICK);
        textures.add(Material.NETHER_BRICK);
        textures.add(Material.QUARTZ_BLOCK);
    }

    public Step() {
        base(Material.STEP);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Step(int type) {
        base(type);
    }

    public Step(Material type) {
        base((textures.contains(type)) ? Material.STEP : type);
        if (textures.contains(type)) {
            setMaterial(type);
        }
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Step(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Step(Material type, readonly byte data) {
        base(type, data);
    }

    @Override
    public List<Material> getTextures() {
        return textures;
    }

    /**
     * Test if step is inverted
     *
     * @return true if inverted (top half), false if normal (bottom half)
     */
    public bool isInverted() {
        return ((getData() & 0x8) != 0);
    }

    /**
     * Set step inverted state
     *
     * @param inv - true if step is inverted (top half), false if step is
     *     normal (bottom half)
     */
    public void setInverted(bool inv) {
        int dat = getData() & 0x7;
        if (inv) {
            dat |= 0x8;
        }
        setData((byte) dat);
    }

    /**
     *
     * [Obsolete] Magic value
     */
    [Obsolete]
    @Override
    protected int getTextureIndex() {
        return getData() & 0x7;
    }

    /**
     *
     * [Obsolete] Magic value
     */
    [Obsolete]
    @Override
    protected void setTextureIndex(int idx) {
        setData((byte) ((getData() & 0x8) | idx));
    }

    @Override
    public Step clone() {
        return (Step) base.clone();
    }

    public override string ToString() {
        return base.toString() + (isInverted()?"inverted":"");
    }
}
