package org.bukkit.material;

import java.util.List;
import java.util.List;

import org.bukkit.Material;

/**
 * Represents the different types of smooth bricks.
 */
public class SmoothBrick : TexturedMaterial {

    private static readonly List<Material> textures = new List<Material>();
    static {
        textures.add(Material.STONE);
        textures.add(Material.MOSSY_COBBLESTONE);
        textures.add(Material.COBBLESTONE);
        textures.add(Material.SMOOTH_BRICK);
    }

    public SmoothBrick() {
        base(Material.SMOOTH_BRICK);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SmoothBrick(int type) {
        base(type);
    }

    public SmoothBrick(Material type) {
        base((textures.contains(type)) ? Material.SMOOTH_BRICK : type);
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
    public SmoothBrick(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SmoothBrick(Material type, readonly byte data) {
        base(type, data);
    }

    @Override
    public List<Material> getTextures() {
        return textures;
    }

    @Override
    public SmoothBrick clone() {
        return (SmoothBrick) base.clone();
    }
}
