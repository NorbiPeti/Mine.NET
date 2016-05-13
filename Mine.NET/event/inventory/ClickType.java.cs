namespace Mine.NET.Event.inventory
{
    /**
     * What the client did to trigger this action (not the result).
     */
    public enum ClickTypes
    {

        /**
         * The left (or primary) mouse button.
         */
        LEFT,
        /**
         * Holding shift while pressing the left mouse button.
         */
        SHIFT_LEFT,
        /**
         * The right mouse button.
         */
        RIGHT,
        /**
         * Holding shift while pressing the right mouse button.
         */
        SHIFT_RIGHT,
        /**
         * Clicking the left mouse button on the grey area around the inventory.
         */
        WINDOW_BORDER_LEFT,
        /**
         * Clicking the right mouse button on the grey area around the inventory.
         */
        WINDOW_BORDER_RIGHT,
        /**
         * The middle mouse button, or a "scrollwheel click".
         */
        MIDDLE,
        /**
         * One of the number keys 1-9, correspond to slots on the hotbar.
         */
        NUMBER_KEY,
        /**
         * Pressing the left mouse button twice in quick succession.
         */
        DOUBLE_CLICK,
        /**
         * The "Drop" key (defaults to Q).
         */
        DROP,
        /**
         * Holding Ctrl while pressing the "Drop" key (defaults to Q).
         */
        CONTROL_DROP,
        /**
         * Any action done with the Creative inventory open.
         */
        CREATIVE,
        /**
         * A type of inventory manipulation not yet recognized by Bukkit.
         * <p>
         * This is only for transitional purposes on a new Minecraft update, and
         * should never be relied upon.
         * <p>
         * Any ClickType.UNKNOWN is called on a best-effort basis.
         */
        UNKNOWN
    }

    public static class ClickType
    {/**
     * Gets whether this ClickType represents the pressing of a key on a
     * keyboard.
     *
     * @return true if this ClickType represents the pressing of a key
     */
        public static bool isKeyboardClick(ClickTypes type)
        {
            return (type == ClickTypes.NUMBER_KEY) || (type == ClickTypes.DROP) || (type == ClickTypes.CONTROL_DROP);
        }

        /**
         * Gets whether this ClickType represents an action that can only be
         * performed by a Player in creative mode.
         *
         * @return true if this action requires Creative mode
         */
        public static bool isCreativeAction(ClickTypes type)
        {
            // Why use middle click?
            return (type == ClickTypes.MIDDLE) || (type == ClickTypes.CREATIVE);
        }

        /**
         * Gets whether this ClickType represents a right click.
         *
         * @return true if this ClickType represents a right click
         */
        public static bool isRightClick(ClickTypes type)
        {
            return (type == ClickTypes.RIGHT) || (type == ClickTypes.SHIFT_RIGHT);
        }

        /**
         * Gets whether this ClickType represents a left click.
         *
         * @return true if this ClickType represents a left click
         */
        public static bool isLeftClick(ClickTypes type)
        {
            return (type == ClickTypes.LEFT) || (type == ClickTypes.SHIFT_LEFT) || (type == ClickTypes.DOUBLE_CLICK) || (type == ClickTypes.CREATIVE);
        }

        /**
         * Gets whether this ClickType indicates that the shift key was pressed
         * down when the click was made.
         *
         * @return true if the action uses Shift.
         */
        public static bool isShiftClick(ClickTypes type)
        {
            return (type == ClickTypes.SHIFT_LEFT) || (type == ClickTypes.SHIFT_RIGHT) || (type == ClickTypes.CONTROL_DROP);
        }
    }
}
