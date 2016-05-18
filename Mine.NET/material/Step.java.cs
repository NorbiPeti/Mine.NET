namespace Mine.NET.material;

import java.util.List;
import java.util.List;

import org.bukkit.Materials;

/**
 * Represents the different types of steps.
 */
public class Step : TexturedMaterial {
    private static readonly List<Materials> textures = new List<Materials>();
    static {
        textures.add(Materials.STONE);
        textures.add(Materials.SANDSTONE);
        textures.add(Materials.WOOD);
        textures.add(Materials.COBBLESTONE);
        textures.add(Materials.BRICK);
        textures.add(Materials.SMOOTH_BRICK);
        textures.add(Materials.NETHER_BRICK);
        textures.add(Materials.QUARTZ_BLOCK);
    }

    public Step() : base(Materials.STEP) {
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Step(int type) : base(type) {
    }

    public Step(Materials type) {
        base((textures.contains(type)) ? Materials.STEP : type);
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
    public Step(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Step(Materials type, readonly byte data) : base(type, data) {
    }

    public override List<Materials> getTextures() {
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

    public override Step clone() {
        return (Step) base.clone();
    }

    public override string ToString() {
        return base.ToString() + (isInverted()?"inverted":"");
    }
}
