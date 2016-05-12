using Mine.NET.inventory;
using System;

namespace Mine.NET.entity
{
    /**
     * Represents a human entity, such as an NPC or a player
     */
    public interface HumanEntity : LivingEntity, AnimalTamer, Permissible, InventoryHolder
    {
        /**
         * Get the player's EnderChest inventory
         *
         * @return The EnderChest of the player
         */
        Inventory getEnderChest();

        /**
         * Gets the players selected main hand
         *
         * @return the players main hand
         */
        MainHand getMainHand();

        /**
         * If the player currently has an inventory window open, this method will
         * set a property of that window, such as the state of a progress bar.
         *
         * @param prop The property.
         * @param value The value to set the property to.
         * @return True if the property was successfully set.
         */
        bool setWindowProperty(InventoryView.Property prop, int value);

        /**
         * Gets the inventory view the player is currently viewing. If they do not
         * have an inventory window open, it returns their internal crafting view.
         *
         * @return The inventory view.
         */
        InventoryView getOpenInventory();

        /**
         * Opens an inventory window with the specified inventory on the top and
         * the player's inventory on the bottom.
         *
         * @param inventory The inventory to open
         * @return The newly opened inventory view
         */
        InventoryView openInventory(Inventory inventory);

        /**
         * Opens an empty workbench inventory window with the player's inventory
         * on the bottom.
         *
         * @param location The location to attach it to. If null, the player's
         *     location is used.
         * @param force If false, and there is no workbench block at the location,
         *     no inventory will be opened and null will be returned.
         * @return The newly opened inventory view, or null if it could not be
         *     opened.
         */
        InventoryView openWorkbench(Location location, bool force);

        /**
         * Opens an empty enchanting inventory window with the player's inventory
         * on the bottom.
         *
         * @param location The location to attach it to. If null, the player's
         *     location is used.
         * @param force If false, and there is no enchanting table at the
         *     location, no inventory will be opened and null will be returned.
         * @return The newly opened inventory view, or null if it could not be
         *     opened.
         */
        InventoryView openEnchanting(Location location, bool force);

        /**
         * Opens an inventory window to the specified inventory view.
         *
         * @param inventory The view to open
         */
        void openInventory(InventoryView inventory);

        /**
         * Starts a trade between the player and the villager.
         *
         * Note that only one player may trade with a villager at once. You must use
         * the force parameter for this.
         *
         * @param trader The merchant to trade with. Cannot be null.
         * @param force whether to force the trade even if another player is trading
         * @return The newly opened inventory view, or null if it could not be
         * opened.
         */
        InventoryView openMerchant(Villager trader, bool force);

        /**
         * Force-closes the currently open inventory view for this player, if any.
         */
        void closeInventory();

        /**
         * Returns the ItemStack currently in your hand, can be empty.
         *
         * @return The ItemStack of the item you are currently holding.
         * [Obsolete] Humans may now dual wield in their off hand, use explicit
         * methods in {@link PlayerInventory}.
         */
        [Obsolete]
        ItemStack getItemInHand();

        /**
         * Sets the item to the given ItemStack, this will replace whatever the
         * user was holding.
         *
         * @param item The ItemStack which will end up in the hand
         * [Obsolete] Humans may now dual wield in their off hand, use explicit
         * methods in {@link PlayerInventory}.
         */
        [Obsolete]
        void setItemInHand(ItemStack item);

        /**
         * Returns the ItemStack currently on your cursor, can be empty. Will
         * always be empty if the player currently has no open window.
         *
         * @return The ItemStack of the item you are currently moving around.
         */
        ItemStack getItemOnCursor();

        /**
         * Sets the item to the given ItemStack, this will replace whatever the
         * user was moving. Will always be empty if the player currently has no
         * open window.
         *
         * @param item The ItemStack which will end up in the hand
         */
        void setItemOnCursor(ItemStack item);

        /**
         * Returns whether this player is slumbering.
         *
         * @return slumber state
         */
        bool isSleeping();

        /**
         * Get the sleep ticks of the player. This value may be capped.
         *
         * @return slumber ticks
         */
        int getSleepTicks();

        /**
         * Gets this human's current {@link GameMode}
         *
         * @return Current game mode
         */
        GameMode getGameMode();

        /**
         * Sets this human's current {@link GameMode}
         *
         * @param mode New game mode
         */
        void setGameMode(GameMode mode);

        /**
         * Check if the player is currently blocking (ie with a sword).
         *
         * @return Whether they are blocking.
         */
        bool isBlocking();

        /**
         * Get the total amount of experience required for the player to level
         *
         * @return Experience required to level up
         */
        int getExpToLevel();
    }
}
