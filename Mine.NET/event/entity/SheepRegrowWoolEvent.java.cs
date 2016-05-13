using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a sheep regrows its wool
     */
    public class SheepRegrowWoolEvent : EntityEvent<Sheep>, Cancellable {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel;

        public SheepRegrowWoolEvent(Sheep sheep) : base(sheep)
        {
            this.cancel = false;
        }

        public bool isCancelled() {
            return cancel;
        }

        public void setCancelled(bool cancel) {
            this.cancel = cancel;
        }

        public override HandlerList getHandlers() {
            return handlers;
        }

        public static HandlerList getHandlerList() {
            return handlers;
        }

    }
}
