using System;
using System.Collections.Generic;

namespace Mine.NET.block.banner
{
    public enum PatternType
    {
        BASE,
        SQUARE_BOTTOM_LEFT,
        SQUARE_BOTTOM_RIGHT,
        SQUARE_TOP_LEFT,
        SQUARE_TOP_RIGHT,
        STRIPE_BOTTOM,
        STRIPE_TOP,
        STRIPE_LEFT,
        STRIPE_RIGHT,
        STRIPE_CENTER,
        STRIPE_MIDDLE,
        STRIPE_DOWNRIGHT,
        STRIPE_DOWNLEFT,
        STRIPE_SMALL,
        CROSS,
        STRAIGHT_CROSS,
        TRIANGLE_BOTTOM,
        TRIANGLE_TOP,
        TRIANGLES_BOTTOM,
        TRIANGLES_TOP,
        DIAGONAL_LEFT,
        DIAGONAL_RIGHT,
        DIAGONAL_LEFT_MIRROR,
        DIAGONAL_RIGHT_MIRROR,
        CIRCLE_MIDDLE,
        RHOMBUS_MIDDLE,
        HALF_VERTICAL,
        HALF_HORIZONTAL,
        HALF_VERTICAL_MIRROR,
        HALF_HORIZONTAL_MIRROR,
        BORDER,
        CURLY_BORDER,
        CREEPER,
        GRADIENT,
        GRADIENT_UP,
        BRICKS,
        SKULL,
        FLOWER,
        MOJANG
    }

    public class PatternTypeC
    {
        public static readonly Dictionary<String, PatternType> byString = new Dictionary<string, PatternType>
    {
        { "b", PatternType.BASE },
        { "bl", PatternType.SQUARE_BOTTOM_LEFT },
        { "br", PatternType.SQUARE_BOTTOM_RIGHT },
        { "tl", PatternType.SQUARE_TOP_LEFT },
        { "tr", PatternType.SQUARE_TOP_RIGHT },
        { "bs", PatternType.STRIPE_BOTTOM },
        { "ts", PatternType.STRIPE_TOP },
        { "ls", PatternType.STRIPE_LEFT },
        { "rs", PatternType.STRIPE_RIGHT },
        { "cs", PatternType.STRIPE_CENTER },
        { "ms", PatternType.STRIPE_MIDDLE },
        { "drs", PatternType.STRIPE_DOWNRIGHT },
        { "dls", PatternType.STRIPE_DOWNLEFT },
        { "ss", PatternType.STRIPE_SMALL },
        { "cr", PatternType.CROSS },
        { "sc", PatternType.STRAIGHT_CROSS },
        { "bt", PatternType.TRIANGLE_BOTTOM },
        { "tt", PatternType.TRIANGLE_TOP },
        { "bts", PatternType.TRIANGLES_BOTTOM },
        { "tts", PatternType.TRIANGLES_TOP },
        { "ld", PatternType.DIAGONAL_LEFT },
        { "rd", PatternType.DIAGONAL_RIGHT },
        { "lud", PatternType.DIAGONAL_LEFT_MIRROR },
        { "rud", PatternType.DIAGONAL_RIGHT_MIRROR },
        { "mc", PatternType.CIRCLE_MIDDLE },
        { "mr", PatternType.RHOMBUS_MIDDLE },
        { "vh", PatternType.HALF_VERTICAL },
        { "hh", PatternType.HALF_HORIZONTAL },
        { "vhr", PatternType.HALF_VERTICAL_MIRROR },
        { "hhb", PatternType.HALF_HORIZONTAL_MIRROR },
        { "bo", PatternType.BORDER },
        { "cbo", PatternType.CURLY_BORDER },
        { "cre", PatternType.CREEPER },
        { "gra", PatternType.GRADIENT },
        { "gru", PatternType.GRADIENT_UP },
        { "bri", PatternType.BRICKS },
        { "sku", PatternType.SKULL },
        { "flo", PatternType.FLOWER },
        { "moj", PatternType.MOJANG }
    };
    }
}
