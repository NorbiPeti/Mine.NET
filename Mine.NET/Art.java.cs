using System;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET
{
    /**
    * Represents the art on a painting
    */
    public class Art
    {
        public enum Arts
        {
            KEBAB,
            AZTEC,
            ALBAN,
            AZTEC2,
            BOMB,
            PLANT,
            WASTELAND,
            POOL,
            COURBET,
            SEA,
            SUNSET,
            CREEBET,
            WANDERER,
            GRAHAM,
            MATCH,
            BUST,
            STAGE,
            VOID,
            SKULL_AND_ROSES,
            WITHER,
            FIGHTERS,
            POINTER,
            PIGSCENE,
            BURNINGSKULL,
            SKELETON,
            DONKEYKONG
        }

        private static Dictionary<Arts, Art> ArtDictionary = new Dictionary<Arts, Art>()
    {
        { Arts.KEBAB, new Art(0, 1, 1, "kebab") },
        { Arts.AZTEC, new Art(1, 1, 1, "aztec") },
        { Arts.ALBAN, new Art(2, 1, 1, "alban") },
        { Arts.AZTEC2, new Art(3, 1, 1, "aztec2") },
        { Arts.BOMB, new Art(4, 1, 1, "bomb") },
        { Arts.PLANT, new Art(5, 1, 1, "plant") },
        { Arts.WASTELAND, new Art(6, 1, 1, "wasteland") },
        { Arts.POOL, new Art(7, 2, 1, "pool") },
        { Arts.COURBET, new Art(8, 2, 1, "courbet") },
        { Arts.SEA, new Art(9, 2, 1, "sea") },
        { Arts.SUNSET, new Art(10, 2, 1, "sunset") },
        { Arts.CREEBET, new Art(11, 2, 1, "creebet") },
        { Arts.WANDERER, new Art(12, 1, 2, "wanderer") },
        { Arts.GRAHAM, new Art(13, 1, 2, "graham") },
        { Arts.MATCH, new Art(14, 2, 2, "match") },
        { Arts.BUST, new Art(15, 2, 2, "bust") },
        { Arts.STAGE, new Art(16, 2, 2, "stage") },
        { Arts.VOID, new Art(17, 2, 2, "void") },
        { Arts.SKULL_AND_ROSES, new Art(18, 2, 2, "skullandroses") },
        { Arts.WITHER, new Art(19, 2, 2, "wither") },
        { Arts.FIGHTERS, new Art(20, 4, 2, "fighters") },
        { Arts.POINTER, new Art(21, 4, 4, "pointer") },
        { Arts.PIGSCENE, new Art(22, 4, 4, "pigscene") },
        { Arts.BURNINGSKULL, new Art(23, 4, 4, "burningskull") },
        { Arts.SKELETON, new Art(24, 4, 3, "skeleton") },
        { Arts.DONKEYKONG, new Art(25, 4, 3, "donkeykong") }
    };

        private int id, width, height;
        private string name;

        private Art(int id, int width, int height, string name)
        {
            this.id = id;
            this.width = width;
            this.height = height;
            this.name = name;
        }

        /**
         * Gets the width of the painting, in blocks
         *
         * @return The width of the painting, in blocks
         */
        public int getBlockWidth()
        {
            return width;
        }

        /**
         * Gets the height of the painting, in blocks
         *
         * @return The height of the painting, in blocks
         */
        public int getBlockHeight()
        {
            return height;
        }

        /**
         * Get the ID of this painting.
         *
         * @return The ID of this painting
         * [Obsolete] Magic value
         */
        [Obsolete("", true)]
        public int getId()
        {
            return id;
        }

        /**
         * Get a painting by its numeric ID
         *
         * @param id The ID
         * @return The painting
         * [Obsolete] Magic value
         */
        [Obsolete("", true)]
        public static Art getById(int id)
        {
            throw new NotImplementedException();
        }

        /**
         * Get a painting by its unique name
         * <p>
         * This ignores underscores and capitalization
         *
         * @param name The name
         * @return The painting
         */
        public static Art getByName(String name)
        {
            //if(name==null) throw new ArgumentNullException("Name cannot be null"); - TODO

            return ArtDictionary.Values.First(a => a.name == name);
        }
    }
}
