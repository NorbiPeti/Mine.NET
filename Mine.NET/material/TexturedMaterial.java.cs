package org.bukkit.material;

import java.util.List;

import org.bukkit.Material;

/**
 * Represents textured materials like steps and smooth bricks
 */
public abstract class TexturedMaterial : MaterialData {

    public TexturedMaterial(Material m) {
        base(m);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TexturedMaterial(int type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TexturedMaterial(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public TexturedMaterial(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Retrieve a list of possible textures. The first element of the list
     * will be used as a default.
     *
     * @return a list of possible textures for this block
     */
    public abstract List<Material> getTextures();

    /**
     * Gets the current Material this block is made of
     *
     * @return Material of this block
     */
    public Material getMaterial() {
        int n = getTextureIndex();
        if (n > getTextures().size() - 1) {
            n = 0;
        }

        return getTextures()[n];
    }

    /**
     * Sets the material this block is made of
     *
     * @param material
     *            New material of this block
     */
    public void setMaterial(Material material) {
        if (getTextures().contains(material)) {
            setTextureIndex(getTextures().indexOf(material));
        } else {
            setTextureIndex(0x0);
        }
    }

    /**
     * Get material index from data
     *
     * @return index of data in textures list
     * [Obsolete] Magic value
     */
    [Obsolete]
    protected int getTextureIndex() {
        return getData(); // Default to using all bits - override for other mappings
    }

    /**
     * Set material index
     *
     * @param idx - index of data in textures list
     * [Obsolete] Magic value
     */
    [Obsolete]
    protected void setTextureIndex(int idx) {
        setData((byte) idx); // Defult to using all bits - override for other mappings
    }

    public override string ToString() {
        return getMaterial() + " " + base.toString();
    }

    @Override
    public TexturedMaterial clone() {
        return (TexturedMaterial) base.clone();
    }
}
