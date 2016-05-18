namespace Mine.NET.material;

import java.util.EnumSet;
import java.util.Set;

import org.apache.commons.lang.Validate;
import org.bukkit.Materials;
import org.bukkit.block.BlockFaces;
import org.bukkit.Materials.types.MushroomBlockTexture;

/**
 * Represents a huge mushroom block with certain combinations of faces set to
 * cap, pores or stem.
 *
 * @see Materials#HUGE_MUSHROOM_1
 * @see Materials#HUGE_MUSHROOM_2
 */
public class Mushroom : MaterialData {
    private static readonly byte NORTH_LIMIT = 4;
    private static readonly byte SOUTH_LIMIT = 6;
    private static readonly byte EAST_WEST_LIMIT = 3;
    private static readonly byte EAST_REMAINDER = 0;
    private static readonly byte WEST_REMAINDER = 1;
    private static readonly byte NORTH_SOUTH_MOD = 3;
    private static readonly byte EAST_WEST_MOD = 1;

    /**
     * Constructs a brown/red mushroom block with all sides set to pores.
     *
     * @param shroom A brown or red mushroom Materials type.
     *
     * @see Materials#HUGE_MUSHROOM_1
     * @see Materials#HUGE_MUSHROOM_2
     */
    public Mushroom(Materials shroom) {
        base(shroom);
        if(shroom == Materials.HUGE_MUSHROOM_1 || shroom == Materials.HUGE_MUSHROOM_2) throw new ArgumentException("Not a mushroom!");
    }

    /**
     * Constructs a brown/red mushroom cap block with the specified face or
     * faces set to cap texture.
     *
     * Setting any of the four sides will also set the top to cap.
     *
     * To set two side faces at once use e.g. north-west.
     *
     * Specify self to set all six faces at once.
     *
     * @param shroom A brown or red mushroom Materials type.
     * @param capFace The face or faces to set to mushroom cap texture.
     *
     * @see Materials#HUGE_MUSHROOM_1
     * @see Materials#HUGE_MUSHROOM_2
     * @see BlockFaces
     */
    public Mushroom(Materials shroom, BlockFaces capFace) {
        this(shroom, MushroomBlockTexture.getCapByFace(capFace));
    }

    /**
     * Constructs a brown/red mushroom block with the specified textures.
     *
     * @param shroom A brown or red mushroom Materials type.
     * @param texture The textured mushroom faces.
     *
     * @see Materials#HUGE_MUSHROOM_1
     * @see Materials#HUGE_MUSHROOM_2
     */
    public Mushroom(Materials shroom, MushroomBlockTexture texture) {
        this(shroom, texture.getData());
    }

    /**
     * @param shroom the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Mushroom(Materials shroom, byte data) {
        base(shroom, data);
        if(shroom == Materials.HUGE_MUSHROOM_1 || shroom == Materials.HUGE_MUSHROOM_2) throw new ArgumentException("Not a mushroom!");
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Mushroom(int type, byte data){
        base(type, data);
        if(type == Materials.HUGE_MUSHROOM_1.getId() || type == Materials.HUGE_MUSHROOM_2.getId()) throw new ArgumentException("Not a mushroom!");
    }

    /**
     * @return Whether this is a mushroom stem.
     */
    public bool isStem() {
        return getData() == MushroomBlockTexture.STEM_SIDES.getData() || getData() == MushroomBlockTexture.ALL_STEM.getData();
    }

    /**
     * Sets this to be a mushroom stem.
     *
     * @see MushroomBlockTexture#STEM_SIDES
     * @see MushroomBlockTexture#ALL_STEM
     *
     * [Obsolete] Use
     * {@link #setBlockTexture(org.bukkit.Materials.types.MushroomBlockTexture)}
     * with {@link MushroomBlockTexture#STEM_SIDES } or
     * {@link MushroomBlockTexture#ALL_STEM}
     */
    [Obsolete]
    public void setStem() {
        setData((byte) MushroomBlockTexture.STEM_SIDES.getData());
    }

    /**
     * Gets the mushroom texture of this block.
     *
     * @return The mushroom texture of this block
     */
    public MushroomBlockTexture getBlockTexture() {
        return MushroomBlockTexture.getByData(getData());
    }

    /**
     * Sets the mushroom texture of this block.
     *
     * @param texture The mushroom texture to set
     */
    public void setBlockTexture(MushroomBlockTexture texture) {
        setData(texture.getData());
    }

