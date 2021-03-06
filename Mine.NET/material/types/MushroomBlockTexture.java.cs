using Mine.NET.block;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET.material.types
{
    /**
     * Represents the different textured blocks of mushroom.
     */
    public enum MushroomBlockTextures
    {

        /**
         * Pores on all faces.
         */
        ALL_PORES,
        /**
         * Cap texture on the top, north and west faces, pores on remaining sides.
         */
        CAP_NORTH_WEST,
        /**
         * Cap texture on the top and north faces, pores on remaining sides.
         */
        CAP_NORTH,
        /**
         * Cap texture on the top, north and east faces, pores on remaining sides.
         */
        CAP_NORTH_EAST,
        /**
         * Cap texture on the top and west faces, pores on remaining sides.
         */
        CAP_WEST,
        /**
         * Cap texture on the top face, pores on remaining sides.
         */
        CAP_TOP,
        /**
         * Cap texture on the top and east faces, pores on remaining sides.
         */
        CAP_EAST,
        /**
         * Cap texture on the top, south and west faces, pores on remaining sides.
         */
        CAP_SOUTH_WEST,
        /**
         * Cap texture on the top and south faces, pores on remaining sides.
         */
        CAP_SOUTH,
        /**
         * Cap texture on the top, south and east faces, pores on remaining sides.
         */
        CAP_SOUTH_EAST,
        /**
         * Stem texture on the north, east, south and west faces, pores on top and
         * bottom.
         */
        STEM_SIDES,
        /**
         * Cap texture on all faces.
         */
        ALL_CAP,
        /**
         * Stem texture on all faces.
         */
        ALL_STEM
    }

    public class MushroomBlockTexture
    {
        public static readonly Dictionary<MushroomBlockTextures, MushroomBlockTexture> AllMBT = new Dictionary<MushroomBlockTextures, MushroomBlockTexture>()
        { //Find: "\*\/\s+([^\(]+)(\([^\(]+\))" - Replace: "*/\n{ MushroomBlockTextures.$1, new MushroomBlockTexture$2 }"
        /**
         * Pores on all faces.
         */
{ MushroomBlockTextures.ALL_PORES, new MushroomBlockTexture(0, null) },
        /**
         * Cap texture on the top, north and west faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_NORTH_WEST, new MushroomBlockTexture(1, BlockFaces.NORTH_WEST) },
        /**
         * Cap texture on the top and north faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_NORTH, new MushroomBlockTexture(2, BlockFaces.NORTH) },
        /**
         * Cap texture on the top, north and east faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_NORTH_EAST, new MushroomBlockTexture(3, BlockFaces.NORTH_EAST) },
        /**
         * Cap texture on the top and west faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_WEST, new MushroomBlockTexture(4, BlockFaces.WEST) },
        /**
         * Cap texture on the top face, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_TOP, new MushroomBlockTexture(5, BlockFaces.UP) },
        /**
         * Cap texture on the top and east faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_EAST, new MushroomBlockTexture(6, BlockFaces.EAST) },
        /**
         * Cap texture on the top, south and west faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_SOUTH_WEST, new MushroomBlockTexture(7, BlockFaces.SOUTH_WEST) },
        /**
         * Cap texture on the top and south faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_SOUTH, new MushroomBlockTexture(8, BlockFaces.SOUTH) },
        /**
         * Cap texture on the top, south and east faces, pores on remaining sides.
         */
{ MushroomBlockTextures.CAP_SOUTH_EAST, new MushroomBlockTexture(9, BlockFaces.SOUTH_EAST) },
        /**
         * Stem texture on the north, east, south and west faces, pores on top and
         * bottom.
         */
{ MushroomBlockTextures.STEM_SIDES, new MushroomBlockTexture(10, null) },
        /**
         * Cap texture on all faces.
         */
{ MushroomBlockTextures.ALL_CAP, new MushroomBlockTexture(14, BlockFaces.SELF) },
        /**
         * Stem texture on all faces.
         */
{ MushroomBlockTextures.ALL_STEM, new MushroomBlockTexture(15, null) }
    };

        private readonly Byte data;
        private readonly BlockFaces? capFace;

        private MushroomBlockTexture(int data, BlockFaces? capFace)
        {
            this.data = (byte)data;
            this.capFace = capFace;
        }

        /**
         * Gets the associated data value representing this mushroom block face.
         *
         * @return A byte containing the data value of this mushroom block face
         * [Obsolete] Magic value
         */
        [Obsolete]
        public byte getData()
        {
            return data;
        }

        /**
         * Gets the face that has cap texture.
         *
         * @return The cap face
         */
        public BlockFaces? getCapFace()
        {
            return capFace;
        }

        /**
         * Gets the MushroomBlockType with cap texture on the given block face.
         *
         * @param face the required block face with cap texture
         * @return The {@link MushroomBlockTexture} representing the given block
         * face, or null if it doesn't exist
         *
         * @see BlockFaces
         */
        public static MushroomBlockTexture getCapByFace(BlockFaces face)
        {
            return AllMBT.Single(mbt => mbt.Value.capFace == face).Value;
        }

        /*static {
            foreach (MushroomBlockTexture type  in  values()) {
                BY_DATA.Add(type.data, type);
                BY_BLOCKFACE.Add(type.capFace, type);
            }
        }*/
    }
}
