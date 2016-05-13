using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Thrown when a player drops an item from their inventory
     */
    public class PlayerDropItemEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Item drop;
        private bool cancel = false;

        public PlayerDropItemEvent(Player player, Item drop) : base(player)
        {
            this.drop = drop;
        }

        /**
         * Gets the ItemDrop created by the player
         *
         * @return ItemDrop created by the player
         */
        public Item getItemDrop()
        {
            return drop;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
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
