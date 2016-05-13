using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player is about to teleport because it is in contact with a
     * portal.
     * <p>
     * For other entities see {@link org.bukkit.event.entity.EntityPortalEvent}
     */
    public class PlayerPortalEvent : PlayerTeleportEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        protected TravelAgent travelAgent;

        public PlayerPortalEvent(Player player, Location from, Location to, TravelAgent pta) :
            base(player, from, to)
        {
            this.travelAgent = pta;
        }

        public PlayerPortalEvent(Player player, Location from, Location to, TravelAgent pta, TeleportCause cause) :
            base(player, from, to, cause)
        {
            this.travelAgent = pta;
        }

        /**
         * Sets whether or not the Travel Agent will be used.
         * <p>
         * If this is set to true, the TravelAgent will try to find a Portal at
         * the {@link #getTo()} Location, and will try to create one if there is
         * none.
         * <p>
         * If this is set to false, the {@link #getPlayer()} will only be
         * teleported to the {@link #getTo()} Location.
         *
         * @param useTravelAgent whether to use the Travel Agent
         */
        public bool useTravelAgent { get; set; } = true;

        /**
         * Gets the Travel Agent used (or not) in this event.
         *
         * @return the Travel Agent used (or not) in this event
         */
        public TravelAgent getPortalTravelAgent()
        {
            return this.travelAgent;
        }

        /**
         * Sets the Travel Agent used (or not) in this event.
         *
         * @param travelAgent the Travel Agent used (or not) in this event
         */
        public void setPortalTravelAgent(TravelAgent travelAgent)
        {
            this.travelAgent = travelAgent;
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
