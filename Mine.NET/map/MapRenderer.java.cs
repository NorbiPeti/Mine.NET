using Mine.NET.entity;

namespace Mine.NET.map
{
    /**
     * Represents a renderer for a map.
     */
    public abstract class MapRenderer
    {

        private bool contextual;

        /**
         * Initialize the map renderer base to be non-contextual. See {@link
         * #isContextual()}.
         */
        public MapRenderer() : this(false)
        {
        }

        /**
         * Initialize the map renderer base with the given contextual status.
         *
         * @param contextual Whether the renderer is contextual. See {@link
         *     #isContextual()}.
         */
        public MapRenderer(bool contextual)
        {
            this.contextual = contextual;
        }

        /**
         * Get whether the renderer is contextual, i.e. has different canvases for
         * different players.
         *
         * @return True if contextual, false otherwise.
         */
        public bool isContextual()
        {
            return contextual;
        }

        /**
         * Initialize this MapRenderer for the given map.
         *
         * @param map The MapView being initialized.
         */
        public void initialize(MapView map) { }

        /**
         * Render to the given map.
         *
         * @param map The MapView being rendered to.
         * @param canvas The canvas to use for rendering.
         * @param player The player who triggered the rendering.
         */
        abstract public void render(MapView map, MapCanvas canvas, Player player);

    }
}
