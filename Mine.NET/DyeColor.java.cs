using System;
using System.Collections.Generic;

namespace Mine.NET
{
    /**
    * All supported color values for dyes and cloth
*/
    public class DyeColor
    {
        public enum Colors
        {
            /**
             * Represents white dye.
             */
            WHITE,
            /**
             * Represents orange dye.
             */
            ORANGE,
            /**
             * Represents magenta dye.
             */
            MAGENTA,
            /**
             * Represents light blue dye.
             */
            LIGHT_BLUE,
            /**
             * Represents yellow dye.
             */
            YELLOW,
            /**
             * Represents lime dye.
             */
            LIME,
            /**
             * Represents pink dye.
             */
            PINK,
            /**
             * Represents gray dye.
             */
            GRAY,
            /**
             * Represents silver dye.
             */
            SILVER,
            /**
             * Represents cyan dye.
             */
            CYAN,
            /**
             * Represents purple dye.
             */
            PURPLE,
            /**
             * Represents blue dye.
             */
            BLUE,
            /**
             * Represents brown dye.
             */
            BROWN,
            /**
             * Represents green dye.
             */
            GREEN,
            /**
             * Represents red dye.
             */
            RED,
            /**
             * Represents black dye.
             */
            BLACK
        }

        private static Dictionary<Colors, DyeColor> AllColors = new Dictionary<Colors, DyeColor>
    {
        /**
         * Represents white dye.
         */
        { Colors.WHITE, new DyeColor(0x0, 0xF, Color.WHITE, Color.fromRGB(0xF0F0F0)) },
        /**
         * Represents orange dye.
         */
        { Colors.ORANGE, new DyeColor(0x1, 0xE, Color.fromRGB(0xD87F33), Color.fromRGB(0xEB8844)) },
        /**
         * Represents magenta dye.
         */
        { Colors.MAGENTA, new DyeColor(0x2, 0xD, Color.fromRGB(0xB24CD8), Color.fromRGB(0xC354CD)) },
        /**
         * Represents light blue dye.
         */
        { Colors.LIGHT_BLUE, new DyeColor(0x3, 0xC, Color.fromRGB(0x6699D8), Color.fromRGB(0x6689D3)) },
        /**
         * Represents yellow dye.
         */
        { Colors.YELLOW, new DyeColor(0x4, 0xB, Color.fromRGB(0xE5E533), Color.fromRGB(0xDECF2A)) },
        /**
         * Represents lime dye.
         */
        { Colors.LIME, new DyeColor(0x5, 0xA, Color.fromRGB(0x7FCC19), Color.fromRGB(0x41CD34)) },
        /**
         * Represents pink dye.
         */
        { Colors.PINK, new DyeColor(0x6, 0x9, Color.fromRGB(0xF27FA5), Color.fromRGB(0xD88198)) },
        /**
         * Represents gray dye.
         */
        { Colors.GRAY, new DyeColor(0x7, 0x8, Color.fromRGB(0x4C4C4C), Color.fromRGB(0x434343)) },
        /**
         * Represents silver dye.
         */
        { Colors.SILVER, new DyeColor(0x8, 0x7, Color.fromRGB(0x999999), Color.fromRGB(0xABABAB)) },
        /**
         * Represents cyan dye.
         */
        { Colors.CYAN, new DyeColor(0x9, 0x6, Color.fromRGB(0x4C7F99), Color.fromRGB(0x287697)) },
        /**
         * Represents purple dye.
         */
        { Colors.PURPLE, new DyeColor(0xA, 0x5, Color.fromRGB(0x7F3FB2), Color.fromRGB(0x7B2FBE)) },
        /**
         * Represents blue dye.
         */
        { Colors.BLUE, new DyeColor(0xB, 0x4, Color.fromRGB(0x334CB2), Color.fromRGB(0x253192)) },
        /**
         * Represents brown dye.
         */
        { Colors.BROWN, new DyeColor(0xC, 0x3, Color.fromRGB(0x664C33), Color.fromRGB(0x51301A)) },
        /**
         * Represents green dye.
         */
        { Colors.GREEN, new DyeColor(0xD, 0x2, Color.fromRGB(0x667F33), Color.fromRGB(0x3B511A)) },
        /**
         * Represents red dye.
         */
        { Colors.RED, new DyeColor(0xE, 0x1, Color.fromRGB(0x993333), Color.fromRGB(0xB3312C)) },
        /**
         * Represents black dye.
         */
        { Colors.BLACK, new DyeColor(0xF, 0x0, Color.fromRGB(0x191919), Color.fromRGB(0x1E1B1B)) }
    };

