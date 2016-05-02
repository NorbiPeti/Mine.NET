
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
/**
* All supported color values for chat
*/
public class ChatColor {
    private enum Colors
    {
        /**
         * Represents black
         */
        BLACK,
        /**
         * Represents dark blue
         */
        DARK_BLUE,
        /**
         * Represents dark green
         */
        DARK_GREEN,
        /**
         * Represents dark blue (aqua)
         */
        DARK_AQUA,
        /**
         * Represents dark red
         */
        DARK_RED,
        /**
         * Represents dark purple
         */
        DARK_PURPLE,
        /**
         * Represents gold
         */
        GOLD,
        /**
         * Represents gray
         */
        GRAY,
        /**
         * Represents dark gray
         */
        DARK_GRAY,
        /**
         * Represents blue
         */
        BLUE,
        /**
         * Represents green
         */
        GREEN,
        /**
         * Represents aqua
         */
        AQUA,
        /**
         * Represents red
         */
        RED,
        /**
         * Represents light purple
         */
        LIGHT_PURPLE,
        /**
         * Represents yellow
         */
        YELLOW,
        /**
         * Represents white
         */
        WHITE,
        /**
         * Represents magical characters that change around randomly
         */
        MAGIC,
        /**
         * Makes the text bold.
         */
        BOLD,
        /**
         * Makes a line appear through the text.
         */
        STRIKETHROUGH,
        /**
         * Makes the text appear underlined.
         */
        UNDERLINE,
        /**
         * Makes the text italic.
         */
        ITALIC,
        /**
         * Resets all previous chat colors or formats.
         */
        RESET
    }

    private static Dictionary<Colors, ChatColor> AllColors = new Dictionary<Colors, ChatColor>
        {
        /**
         * Represents black
         */
        { Colors.BLACK, new ChatColor('0', 0x00) },
        /**
         * Represents dark blue
         */
        { Colors.DARK_BLUE, new ChatColor('1', 0x1) },
        /**
         * Represents dark green
         */
        { Colors.DARK_GREEN, new ChatColor('2', 0x2) },
        /**
         * Represents dark blue (aqua)
         */
        { Colors.DARK_AQUA, new ChatColor('3', 0x3) },
        /**
         * Represents dark red
         */
        { Colors.DARK_RED, new ChatColor('4', 0x4) },
        /**
         * Represents dark purple
         */
        { Colors.DARK_PURPLE, new ChatColor('5', 0x5) },
        /**
         * Represents gold
         */
        { Colors.GOLD, new ChatColor('6', 0x6) },
        /**
         * Represents gray
         */
        { Colors.GRAY, new ChatColor('7', 0x7) },
        /**
         * Represents dark gray
         */
        { Colors.DARK_GRAY, new ChatColor('8', 0x8) },
        /**
         * Represents blue
         */
        { Colors.BLUE, new ChatColor('9', 0x9) },
        /**
         * Represents green
         */
        { Colors.GREEN, new ChatColor('a', 0xA) },
        /**
         * Represents aqua
         */
        { Colors.AQUA, new ChatColor('b', 0xB) },
        /**
         * Represents red
         */
        { Colors.RED, new ChatColor('c', 0xC) },
        /**
         * Represents light purple
         */
        { Colors.LIGHT_PURPLE, new ChatColor('d', 0xD) },
        /**
         * Represents yellow
         */
        { Colors.YELLOW, new ChatColor('e', 0xE) },
        /**
         * Represents white
         */
        { Colors.WHITE, new ChatColor('f', 0xF) },
        /**
         * Represents magical characters that change around randomly
         */
        { Colors.MAGIC, new ChatColor('k', 0x10, true) },
        /**
         * Makes the text bold.
         */
        { Colors.BOLD, new ChatColor('l', 0x11, true) },
        /**
         * Makes a line appear through the text.
         */
        { Colors.STRIKETHROUGH, new ChatColor('m', 0x12, true) },
        /**
         * Makes the text appear underlined.
         */
        { Colors.UNDERLINE, new ChatColor('n', 0x13, true) },
        /**
         * Makes the text italic.
         */
        { Colors.ITALIC, new ChatColor('o', 0x14, true) },
        /**
         * Resets all previous chat colors or formats.
         */
        { Colors.RESET, new ChatColor('r', 0x15) }
    };

