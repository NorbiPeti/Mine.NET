package org.bukkit.map;

import java.util.HashMap;

/**
 * Represents a bitmap font drawable to a map.
 */
public class MapFont {

    private readonly HashMap<char, charSprite> chars = new Dictionary<char, charSprite>();
    private int height = 0;
    protected bool malleable = true;

    /**
     * Set the sprite for a given char.
     *
     * @param ch The char to set the sprite for.
     * @param sprite The charSprite to set.
     * @throws IllegalStateException if this font is static.
     */
    public void setChar(char ch, charSprite sprite) {
        if (!malleable) {
            throw new IllegalStateException("this font is not malleable");
        }

        chars.Add(ch, sprite);
        if (sprite.getHeight() > height) {
            height = sprite.getHeight();
        }
    }

    /**
     * Get the sprite for a given char.
     *
     * @param ch The char to get the sprite for.
     * @return The charSprite associated with the char, or null if
     *     there is none.
     */
    public charSprite getChar(char ch) {
        return chars[ch];
    }

    /**
     * Get the width of the given text as it would be rendered using this
     * font.
     *
     * @param text The text.
     * @return The width in pixels.
     */
    public int getWidth(String text) {
        if (!isValid(text)) {
            throw new ArgumentException("text contains invalid chars");
        }

        if (text.Length == 0){
            return 0;
        }

        int result = 0;
        for (int i = 0; i < text.Length; ++i) {
            result += chars[text[i]].getWidth();
        }
        result += text.Length - 1; // Account for 1px spacing between chars

        return result;
    }

    /**
     * Get the height of this font.
     *
     * @return The height of the font.
     */
    public int getHeight() {
        return height;
    }

    /**
     * Check whether the given text is valid.
     *
     * @param text The text.
     * @return True if the string contains only defined chars, false
     *     otherwise.
     */
    public bool isValid(String text) {
        for (int i = 0; i < text.Length; ++i) {
            char ch = text[i];
            if (ch == '\u00A7' || ch == '\n') continue;
            if (chars[ch] == null) return false;
        }
        return true;
    }

    /**
     * Represents the graphics for a single char in a MapFont.
     */
    public static class charSprite {

        private readonly int width;
        private readonly int height;
        private readonly bool[] data;

        public charSprite(int width, int height, bool[] data) {
            this.width = width;
            this.height = height;
            this.data = data;

            if (data.length != width * height) {
                throw new ArgumentException("size of data does not match dimensions");
            }
        }

        /**
         * Get the value of a pixel of the char.
         *
         * @param row The row, in the range [0,8).
         * @param col The column, in the range [0,8).
         * @return True if the pixel is solid, false if transparent.
         */
        public bool get(int row, int col) {
            if (row < 0 || col < 0 || row >= height || col >= width) return false;
            return data[row * width + col];
        }

        /**
         * Get the width of the char sprite.
         *
         * @return The width of the char.
         */
        public int getWidth() {
            return width;
        }

        /**
         * Get the height of the char sprite.
         *
         * @return The height of the char.
         */
        public int getHeight() {
            return height;
        }

    }

}