        private readonly byte woolData;
        private readonly byte dyeData;
        private readonly Color color;
        private readonly Color firework;
        private readonly static DyeColor[] BY_WOOL_DATA;
        private readonly static DyeColor[] BY_DYE_DATA;
        private readonly static Dictionary<Color, DyeColor> BY_COLOR;
        private readonly static Dictionary<Color, DyeColor> BY_FIREWORK;

        private DyeColor(int woolData, int dyeData, Color color, Color firework)
        {
            this.woolData = (byte)woolData;
            this.dyeData = (byte)dyeData;
            this.color = color;
            this.firework = firework;
        }

        /**
         * Gets the associated (wool) data value representing this color.
         *
         * @return A byte containing the (wool) data value of this color
         * [Obsolete] The name is misleading. It would imply {@link
         *     Material#INK_SACK} but uses {@link Material#WOOL}
         * @see #getWoolData()
         * @see #getDyeData()
         */
        [Obsolete]
        public byte getData()
        {
            return getWoolData();
        }

        /**
         * Gets the associated wool data value representing this color.
         *
         * @return A byte containing the wool data value of this color
         * @see #getDyeData()
         * [Obsolete] Magic value
         */
        [Obsolete]
        public byte getWoolData()
        {
            return woolData;
        }

        /**
         * Gets the associated dye data value representing this color.
         *
         * @return A byte containing the dye data value of this color
         * @see #getWoolData()
         * [Obsolete] Magic value
         */
        [Obsolete]
        public byte getDyeData()
        {
            return dyeData;
        }

        /**
         * Gets the color that this dye represents.
         *
         * @return The {@link Color} that this dye represents
         */
        public Color getColor()
        {
            return color;
        }

        /**
         * Gets the firework color that this dye represents.
         *
         * @return The {@link Color} that this dye represents
         */
        public Color getFireworkColor()
        {
            return firework;
        }

        /**
         * Gets the DyeColor with the given (wool) data value.
         *
         * @param data (wool) data value to fetch
         * @return The {@link DyeColor} representing the given value, or null if
         *     it doesn't exist
         * [Obsolete] The name is misleading. It would imply {@link
         *     Material#INK_SACK} but uses {@link Material#WOOL}
         * @see #getByDyeData(byte)
         * @see #getByWoolData(byte)
         */
        [Obsolete]
        public static DyeColor getByData(byte data)
        {
            return getByWoolData(data);
        }

        /**
         * Gets the DyeColor with the given wool data value.
         *
         * @param data Wool data value to fetch
         * @return The {@link DyeColor} representing the given value, or null if
         *     it doesn't exist
         * @see #getByDyeData(byte)
         * [Obsolete] Magic value
         */
        [Obsolete]
        public static DyeColor getByWoolData(byte data)
        {
            int i = 0xff & data;
            if (i >= BY_WOOL_DATA.Length)
            {
                return null;
            }
            return BY_WOOL_DATA[i];
        }

        /**
         * Gets the DyeColor with the given dye data value.
         *
         * @param data Dye data value to fetch
         * @return The {@link DyeColor} representing the given value, or null if
         *     it doesn't exist
         * @see #getByWoolData(byte)
         * [Obsolete] Magic value
         */
        [Obsolete]
        public static DyeColor getByDyeData(byte data)
        {
            int i = 0xff & data;
            if (i >= BY_DYE_DATA.Length)
            {
                return null;
            }
            return BY_DYE_DATA[i];
        }

        /**
         * Gets the DyeColor with the given color value.
         *
         * @param color Color value to get the dye by
         * @return The {@link DyeColor} representing the given value, or null if
         *     it doesn't exist
         */
        public static DyeColor getByColor(Color color)
        {
            return BY_COLOR[color];
        }

        /**
         * Gets the DyeColor with the given firework color value.
         *
         * @param color Color value to get dye by
         * @return The {@link DyeColor} representing the given value, or null if
         *     it doesn't exist
         */
        public static DyeColor getByFireworkColor(Color color)
        {
            return BY_FIREWORK[color];
        }

        static DyeColor()
        {
            BY_WOOL_DATA = new DyeColor[AllColors.Values.Count];
            BY_DYE_DATA = new DyeColor[AllColors.Values.Count];

            foreach (DyeColor color in AllColors.Values)
            {
                BY_WOOL_DATA[color.woolData & 0xff] = color;
                BY_DYE_DATA[color.dyeData & 0xff] = color;
            }
        }
    }
}
