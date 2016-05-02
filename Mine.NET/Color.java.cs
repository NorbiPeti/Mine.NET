
using System;
using System.Collections.Generic;
/**
* A container for a color palette. This class is immutable; the set methods
* return a new color. The color names listed as fields are HTML4 standards,
* but subject to change.
*/
[Serializable]
public class Color : ConfigurationSerializable
{
    private static readonly int BIT_MASK = 0xff;

    /**
     * White, or (0xFF,0xFF,0xFF) in (R,G,B)
     */
    public static readonly Color WHITE = fromRGB(0xFFFFFF);

    /**
     * Silver, or (0xC0,0xC0,0xC0) in (R,G,B)
     */
    public static readonly Color SILVER = fromRGB(0xC0C0C0);

    /**
     * Gray, or (0x80,0x80,0x80) in (R,G,B)
     */
    public static readonly Color GRAY = fromRGB(0x808080);

    /**
     * Black, or (0x00,0x00,0x00) in (R,G,B)
     */
    public static readonly Color BLACK = fromRGB(0x000000);

    /**
     * Red, or (0xFF,0x00,0x00) in (R,G,B)
     */
    public static readonly Color RED = fromRGB(0xFF0000);

    /**
     * Maroon, or (0x80,0x00,0x00) in (R,G,B)
     */
    public static readonly Color MAROON = fromRGB(0x800000);

    /**
     * Yellow, or (0xFF,0xFF,0x00) in (R,G,B)
     */
    public static readonly Color YELLOW = fromRGB(0xFFFF00);

    /**
     * Olive, or (0x80,0x80,0x00) in (R,G,B)
     */
    public static readonly Color OLIVE = fromRGB(0x808000);

    /**
     * Lime, or (0x00,0xFF,0x00) in (R,G,B)
     */
    public static readonly Color LIME = fromRGB(0x00FF00);

    /**
     * Green, or (0x00,0x80,0x00) in (R,G,B)
     */
    public static readonly Color GREEN = fromRGB(0x008000);

    /**
     * Aqua, or (0x00,0xFF,0xFF) in (R,G,B)
     */
    public static readonly Color AQUA = fromRGB(0x00FFFF);

    /**
     * Teal, or (0x00,0x80,0x80) in (R,G,B)
     */
    public static readonly Color TEAL = fromRGB(0x008080);

    /**
     * Blue, or (0x00,0x00,0xFF) in (R,G,B)
     */
    public static readonly Color BLUE = fromRGB(0x0000FF);

    /**
     * Navy, or (0x00,0x00,0x80) in (R,G,B)
     */
    public static readonly Color NAVY = fromRGB(0x000080);

    /**
     * Fuchsia, or (0xFF,0x00,0xFF) in (R,G,B)
     */
    public static readonly Color FUCHSIA = fromRGB(0xFF00FF);

    /**
     * Purple, or (0x80,0x00,0x80) in (R,G,B)
     */
    public static readonly Color PURPLE = fromRGB(0x800080);

    /**
     * Orange, or (0xFF,0xA5,0x00) in (R,G,B)
     */
    public static readonly Color ORANGE = fromRGB(0xFFA500);

    private readonly byte red;
    private readonly byte green;
    private readonly byte blue;

    /**
     * Creates a new Color object from a red, green, and blue
     *
     * @param red integer from 0-255
     * @param green integer from 0-255
     * @param blue integer from 0-255
     * @return a new Color object for the red, green, blue
     * @throws IllegalArgumentException if any value is strictly {@literal >255 or <0}
     */
    public static Color fromRGB(int red, int green, int blue)
    {
        return new Color(red, green, blue);
    }

    /**
     * Creates a new Color object from a blue, green, and red
     *
     * @param blue integer from 0-255
     * @param green integer from 0-255
     * @param red integer from 0-255
     * @return a new Color object for the red, green, blue
     * @throws IllegalArgumentException if any value is strictly {@literal >255 or <0}
     */
    public static Color fromBGR(int blue, int green, int red)
    {
        return new Color(red, green, blue);
    }

    /**
     * Creates a new color object from an integer that contains the red,
     * green, and blue bytes in the lowest order 24 bits.
     *
     * @param rgb the integer storing the red, green, and blue values
     * @return a new color object for specified values
     * @throws IllegalArgumentException if any data is in the highest order 8
     *     bits
     */
    public static Color fromRGB(int rgb)
    {
        if ((rgb >> 24) != 0)
            throw new ArgumentException("Extrenuous data in: " + rgb, "rgb");
        return fromRGB(rgb >> 16 & BIT_MASK, rgb >> 8 & BIT_MASK, rgb >> 0 & BIT_MASK);
    }

    /**
     * Creates a new color object from an integer that contains the blue,
     * green, and red bytes in the lowest order 24 bits.
     *
     * @param bgr the integer storing the blue, green, and red values
     * @return a new color object for specified values
     * @throws IllegalArgumentException if any data is in the highest order 8
     *     bits
     */
    public static Color fromBGR(int bgr)
    {
        if ((bgr >> 24) != 0)
            throw new ArgumentException("Extrenuous data in: " + bgr, "bgr");
        return fromBGR(bgr >> 16 & BIT_MASK, bgr >> 8 & BIT_MASK, bgr >> 0 & BIT_MASK);
    }

