using Mine.NET.entity;

namespace Mine.NET.Event.hanging
{
    /**
     * Triggered when a hanging entity is removed
     */
    public class HangingBreakEventArgs : HangingEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly RemoveCause cause;

        public HangingBreakEventArgs(Hanging hanging, RemoveCause cause) : base(hanging)
        {
            this.cause = cause;
        }

        /**
         * Gets the cause for the hanging entity's removal
         *
         * @return the RemoveCause for the hanging entity's removal
         */
        public RemoveCause getCause()
        {
            return cause;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }

        /**
         * An enum to specify the cause of the removal
         */
        public enum RemoveCause
        {
            /**
             * Removed by an entity
             */
            ENTITY,
            /**
             * Removed by an explosion
             */
            EXPLOSION,
            /**
             * Removed by placing a block on it
             */
            OBSTRUCTION,
            /**
             * Removed by destroying the block behind it, etc
             */
            PHYSICS,
            /**
             * Removed by an uncategorised cause
             */
            DEFAULT,
        }
    }
}
