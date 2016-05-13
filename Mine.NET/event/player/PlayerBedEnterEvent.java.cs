using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * This event is fired when the player is almost about to enter the bed.
     */
    public class PlayerBedEnterEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel = false;
        private readonly Block bed;

        public PlayerBedEnterEvent(Player who, Block bed) : base(who)
        {
            this.bed = bed;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Returns the bed block involved in this event.
         *
         * @return the bed block involved in this event
         */
        public Block getBed()
        {
            return bed;
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
