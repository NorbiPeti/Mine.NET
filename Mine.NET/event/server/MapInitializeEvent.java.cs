using Mine.NET.map;

namespace Mine.NET.Event.server
{
    /**
     * Called when a map is initialized.
     */
    public class MapInitializeEvent : ServerEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly MapView mapView;

        public MapInitializeEvent(MapView mapView)
        {
            this.mapView = mapView;
        }

        /**
         * Gets the map initialized in this event.
         *
         * @return Map for this event
         */
        public MapView getMap()
        {
            return mapView;
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
