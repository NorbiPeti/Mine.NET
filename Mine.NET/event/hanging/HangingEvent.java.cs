using Mine.NET.entity;

namespace Mine.NET.Event.hanging
{
    /**
     * Represents a hanging entity-related event.
     */
    public abstract class HangingEventArgs : GameEventArgs
    {
        protected Hanging hanging;

        protected HangingEventArgs(Hanging painting)
        {
            this.hanging = painting;
        }

        /**
         * Gets the hanging entity involved in this event.
         *
         * @return the hanging entity
         */
        public Hanging getEntity()
        {
            return hanging;
        }
    }
}
