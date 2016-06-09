using Mine.NET.block;
using Mine.NET.material.types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET.material
{

    /**
     * Represents a huge mushroom block with certain combinations of faces set to
     * cap, pores or stem.
     *
     * @see Materials#HUGE_MUSHROOM_1
     * @see Materials#HUGE_MUSHROOM_2
     */
    public class Mushroom : MaterialData
    {
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
        public Mushroom(Materials shroom) : base(shroom)
        {
            if (shroom == Materials.HUGE_MUSHROOM_1 || shroom == Materials.HUGE_MUSHROOM_2) throw new ArgumentException("Not a mushroom!");
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
        public Mushroom(Materials shroom, MushroomBlockTexture texture) : base(shroom)
        {
            if (shroom == Materials.HUGE_MUSHROOM_1 || shroom == Materials.HUGE_MUSHROOM_2) throw new ArgumentException("Not a mushroom!");
        }

        private MushroomBlockTextures texture;
        /**
         * @return Whether this is a mushroom stem.
         */
        public bool isStem()
        {
            return texture == MushroomBlockTextures.STEM_SIDES || texture == MushroomBlockTextures.ALL_STEM;
        }

        /**
         * Gets the mushroom texture of this block.
         *
         * @return The mushroom texture of this block
         */
        public MushroomBlockTextures getBlockTexture()
        {
            return texture;
        }

        /**
         * Sets the mushroom texture of this block.
         *
         * @param texture The mushroom texture to set
         */
        public void setBlockTexture(MushroomBlockTextures texture)
        {
            this.texture = texture;
        }

        /**
         * Checks whether a face of the block is painted with cap texture.
         *
         * @param face The face to check.
         * @return True if it is painted.
         */
        public bool isFacePainted(BlockFaces face)
        {
            //byte data = getData();

            if (texture == MushroomBlockTextures.ALL_PORES || texture == MushroomBlockTextures.STEM_SIDES
                    || texture == MushroomBlockTextures.ALL_STEM)
            {
                return false;
            }

            switch (face)
            {
                case BlockFaces.WEST:
                    return (byte)texture < NORTH_LIMIT;
                case BlockFaces.EAST:
                    return (byte)texture > SOUTH_LIMIT;
                case BlockFaces.NORTH:
                    return (byte)texture % EAST_WEST_LIMIT == EAST_REMAINDER;
                case BlockFaces.SOUTH:
                    return (byte)texture % EAST_WEST_LIMIT == WEST_REMAINDER;
                case BlockFaces.UP:
                    return true;
                case BlockFaces.DOWN:
                case BlockFaces.SELF:
                    return (byte)texture == (byte)MushroomBlockTextures.ALL_CAP;
                default:
                    return false;
            }
        }

        /**
         * @return A set of all faces that are currently painted (an empty set if
         *     it is a stem)
         */
        public List<BlockFaces> getPaintedFaces()
        {
            List<BlockFaces> faces = new List<BlockFaces>();

            if (isFacePainted(BlockFaces.WEST))
            {
                faces.Add(BlockFaces.WEST);
            }

            if (isFacePainted(BlockFaces.NORTH))
            {
                faces.Add(BlockFaces.NORTH);
            }

            if (isFacePainted(BlockFaces.SOUTH))
            {
                faces.Add(BlockFaces.SOUTH);
            }

            if (isFacePainted(BlockFaces.EAST))
            {
                faces.Add(BlockFaces.EAST);
            }

            if (isFacePainted(BlockFaces.UP))
            {
                faces.Add(BlockFaces.UP);
            }

            if (isFacePainted(BlockFaces.DOWN))
            {
                faces.Add(BlockFaces.DOWN);
            }

            return faces;
        }

        public override string ToString()
        {
            return getItemType().ToString() + (isStem() ? " STEM " : " CAP ") + getPaintedFaces();
        }

        public new Mushroom Clone() { return (Mushroom)base.Clone(); }
    }
}