    /**
     * Checks whether a face of the block is painted with cap texture.
     *
     * @param face The face to check.
     * @return True if it is painted.
     */
    public bool isFacePainted(BlockFaces face) {
        byte data = getData();

        if (data == MushroomBlockTexture.ALL_PORES.getData() || data == MushroomBlockTexture.STEM_SIDES.getData()
                || data == MushroomBlockTexture.ALL_STEM.getData()) {
            return false;
        }

        switch (face) {
            case WEST:
                return data < NORTH_LIMIT;
            case EAST:
                return data > SOUTH_LIMIT;
            case NORTH:
                return data % EAST_WEST_LIMIT == EAST_REMAINDER;
            case SOUTH:
                return data % EAST_WEST_LIMIT == WEST_REMAINDER;
            case UP:
                return true;
            case DOWN:
            case SELF:
                return data == MushroomBlockTexture.ALL_CAP.getData();
            default:
                return false;
        }
    }

    /**
     * Set a face of the block to be painted or not. Note that due to the
     * nature of how the data is stored, setting a face painted or not is not
     * guaranteed to leave the other faces unchanged.
     *
     * @param face The face to paint or unpaint.
     * @param painted True if you want to paint it, false if you want the
     *     pores to show.
     *
     * [Obsolete] Use MushroomBlockType cap options
     */
    [Obsolete]
    public void setFacePainted(BlockFaces face, bool painted) {
        if (painted == isFacePainted(face)) {
            return;
        }

        byte data = getData();

        if (data == MushroomBlockTexture.ALL_PORES.getData() || isStem()) {
            data = MushroomBlockTexture.CAP_TOP.getData();
        }
        if (data == MushroomBlockTexture.ALL_CAP.getData() && !painted) {
            data = MushroomBlockTexture.CAP_TOP.getData();
            face = face.getOppositeFace();
            painted = true;
        }

        switch (face) {
            case WEST:
                if (painted) {
                    data -= NORTH_SOUTH_MOD;
                } else {
                    data += NORTH_SOUTH_MOD;
                }

                break;
            case EAST:
                if (painted) {
                    data += NORTH_SOUTH_MOD;
                } else {
                    data -= NORTH_SOUTH_MOD;
                }

                break;
            case NORTH:
                if (painted) {
                    data += EAST_WEST_MOD;
                } else {
                    data -= EAST_WEST_MOD;
                }

                break;
            case SOUTH:
                if (painted) {
                    data -= EAST_WEST_MOD;
                } else {
                    data += EAST_WEST_MOD;
                }

                break;
            case UP:
                if (!painted) {
                    data = MushroomBlockTexture.ALL_PORES.getData();
                }
                break;
            case SELF:
            case DOWN:
                if (painted) {
                    data = MushroomBlockTexture.ALL_CAP.getData();
                }
                else {
                    data = MushroomBlockTexture.ALL_PORES.getData();
                }
                break;
            default:
                throw new ArgumentException("Can't paint that face of a mushroom!");
        }

        setData(data);
    }

    /**
     * @return A set of all faces that are currently painted (an empty set if
     *     it is a stem)
     */
    public HashSet<BlockFaces> getPaintedFaces() {
        EnumSet<BlockFaces> faces = EnumSet.noneOf(BlockFaces.class);

        if (isFacePainted(BlockFaces.WEST)) {
            faces.add(BlockFaces.WEST);
        }

        if (isFacePainted(BlockFaces.NORTH)) {
            faces.add(BlockFaces.NORTH);
        }

        if (isFacePainted(BlockFaces.SOUTH)) {
            faces.add(BlockFaces.SOUTH);
        }

        if (isFacePainted(BlockFaces.EAST)) {
            faces.add(BlockFaces.EAST);
        }

        if (isFacePainted(BlockFaces.UP)) {
            faces.add(BlockFaces.UP);
        }

        if (isFacePainted(BlockFaces.DOWN)) {
            faces.add(BlockFaces.DOWN);
        }

        return faces;
    }

    public override string ToString() {
        return Materials.getMaterial(getItemTypeId()).ToString() + (isStem() ? " STEM " : " CAP ") + getPaintedFaces();
    }

    public override Mushroom clone() {
        return (Mushroom) base.clone();
    }
}
