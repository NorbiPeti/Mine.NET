namespace Mine.NET.material.types
{
/**
 * Represents the different textured blocks of mushroom.
 */
public enum MushroomBlockTexture {

    /**
     * Pores on all faces.
     */
    ALL_PORES(0, null),
    /**
     * Cap texture on the top, north and west faces, pores on remaining sides.
     */
    CAP_NORTH_WEST(1, BlockFace.NORTH_WEST),
    /**
     * Cap texture on the top and north faces, pores on remaining sides.
     */
    CAP_NORTH(2, BlockFace.NORTH),
    /**
     * Cap texture on the top, north and east faces, pores on remaining sides.
     */
    CAP_NORTH_EAST(3, BlockFace.NORTH_EAST),
    /**
     * Cap texture on the top and west faces, pores on remaining sides.
     */
    CAP_WEST(4, BlockFace.WEST),
    /**
     * Cap texture on the top face, pores on remaining sides.
     */
    CAP_TOP(5, BlockFace.UP),
    /**
     * Cap texture on the top and east faces, pores on remaining sides.
     */
    CAP_EAST(6, BlockFace.EAST),
    /**
     * Cap texture on the top, south and west faces, pores on remaining sides.
     */
    CAP_SOUTH_WEST(7, BlockFace.SOUTH_WEST),
    /**
     * Cap texture on the top and south faces, pores on remaining sides.
     */
    CAP_SOUTH(8, BlockFace.SOUTH),
    /**
     * Cap texture on the top, south and east faces, pores on remaining sides.
     */
    CAP_SOUTH_EAST(9, BlockFace.SOUTH_EAST),
    /**
     * Stem texture on the north, east, south and west faces, pores on top and
     * bottom.
     */
    STEM_SIDES(10, null),
    /**
     * Cap texture on all faces.
     */
    ALL_CAP(14, BlockFace.SELF),
    /**
     * Stem texture on all faces.
     */
    ALL_STEM(15, null);
    private readonly static Dictionary<Byte, MushroomBlockTexture> BY_DATA = Maps.newHashMap();
    private readonly static Dictionary<BlockFace, MushroomBlockTexture> BY_BLOCKFACE = Maps.newHashMap();

    private readonly Byte data;
    private readonly BlockFace capFace;

    private MushroomBlockTexture(int data, readonly BlockFace capFace) {
        this.data = (byte) data;
        this.capFace = capFace;
    }

    /**
     * Gets the associated data value representing this mushroom block face.
     *
     * @return A byte containing the data value of this mushroom block face
     * [Obsolete] Magic value
     */
    [Obsolete]
    public byte getData() {
        return data;
    }

    /**
     * Gets the face that has cap texture.
     *
     * @return The cap face
     */
    public BlockFace getCapFace() {
        return capFace;
    }

    /**
     * Gets the MushroomBlockType with the given data value.
     *
     * @param data Data value to fetch
     * @return The {@link MushroomBlockTexture} representing the given value, or
     * null if it doesn't exist
     * [Obsolete] Magic value
     */
    [Obsolete]
    public static MushroomBlockTexture getByData(byte data) {
        return BY_DATA[data];
    }

    /**
     * Gets the MushroomBlockType with cap texture on the given block face.
     *
     * @param face the required block face with cap texture
     * @return The {@link MushroomBlockTexture} representing the given block
     * face, or null if it doesn't exist
     *
     * @see BlockFace
     */
    public static MushroomBlockTexture getCapByFace(BlockFace face) {
        return BY_BLOCKFACE[face];
    }

    static {
        foreach (MushroomBlockTexture type  in  values()) {
            BY_DATA.Add(type.data, type);
            BY_BLOCKFACE.Add(type.capFace, type);
        }
    }
}
}
