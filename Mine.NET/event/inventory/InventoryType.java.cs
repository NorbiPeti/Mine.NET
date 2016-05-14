using System;

namespace Mine.NET.Event.inventory
{
    public class InventoryType
    {

        /**
         * A chest inventory, with 0, 9, 18, 27, 36, 45, or 54 slots of type
         * CONTAINER.
         */
        public static readonly InventoryType CHEST = new InventoryType(27, "Chest");
        /**
         * A dispenser inventory, with 9 slots of type CONTAINER.
         */
        public static readonly InventoryType DISPENSER = new InventoryType(9, "Dispenser");
        /**
         * A dropper inventory, with 9 slots of type CONTAINER.
         */
        public static readonly InventoryType DROPPER = new InventoryType(9, "Dropper");
        /**
         * A furnace inventory, with a RESULT slot, a CRAFTING slot, and a FUEL
         * slot.
         */
        public static readonly InventoryType FURNACE = new InventoryType(3, "Furnace");
        /**
         * A workbench inventory, with 9 CRAFTING slots and a RESULT slot.
         */
        public static readonly InventoryType WORKBENCH = new InventoryType(10, "Crafting");
        /**
         * A player's crafting inventory, with 4 CRAFTING slots and a RESULT slot.
         * Also implies that the 4 ARMOR slots are accessible.
         */
        public static readonly InventoryType CRAFTING = new InventoryType(5, "Crafting");
        /**
         * An enchantment table inventory, with two CRAFTING slots and three
         * enchanting buttons.
         */
        public static readonly InventoryType ENCHANTING = new InventoryType(2, "Enchanting");
        /**
         * A brewing stand inventory, with one FUEL slot and three CRAFTING slots.
         */
        public static readonly InventoryType BREWING = new InventoryType(5, "Brewing");
        /**
         * A player's inventory, with 9 QUICKBAR slots, 27 CONTAINER slots, 4 ARMOR
         * slots and 1 offhand slot. The ARMOR and offhand slots may not be visible
         * to the player, though.
         */
        public static readonly InventoryType PLAYER = new InventoryType(41, "Player");
        /**
         * The creative mode inventory, with only 9 QUICKBAR slots and nothing
         * else. (The actual creative interface with the items is client-side and
         * cannot be altered by the server.)
         */
        public static readonly InventoryType CREATIVE = new InventoryType(9, "Creative");
        /**
         * The merchant inventory, with 2 TRADE-IN slots, and 1 RESULT slot.
         */
        public static readonly InventoryType MERCHANT = new InventoryType(3, "Villager");
        /**
         * The ender chest inventory, with 27 slots.
         */
        public static readonly InventoryType ENDER_CHEST = new InventoryType(27, "Ender Chest");
        /**
         * An anvil inventory, with 2 CRAFTING slots and 1 RESULT slot
         */
        public static readonly InventoryType ANVIL = new InventoryType(3, "Repairing");
        /**
         * A beacon inventory, with 1 CRAFTING slot
         */
        public static readonly InventoryType BEACON = new InventoryType(1, "container.beacon");
        /**
         * A hopper inventory, with 5 slots of type CONTAINER.
         */
        public static readonly InventoryType HOPPER = new InventoryType(5, "Item Hopper");

        private readonly int size;
        private readonly String title;

        private InventoryType(int defaultSize, String defaultTitle)
        {
            size = defaultSize;
            title = defaultTitle;
        }

        public int getDefaultSize()
        {
            return size;
        }

        public String getDefaultTitle()
        {
            return title;
        }

        public enum SlotType
        {
            /**
             * A result slot in a furnace or crafting inventory.
             */
            RESULT,
            /**
             * A slot in the crafting matrix, or the input slot in a furnace
             * inventory, the potion slot in the brewing stand, or the enchanting
             * slot.
             */
            CRAFTING,
            /**
             * An armour slot in the player's inventory.
             */
            ARMOR,
            /**
             * A regular slot in the container or the player's inventory; anything
             * not covered by the other enum values.
             */
            CONTAINER,
            /**
             * A slot in the bottom row or quickbar.
             */
            QUICKBAR,
            /**
             * A pseudo-slot representing the area outside the inventory window.
             */
            OUTSIDE,
            /**
             * The fuel slot in a furnace inventory, or the ingredient slot in a
             * brewing stand inventory.
             */
            FUEL
        }
    }
}
