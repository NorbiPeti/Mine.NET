using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    public class ItemMergeEventArgs : EntityEventArgs<Item>, Cancellable {
        
        private bool cancelled;
        private readonly Item target;

        public ItemMergeEventArgs(Item item, Item target) : base(item)
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
    }
}
