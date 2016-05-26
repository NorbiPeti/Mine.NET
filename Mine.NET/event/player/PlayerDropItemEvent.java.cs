using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Thrown when a player drops an item from their inventory
     */
    public class PlayerDropItemEventArgs : PlayerEventArgs, Cancellable
    {
        private readonly Item drop;
        private bool cancel = false;

        public PlayerDropItemEventArgs(Player player, Item drop) : base(player)
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
    }
}
