using System;

namespace Mine.NET.map
{
    /**
     * Represents a cursor on a map.
     */
    public sealed class MapCursor
    {
        private byte x, y;
        private byte direction;
        private Type type;
        private bool visible;

        /**
         * Initialize the map cursor.
         *
         * @param x The x coordinate, from -128 to 127.
         * @param y The y coordinate, from -128 to 127.
         * @param direction The facing of the cursor, from 0 to 15.
         * @param type The type (color/style) of the map cursor.
         * @param visible Whether the cursor is visible by default.
         * [Obsolete] Magic value
         */
        public MapCursor(byte x, byte y, byte direction, Type type, bool visible)
        {
            this.x = x;
            this.y = y;
            setDirection(direction);
            this.type = type;
            this.visible = visible;
        }

        /**
         * Get the X position of this cursor.
         *
         * @return The X coordinate.
         */
        public byte getX()
        {
            return x;
        }

        /**
         * Get the Y position of this cursor.
         *
         * @return The Y coordinate.
         */
        public byte getY()
        {
            return y;
        }

        /**
         * Get the direction of this cursor.
         *
         * @return The facing of the cursor, from 0 to 15.
         */
        public byte getDirection()
        {
            return direction;
        }

        /**
         * Get the type of this cursor.
         *
         * @return The type (color/style) of the map cursor.
         */
        public Type getType()
        {
            return type;
        }

        /**
         * Get the visibility status of this cursor.
         *
         * @return True if visible, false otherwise.
         */
        public bool isVisible()
        {
            return visible;
        }

        /**
         * Set the X position of this cursor.
         *
         * @param x The X coordinate.
         */
        public void setX(byte x)
        {
            this.x = x;
        }

        /**
         * Set the Y position of this cursor.
         *
         * @param y The Y coordinate.
         */
        public void setY(byte y)
        {
            this.y = y;
        }

        /**
         * Set the direction of this cursor.
         *
         * @param direction The facing of the cursor, from 0 to 15.
         */
        public void setDirection(byte direction)
        {
            if (direction < 0 || direction > 15)
            {
                throw new ArgumentException("Direction must be in the range 0-15");
            }
            this.direction = direction;
        }

        /**
         * Set the type of this cursor.
         *
         * @param type The type (color/style) of the map cursor.
         */
        public void setType(Type type)
        {
            this.type = type;
        }

        /**
         * Set the visibility status of this cursor.
         *
         * @param visible True if visible.
         */
        public void setVisible(bool visible)
        {
            this.visible = visible;
        }

        /**
         * Represents the standard types of map cursors. More may be made
         * available by texture packs - the value is used by the client as an
         * index in the file './misc/mapicons.png' from minecraft.jar or from a
         * texture pack.
         */
        public enum Type
        {
            WHITE_POINTER = 0,
            GREEN_POINTER = 1,
            RED_POINTER = 2,
            BLUE_POINTER = 3,
            WHITE_CROSS = 4
        }
    }
}
