using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a firework explodes.
     */
    public class FireworkExplodeEvent : EntityEvent, Cancellable {

        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel;

        public FireworkExplodeEvent(Firework what) : base(what)
        {
        }

        public override bool isCancelled() {
            return cancel;
        }

        /**
         * Set the cancelled state of this event. If the firework explosion is
         * cancelled, the firework will still be removed, but no particles will be
         * displayed.
         *
         * @param cancel whether to cancel or not.
         */
        public override void setCancelled(bool cancel) {
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
