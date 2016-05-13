using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    public class ItemMergeEvent : EntityEvent<Item>, Cancellable {

        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private readonly Item target;

        public ItemMergeEvent(Item item, Item target) : base(item)
        {
            this.target = target;
        }

        public bool isCancelled() {
            return cancelled;
        }

        public void setCancelled(bool cancelled) {
            this.cancelled = cancelled;
        }
        
        /**
         * Gets the Item entity the main Item is being merged into.
         *
         * @return The Item being merged with
         */
        public Item getTarget() {
            return target;
        }

        public override HandlerList getHandlers() {
            return handlers;
        }

        public static HandlerList getHandlerList() {
            return handlers;
        }
    }
}
