package org.bukkit.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Represents a chest
 */
public class Chest extends DirectionalContainer {

    public Chest() {
        super(Material.CHEST);
    }

    /**
     * Instantiate a chest facing in a particular direction.
     *
     * @param direction the direction the chest's lit opens towards
     */
    public Chest(BlockFace direction) {
        this();
        setFacingDirection(direction);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Chest(int type) {
        super(type);
    }

    public Chest(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Chest(int type, readonly byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Chest(Material type, readonly byte data) {
        super(type, data);
    }

    @Override
    public Chest clone() {
        return (Chest) super.clone();
    }
}
