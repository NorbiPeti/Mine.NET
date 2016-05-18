namespace Mine.NET.material{

/**
 * Represents the different types of monster eggs
 */
public class MonsterEggs : TexturedMaterial {

    private static readonly List<Materials> textures = new List<Materials>();
    static {
        textures.add(Materials.STONE);
        textures.add(Materials.COBBLESTONE);
        textures.add(Materials.SMOOTH_BRICK);
    }

    public MonsterEggs() : base(Materials.MONSTER_EGGS) {
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MonsterEggs(int type) : base(type) {
    }

    public MonsterEggs(Materials type) {
        base((textures.contains(type)) ? Materials.MONSTER_EGGS : type);
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
    public MonsterEggs(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MonsterEggs(Materials type, readonly byte data) : base(type, data) {
    }

    public override List<Materials> getTextures() {
        return textures;
    }

    public override MonsterEggs clone() {
        return (MonsterEggs) base.clone();
    }
}
