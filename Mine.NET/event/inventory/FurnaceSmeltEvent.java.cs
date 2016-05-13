using Mine.NET.block;
using Mine.NET.Event.block;
using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when an ItemStack is successfully smelted in a furnace.
     */
    public class FurnaceSmeltEvent : BlockEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly ItemStack source;
        private ItemStack result;
        private bool cancelled;

        public FurnaceSmeltEvent(Block furnace, ItemStack source, ItemStack result) : base(furnace)
        {
            this.source = source;
            this.result = result;
            this.cancelled = false;
        }

        /**
         * Gets the smelted ItemStack for this event
         *
         * @return smelting source ItemStack
         */
        public ItemStack getSource()
        {
            return source;
        }

        /**
         * Gets the resultant ItemStack for this event
         *
         * @return smelting result ItemStack
         */
        public ItemStack getResult()
        {
            return result;
        }

        /**
         * Sets the resultant ItemStack for this event
         *
         * @param result new result ItemStack
         */
        public void setResult(ItemStack result)
        {
            this.result = result;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
