using System.Collections.Generic;

namespace Mine.NET.map
{
    /**
     * Represents all the map cursors on a {@link MapCanvas}. Like MapCanvas, a
     * MapCursorCollection is linked to a specific {@link MapRenderer}.
     */
    public sealed class MapCursorCollection
    {
        private List<MapCursor> cursors = new List<MapCursor>();

        /**
         * Get the amount of cursors in this collection.
         *
         * @return The size of this collection.
         */
        public int size()
        {
            return cursors.Count;
        }

        /**
         * Get a cursor from this collection.
         *
         * @param index The index of the cursor.
         * @return The MapCursor.
         */
        public MapCursor getCursor(int index)
        {
            return cursors[index];
        }

        /**
         * Remove a cursor from the collection.
         *
         * @param cursor The MapCursor to remove.
         * @return Whether the cursor was removed successfully.
         */
        public bool removeCursor(MapCursor cursor)
        {
            return cursors.Remove(cursor);
        }

        /**
         * Add a cursor to the collection.
         *
         * @param cursor The MapCursor to add.
         * @return The MapCursor that was passed.
         */
        public MapCursor addCursor(MapCursor cursor)
        {
            cursors.Add(cursor);
            return cursor;
        }

        /**
         * Add a cursor to the collection.
         *
         * @param x The x coordinate, from -128 to 127.
         * @param y The y coordinate, from -128 to 127.
         * @param direction The facing of the cursor, from 0 to 15.
         * @return The newly added MapCursor.
         */
        public MapCursor addCursor(int x, int y, byte direction)
        {
            return addCursor(x, y, direction, (byte)0, true);
        }

        /**
         * Add a cursor to the collection.
         *
         * @param x The x coordinate, from -128 to 127.
         * @param y The y coordinate, from -128 to 127.
         * @param direction The facing of the cursor, from 0 to 15.
         * @param type The type (color/style) of the map cursor.
         * @return The newly added MapCursor.
         * [Obsolete] Magic value
         */
        public MapCursor addCursor(int x, int y, byte direction, MapCursor.Type type)
        {
            return addCursor(x, y, direction, type, true);
        }

        /**
         * Add a cursor to the collection.
         *
         * @param x The x coordinate, from -128 to 127.
         * @param y The y coordinate, from -128 to 127.
         * @param direction The facing of the cursor, from 0 to 15.
         * @param type The type (color/style) of the map cursor.
         * @param visible Whether the cursor is visible.
         * @return The newly added MapCursor.
         * [Obsolete] Magic value
         */
        public MapCursor addCursor(int x, int y, byte direction, MapCursor.Type type, bool visible)
        {
            return addCursor(new MapCursor((byte)x, (byte)y, direction, type, visible));
        }

    }
}
