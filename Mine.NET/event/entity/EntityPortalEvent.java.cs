using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a non-player entity is about to teleport because it is in
     * contact with a portal.
     * <p>
     * For players see {@link org.bukkit.event.player.PlayerPortalEvent}
     */
    public class EntityPortalEventArgs : EntityTeleportEventArgs
    {
        protected TravelAgent travelAgent;

        public EntityPortalEventArgs(Entity entity, Location from, Location to, TravelAgent pta) : base(entity, from, to)
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
         * If this is set to false, the {@link #getEntity()} will only be
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
    }
}
