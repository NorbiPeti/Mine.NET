using Mine.NET.block;
using Mine.NET.material.types;
using System;
using System.Collections.Generic;

namespace Mine.NET.material
{

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
    public Mushroom(Materials shroom) : base(shroom) {
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
    public Mushroom(Materials shroom, BlockFaces capFace) :
        this(shroom, MushroomBlockTexture.getCapByFace(capFace))
        {
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
    public Mushroom(Materials shroom, MushroomBlockTexture texture) :        this(shroom, texture.getData())
        {
    }

    /**
     * @param shroom the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Mushroom(Materials shroom, byte data) : base(shroom, data) {
        if(shroom == Materials.HUGE_MUSHROOM_1 || shroom == Materials.HUGE_MUSHROOM_2) throw new ArgumentException("Not a mushroom!");
    }

    /**
     * @return Whether this is a mushroom stem.
     */
    public bool isStem() {
        return getData() == MushroomBlockTextures.STEM_SIDES || getData() == MushroomBlockTextures.ALL_STEM;
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
            setData((byte)MushroomBlockTextures.STEM_SIDES);
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

        if (data == (byte)MushroomBlockTextures.ALL_PORES || data == (byte)MushroomBlockTextures.STEM_SIDES
                || data == (byte)MushroomBlockTextures.ALL_STEM) {
            return false;
        }

        switch (face) {
            case BlockFaces.WEST:
                return data < NORTH_LIMIT;
            case BlockFaces.EAST:
                return data > SOUTH_LIMIT;
            case BlockFaces.NORTH:
                return data % EAST_WEST_LIMIT == EAST_REMAINDER;
            case BlockFaces.SOUTH:
                return data % EAST_WEST_LIMIT == WEST_REMAINDER;
            case BlockFaces.UP:
                return true;
            case BlockFaces.DOWN:
            case BlockFaces.SELF:
                    return data == (byte)MushroomBlockTextures.ALL_CAP;
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

        if (data == (byte)MushroomBlockTextures.ALL_PORES || isStem()) {
            data = (byte)MushroomBlockTextures.CAP_TOP;
        }
        if (data == (byte)MushroomBlockTextures.ALL_CAP && !painted) {
            data = (byte)MushroomBlockTextures.CAP_TOP;
            face = BlockFace.getOppositeFace(face);
            painted = true;
        }

        switch (face) {
            case BlockFaces.WEST:
                if (painted) {
                    data -= NORTH_SOUTH_MOD;
                } else {
                    data += NORTH_SOUTH_MOD;
                }

                break;
            case BlockFaces.EAST:
                if (painted) {
                    data += NORTH_SOUTH_MOD;
                } else {
                    data -= NORTH_SOUTH_MOD;
                }

                break;
            case BlockFaces.NORTH:
                if (painted) {
                    data += EAST_WEST_MOD;
                } else {
                    data -= EAST_WEST_MOD;
                }

                break;
            case BlockFaces.SOUTH:
                if (painted) {
                    data -= EAST_WEST_MOD;
                } else {
                    data += EAST_WEST_MOD;
                }

                break;
            case BlockFaces.UP:
                if (!painted) {
                        data = (byte)MushroomBlockTextures.ALL_PORES;
                }
                break;
            case BlockFaces.SELF:
            case BlockFaces.DOWN:
                if (painted) {
                        data = (byte)MushroomBlockTextures.ALL_CAP;
                }
                else {
                        data = (byte)MushroomBlockTextures.ALL_PORES;
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
    public List<BlockFaces> getPaintedFaces() {
            List<BlockFaces> faces = new List<BlockFaces>();

        if (isFacePainted(BlockFaces.WEST)) {
            faces.Add(BlockFaces.WEST);
        }

        if (isFacePainted(BlockFaces.NORTH)) {
            faces.Add(BlockFaces.NORTH);
        }

        if (isFacePainted(BlockFaces.SOUTH)) {
            faces.Add(BlockFaces.SOUTH);
        }

        if (isFacePainted(BlockFaces.EAST)) {
            faces.Add(BlockFaces.EAST);
        }

        if (isFacePainted(BlockFaces.UP)) {
            faces.Add(BlockFaces.UP);
        }

        if (isFacePainted(BlockFaces.DOWN)) {
            faces.Add(BlockFaces.DOWN);
        }

        return faces;
    }

    public override string ToString() {
        return Material.getMaterial(getItemTypeId()).ToString() + (isStem() ? " STEM " : " CAP ") + getPaintedFaces();
    }

    public override Mushroom clone() {
        return (Mushroom) base.clone();
    }
}
}
