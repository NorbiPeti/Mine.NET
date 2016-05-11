package org.bukkit.event.hanging;

import org.bukkit.block.Block;
import org.bukkit.block.BlockFace;
import org.bukkit.entity.Hanging;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Triggered when a hanging entity is created in the world
 */
public class HangingPlaceEvent : HangingEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private readonly Player player;
    private readonly Block block;
    private readonly BlockFace blockFace;

    public HangingPlaceEvent(Hanging hanging, readonly Player player, readonly Block block, readonly BlockFace blockFace) {
        super(hanging);
        this.player = player;
        this.block = block;
        this.blockFace = blockFace;
    }

    /**
     * Returns the player placing the hanging entity
     *
     * @return the player placing the hanging entity
     */
    public Player getPlayer() {
        return player;
    }

    /**
     * Returns the block that the hanging entity was placed on
     *
     * @return the block that the hanging entity was placed on
     */
    public Block getBlock() {
        return block;
    }

    /**
     * Returns the face of the block that the hanging entity was placed on
     *
     * @return the face of the block that the hanging entity was placed on
     */
    public BlockFace getBlockFace() {
        return blockFace;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
