using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * This event is called when a player in creative mode puts down or picks up
     * an item in their inventory / hotbar and when they drop items from their
     * Inventory while in creative mode.
     */
    public class InventoryCreativeEvent : InventoryClickEvent
    {
        private ItemStack item;

        public InventoryCreativeEvent(InventoryView what, SlotType type, int slot, ItemStack newItem) :
            base(what, type, slot, ClickTypes.CREATIVE, InventoryAction.PLACE_ALL)
        {
            this.item = newItem;
        }

        public ItemStack getCursor()
        {
            return item;
        }

        public void setCursor(ItemStack item)
        {
            this.item = item;
        }
    }
}
