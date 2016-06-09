using Mine.NET.inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mine.NET.Event.inventory
{
    /**
     * This event is called when the player drags an item in their cursor across
     * the inventory. The ItemStack is distributed across the slots the
     * HumanEntity dragged over. The method of distribution is described by the 
     * DragType returned by {@link #getType()}.
     * <p>
     * Canceling this event will result in none of the changes described in
     * {@link #getNewItems()} being applied to the Inventory.
     * <p>
     * Because InventoryDragEvent occurs within a modification of the Inventory,
     * not all Inventory related methods are safe to use.
     * <p>
     * The following should never be invoked by an EventHandler for
     * InventoryDragEvent using the HumanEntity or InventoryView associated with
     * this event.
     * <ul>
     * <li>{@link HumanEntity#closeInventory()}
     * <li>{@link HumanEntity#openInventory(Inventory)}
     * <li>{@link HumanEntity#openWorkbench(Location, bool)}
     * <li>{@link HumanEntity#openEnchanting(Location, bool)}
     * <li>{@link InventoryView#close()}
     * </ul>
     * To invoke one of these methods, schedule a task using 
     * {@link BukkitScheduler#runTask(Plugin, Action)}, which will run the task
     * on the next tick.  Also be aware that this is not an exhaustive list, and
     * other methods could potentially create issues as well.
     * <p>
     * Assuming the EntityHuman associated with this event is an instance of a
     * Player, manipulating the MaxStackSize or contents of an Inventory will
     * require an Invocation of {@link Player#updateInventory()}.
     * <p>
     * Any modifications to slots that are modified by the results of this
     * InventoryDragEvent will be overwritten. To change these slots, this event
     * should be cancelled and the changes applied. Alternatively, scheduling a
     * task using {@link BukkitScheduler#runTask(Plugin, Action)}, which would
     * execute the task on the next tick, would work as well.
     */
    public class InventoryDragEventArgs : InventoryInteractEventArgs
    {
        private readonly DragType type;
        private readonly Dictionary<int, ItemStack> addedItems;
        private readonly HashSet<int> containerSlots;
        private readonly ItemStack oldCursor;
        private ItemStack newCursor;

        public InventoryDragEventArgs(InventoryView what, ItemStack newCursor, ItemStack oldCursor, bool right, Dictionary<int, ItemStack> slots) :
            base(what)
        {
            if (oldCursor == null) throw new ArgumentNullException(nameof(oldCursor));
            if(slots == null) throw new ArgumentNullException(nameof(slots));

            type = right ? DragType.SINGLE : DragType.EVEN;
            this.newCursor = newCursor;
            this.oldCursor = oldCursor;
            this.addedItems = slots;
            containerSlots = new HashSet<int>(slots.Keys.Select(s => what.convertSlot(s)));
        }

        /**
         * Gets all items to be added to the inventory in this drag.
         *
         * @return map from raw slot id to new ItemStack
         */
        public ReadOnlyDictionary<int, ItemStack> getNewItems()
        {
            return new ReadOnlyDictionary<int, ItemStack>(addedItems);
        }

        /**
         * Gets the raw slot ids to be changed in this drag.
         *
         * @return list of raw slot ids, suitable for getView().getItem(int)
         */
        public HashSet<int> getRawSlots()
        {
            return new HashSet<int>(addedItems.Keys);
        }

        /**
         * Gets the slots to be changed in this drag.
         *
         * @return list of converted slot ids, suitable for {@link
         *     org.bukkit.inventory.Inventory#getItem(int)}.
         */
        public HashSet<int> getInventorySlots()
        {
            return containerSlots;
        }

        /**
         * Gets the result cursor after the drag is done. The returned value is
         * mutable.
         *
         * @return the result cursor
         */
        public ItemStack getCursor()
        {
            return newCursor;
        }

        /**
         * Sets the result cursor after the drag is done.
         * <p>
         * Changing this item stack changes the cursor item. Note that changing
         * the affected "dragged" slots does not change this ItemStack, nor does
         * changing this ItemStack affect the "dragged" slots.
         *
         * @param newCursor the new cursor ItemStack
         */
        public void setCursor(ItemStack newCursor)
        {
            this.newCursor = newCursor;
        }

        /**
         * Gets an ItemStack representing the cursor prior to any modifications
         * as a result of this drag.
         *
         * @return the original cursor
         */
        public ItemStack getOldCursor()
        {
            return oldCursor.Clone();
        }

        /**
         * Gets the DragType that describes the behavior of ItemStacks placed
         * after this InventoryDragEvent.
         * <p>
         * The ItemStacks and the raw slots that they're being applied to can be
         * found using {@link #getNewItems()}.
         *
         * @return the DragType of this InventoryDragEvent
         */
        public DragType getType()
        {
            return type;
        }
    }
}
