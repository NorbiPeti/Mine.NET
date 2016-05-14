using System.Collections.Generic;

namespace Mine.NET.map
{
/**
 * Represents a map item.
 */
public interface MapView {
    /**
     * Check whether this map is virtual. A map is virtual if its lowermost
     * MapRenderer is plugin-provided.
     *
     * @return Whether the map is virtual.
     */
    bool isVirtual();

    /**
     * Get the scale of this map.
     *
     * @return The scale of the map.
     */
    MapViewScale getScale();

    /**
     * Set the scale of this map.
     *
     * @param scale The scale to set.
     */
    void setScale(MapViewScale scale);

    /**
     * Get the center X position of this map.
     *
     * @return The center X position.
     */
    int getCenterX();

    /**
     * Get the center Z position of this map.
     *
     * @return The center Z position.
     */
    int getCenterZ();

    /**
     * Set the center X position of this map.
     *
     * @param x The center X position.
     */
    void setCenterX(int x);

    /**
     * Set the center Z position of this map.
     *
     * @param z The center Z position.
     */
    void setCenterZ(int z);

    /**
     * Get the world that this map is associated with. Primarily used by the
     * internal renderer, but may be used by external renderers. May return
     * null if the world the map is associated with is not loaded.
     *
     * @return The World this map is associated with.
     */
    World getWorld();

    /**
     * Set the world that this map is associated with. The world is used by
     * the internal renderer, and may also be used by external renderers.
     *
     * @param world The World to associate this map with.
     */
    void setWorld(World world);

    /**
     * Get a list of MapRenderers currently in effect.
     *
     * @return A {@code List<MapRenderer>} containing each map renderer.
     */
    List<MapRenderer> getRenderers();

    /**
     * Add a renderer to this map.
     *
     * @param renderer The MapRenderer to add.
     */
    void addRenderer(MapRenderer renderer);

    /**
     * Remove a renderer from this map.
     *
     * @param renderer The MapRenderer to remove.
     * @return True if the renderer was successfully removed.
     */
    bool removeRenderer(MapRenderer renderer);

    }

    /**
     * An enum representing all possible scales a map can be set to.
     */
    public enum MapViewScale
    {
        CLOSEST = 0,
        CLOSE = 1,
        NORMAL = 2,
        FAR = 3,
        FARTHEST = 4
    }
}