    private Color(int red, int green, int blue)
    {
        if (red < 0 || red > BIT_MASK)
            throw new ArgumentException("Red is not between 0-255: " + red, "red");
        if (green < 0 || green > BIT_MASK)
            throw new ArgumentException("Red is not between 0-255: " + green, "green");
        if (blue < 0 || blue > BIT_MASK)
            throw new ArgumentException("Red is not between 0-255: " + blue, "blue");

        this.red = (byte)red;
        this.green = (byte)green;
        this.blue = (byte)blue;
    }

    /**
     * Gets the red component
     *
     * @return red component, from 0 to 255
     */
    public int getRed()
    {
        return BIT_MASK & red;
    }

    /**
     * Creates a new Color object with specified component
     *
     * @param red the red component, from 0 to 255
     * @return a new color object with the red component
     */
    public Color setRed(int red)
    {
        return fromRGB(red, getGreen(), getBlue());
    }

    /**
     * Gets the green component
     *
     * @return green component, from 0 to 255
     */
    public int getGreen()
    {
        return BIT_MASK & green;
    }

    /**
     * Creates a new Color object with specified component
     *
     * @param green the red component, from 0 to 255
     * @return a new color object with the red component
     */
    public Color setGreen(int green)
    {
        return fromRGB(getRed(), green, getBlue());
    }

    /**
     * Gets the blue component
     *
     * @return blue component, from 0 to 255
     */
    public int getBlue()
    {
        return BIT_MASK & blue;
    }

    /**
     * Creates a new Color object with specified component
     *
     * @param blue the red component, from 0 to 255
     * @return a new color object with the red component
     */
    public Color setBlue(int blue)
    {
        return fromRGB(getRed(), getGreen(), blue);
    }

    /**
     *
     * @return An integer representation of this color, as 0xRRGGBB
     */
    public int asRGB()
    {
        return getRed() << 16 | getGreen() << 8 | getBlue() << 0;
    }

    /**
     *
     * @return An integer representation of this color, as 0xBBGGRR
     */
    public int asBGR()
    {
        return getBlue() << 16 | getGreen() << 8 | getRed() << 0;
    }

    /**
     * Creates a new color with its RGB components changed as if it was dyed
     * with the colors passed in, replicating vanilla workbench dyeing
     *
     * @param colors The DyeColors to dye with
     * @return A new color with the changed rgb components
     */
    // TODO: Javadoc what this method does, not what it mimics. API != Implementation
    public Color mixDyes(params DyeColor[] colors)
    {

        Color[] toPass = new Color[colors.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            toPass[i] = colors[i].getColor();
        }

        return mixColors(toPass);
    }

    /**
     * Creates a new color with its RGB components changed as if it was dyed
     * with the colors passed in, replicating vanilla workbench dyeing
     *
     * @param colors The colors to dye with
     * @return A new color with the changed rgb components
     */
    // TODO: Javadoc what this method does, not what it mimics. API != Implementation
    public Color mixColors(params Color[] colors)
    {
        int totalRed = this.getRed();
        int totalGreen = this.getGreen();
        int totalBlue = this.getBlue();
        int totalMax = Math.Max(Math.Max(totalRed, totalGreen), totalBlue);
        foreach (Color color in colors)
        {
            totalRed += color.getRed();
            totalGreen += color.getGreen();
            totalBlue += color.getBlue();
            totalMax += Math.Max(Math.Max(color.getRed(), color.getGreen()), color.getBlue());
        }

        float averageRed = totalRed / (colors.Length + 1);
        float averageGreen = totalGreen / (colors.Length + 1);
        float averageBlue = totalBlue / (colors.Length + 1);
        float averageMax = totalMax / (colors.Length + 1);

        float maximumOfAverages = Math.Max(Math.Max(averageRed, averageGreen), averageBlue);
        float gainFactor = averageMax / maximumOfAverages;

        return Color.fromRGB((int)(averageRed * gainFactor), (int)(averageGreen * gainFactor), (int)(averageBlue * gainFactor));
    }

    public override bool Equals(Object o)
    {
        if (o.GetType() != typeof(Color))
        {
            return false;
        }
        Color that = (Color)o;
        return this.blue == that.blue && this.green == that.green && this.red == that.red;
    }

    public override int GetHashCode()
    {
        return asRGB() ^ typeof(Color).GetHashCode();
    }

    public Dictionary<String, Object> serialize()
    {
        return new Dictionary<String, Object>
        {
            { "RED", getRed() },
            { "BLUE", getBlue() },
            { "GREEN", getGreen() }
        };
    }

    public static Color deserialize(Dictionary<String, Object> map)
    {
        return fromRGB(
            asInt("RED", map),
            asInt("GREEN", map),
            asInt("BLUE", map)
        );
    }

    private static int asInt(String string_, Dictionary<String, Object> map)
    {
        Object value = map.get(string_);
        if (value == null)
        {
            throw new ArgumentException(string_ + " not in map " + map);
        }
        if (!value.GetType().IsAssignableFrom(typeof(int)))
        {
            throw new ArgumentException(string_ + '(' + value + ") is not a number");
        }
        return (int)value;
    }

    public override String ToString()
    {
        return "Color:[rgb0x" + getRed().ToString("X") + getGreen().ToString("X") + getBlue().ToString("X") + "]";
    }
}
