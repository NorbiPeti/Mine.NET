namespace Mine.NET.material{

/**
 * Represents the different types of smooth bricks.
 */
public class SmoothBrick : TexturedMaterial {

    private static readonly List<Materials> textures = new List<Materials>();
    static {
        textures.add(Materials.STONE);
        textures.add(Materials.MOSSY_COBBLESTONE);
        textures.add(Materials.COBBLESTONE);
        textures.add(Materials.SMOOTH_BRICK);
    }

    public SmoothBrick() : base(Materials.SMOOTH_BRICK) {
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SmoothBrick(int type) : base(type) {
    }

    public SmoothBrick(Materials type) {
        base((textures.contains(type)) ? Materials.SMOOTH_BRICK : type);
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
    public SmoothBrick(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SmoothBrick(Materials type, readonly byte data) : base(type, data) {
    }

    public override List<Materials> getTextures() {
        return textures;
    }

    public override SmoothBrick clone() {
        return (SmoothBrick) base.clone();
    }
}
