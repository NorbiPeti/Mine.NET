package org.bukkit.event.block;

import org.bukkit.block.Block;
import org.bukkit.Material;
import org.bukkit.event.HandlerList;

/**
 * Called when we try to place a block, to see if we can build it here or not.
 * <p>
 * Note:
 * <ul>
 * <li>The Block returned by getBlock() is the block we are trying to place
 *     on, not the block we are trying to place.
 * <li>If you want to figure out what is being placed, use {@link
 *     #getMaterial()} or {@link #getMaterialId()} instead.
 * </ul>
 */
public class BlockCanBuildEvent : BlockEvent {
    private static readonly HandlerList handlers = new HandlerList();
    protected bool buildable;

    /**
     *
     * [Obsolete] Magic value
     */
    [Obsolete]
    protected int material;

    /**
     *
     * [Obsolete] Magic value
     * @param block the block involved in this event
     * @param id the id of the block to place
     * @param canBuild whether we can build 
     */
    [Obsolete]
    public BlockCanBuildEvent(Block block, readonly int id, readonly bool canBuild) {
        super(block);
        buildable = canBuild;
        material = id;
    }

    /**
     * Gets whether or not the block can be built here.
     * <p>
     * By default, returns Minecraft's answer on whether the block can be
     * built here or not.
     *
     * @return bool whether or not the block can be built
     */
    public bool isBuildable() {
        return buildable;
    }

    /**
     * Sets whether the block can be built here or not.
     *
     * @param cancel true if you want to allow the block to be built here
     *     despite Minecraft's default behaviour
     */
    public void setBuildable(bool cancel) {
        this.buildable = cancel;
    }

    /**
     * Gets the Material that we are trying to place.
     *
     * @return The Material that we are trying to place
     */
    public Material getMaterial() {
        return Material.getMaterial(material);
    }

    /**
     * Gets the Material ID for the Material that we are trying to place.
     *
     * @return The Material ID for the Material that we are trying to place
     * [Obsolete] Magic value
     */
    [Obsolete]
    public int getMaterialId() {
        return material;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
