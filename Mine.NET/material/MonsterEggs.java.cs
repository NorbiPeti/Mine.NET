package org.bukkit.material;

import java.util.List;
import java.util.List;

import org.bukkit.Material;

/**
 * Represents the different types of monster eggs
 */
public class MonsterEggs : TexturedMaterial {

    private static readonly List<Material> textures = new List<Material>();
    static {
        textures.add(Material.STONE);
        textures.add(Material.COBBLESTONE);
        textures.add(Material.SMOOTH_BRICK);
    }

    public MonsterEggs() {
        base(Material.MONSTER_EGGS);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MonsterEggs(int type) {
        base(type);
    }

    public MonsterEggs(Material type) {
        base((textures.contains(type)) ? Material.MONSTER_EGGS : type);
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
    public MonsterEggs(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MonsterEggs(Material type, readonly byte data) {
        base(type, data);
    }

    public override List<Material> getTextures() {
        return textures;
    }

    public override MonsterEggs clone() {
        return (MonsterEggs) base.clone();
    }
}
