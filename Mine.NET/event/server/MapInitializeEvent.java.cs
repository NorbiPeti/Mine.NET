using Mine.NET.map;

namespace Mine.NET.Event.server
{
    /**
     * Called when a map is initialized.
     */
    public class MapInitializeEventArgs : ServerEventArgs
    {
        private readonly MapView mapView;

        public MapInitializeEventArgs(MapView mapView)
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
    }
}