    /**
     * The special character which prefixes all chat colour codes. Use this if
     * you need to dynamically convert colour codes from your custom format.
     */
    public static readonly char COLOR_CHAR = '\u00A7';
    private static readonly Regex STRIP_COLOR_PATTERN = new Regex("(?i)" + COLOR_CHAR + "[0-9A-FK-OR]");
        
    private readonly int intCode;
    private readonly char code;
    private readonly bool isformat;
    private readonly string tostring;

    private ChatColor(char code, int intCode) {
        new ChatColor(code, intCode, false);
    }

    private ChatColor(char code, int intCode, bool isFormat) {
        this.code = code;
        this.intCode = intCode;
        this.isformat = isFormat;
        this.tostring = new string(new char[] {COLOR_CHAR, code});
    }

    /**
     * Gets the char value associated with this color
     *
     * @return A char value of this color code
     */
    public char getChar() {
        return code;
    }
    
    public override string ToString() {
        return tostring;
    }

    /**
     * Checks if this code is a format code as opposed to a color code.
     * 
     * @return whether this ChatColor is a format code
     */
    public bool isFormat() {
        return isformat;
    }

    /**
     * Checks if this code is a color code as opposed to a format code.
     * 
     * @return whether this ChatColor is a color code
     */
    public bool isColor() {
        return !isformat && this.code != 'r';
    }

    /**
     * Gets the color represented by the specified color code
     *
     * @param code Code to check
     * @return Associative {@link org.bukkit.ChatColor} with the given code,
     *     or null if it doesn't exist
     */
    public static ChatColor getByChar(char code)
    {
        return AllColors.Values.FirstOrDefault(c => c.code == code);
    }

    /**
     * Gets the color represented by the specified color code
     *
     * @param code Code to check
     * @return Associative {@link org.bukkit.ChatColor} with the given code,
     *     or null if it doesn't exist
     */

    public static ChatColor getByChar(string code) {
        if (code == null)
            throw new ArgumentNullException(code, "Code cannot be null");
        if (code.Length == 0)
            throw new ArgumentException("Code must have at least one char", code);

        return AllColors.Values.FirstOrDefault(c => c.code == code[0]);
    }

    /**
     * Strips the given message of all color codes
     *
     * @param input String to strip of color
     * @return A copy of the input string, without any coloring
     */
    public static String stripColor(String input) {
        if (input == null) {
            return null;
        }

        return STRIP_COLOR_PATTERN.Replace(input, "");
    }

    /**
     * Translates a string using an alternate color code character into a
     * string that uses the internal ChatColor.COLOR_CODE color code
     * character. The alternate color code character will only be replaced if
     * it is immediately followed by 0-9, A-F, a-f, K-O, k-o, R or r.
     *
     * @param altColorChar The alternate color code character to replace. Ex: {@literal &}
     * @param textToTranslate Text containing the alternate color code character.
     * @return Text containing the ChatColor.COLOR_CODE color code character.
     */
    public static String translateAlternateColorCodes(char altColorChar, String textToTranslate) {
        char[] b = textToTranslate.ToCharArray();
        for (int i = 0; i < b.Length - 1; i++) {
            if (b[i] == altColorChar && "0123456789AaBbCcDdEeFfKkLlMmNnOoRr".IndexOf(b[i+1]) > -1) {
                b[i] = ChatColor.COLOR_CHAR;
                b[i+1] = char.ToLower(b[i+1]);
            }
        }
        return new String(b);
    }

    /**
     * Gets the ChatColors used at the end of the given input string.
     *
     * @param input Input string to retrieve the colors from.
     * @return Any remaining ChatColors to pass onto the next line.
     */
    public static String getLastColors(String input) {
        String result = "";
        int length = input.Length;

        // Search backwards from the end as it is faster
        for (int index = length - 1; index > -1; index--) {
            char section = input[index];
            if (section == COLOR_CHAR && index < length - 1) {
                char c = input[index + 1];
                ChatColor color = getByChar(c);

                if (color != null) {
                    result = color.ToString() + result;

                    // Once we find a color or reset we can stop searching
                    if (color.isColor() || color.Equals(AllColors[Colors.RESET])) {
                        break;
                    }
                }
            }
        }

        return result;
    }
}
