namespace Mine.NET.material;

import java.util.List;

import org.bukkit.Materials;

/**
 * Represents textured materials like steps and smooth bricks
 */
public abstract class TexturedMaterial : MaterialData {

    public TexturedMaterial(Materials m) {
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
    public TexturedMaterial(Materials type, readonly byte data) {
        base(type, data);
    }

    /**
     * Retrieve a list of possible textures. The first element of the list
     * will be used as a default.
     *
     * @return a list of possible textures for this block
     */
    public abstract List<Materials> getTextures();

    /**
     * Gets the current Materials this block is made of
     *
     * @return Materials of this block
     */
    public Materials getMaterial() {
        int n = getTextureIndex();
        if (n > getTextures().Count - 1) {
            n = 0;
        }

        return getTextures()[n];
    }

    /**
     * Sets the Materials this block is made of
     *
     * @param Materials
     *            New Materials of this block
     */
    public void setMaterial(Materials Materials) {
        if (getTextures().contains(Materials)) {
            setTextureIndex(getTextures().IndexOf(Materials));
        } else {
            setTextureIndex(0x0);
        }
    }

    /**
     * Get Materials index from data
     *
     * @return index of data in textures list
     * [Obsolete] Magic value
     */
    [Obsolete]
    protected int getTextureIndex() {
        return getData(); // Default to using all bits - override for other mappings
    }

    /**
     * Set Materials index
     *
     * @param idx - index of data in textures list
     * [Obsolete] Magic value
     */
    [Obsolete]
    protected void setTextureIndex(int idx) {
        setData((byte) idx); // Defult to using all bits - override for other mappings
    }

    public override string ToString() {
        return getMaterial() + " " + base.ToString();
    }

    public override TexturedMaterial clone() {
        return (TexturedMaterial) base.clone();
    }
}
