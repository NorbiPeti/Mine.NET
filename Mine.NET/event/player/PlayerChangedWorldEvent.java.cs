using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player switches to another world.
     */
    public class PlayerChangedWorldEvent : PlayerEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly World from;

        public PlayerChangedWorldEvent(Player player, World from) : base(player)
        {
            this.from = from;
        }

        /**
         * Gets the world the player is switching from.
         *
         * @return  player's previous world
         */
        public World getFrom()
        {
            return from;
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
