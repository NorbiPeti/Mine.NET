package org.bukkit.event.player;

import org.bukkit.Material;
import org.bukkit.block.Block;
import org.bukkit.block.BlockFace;
import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;
import org.bukkit.inventory.ItemStack;

/**
 * Called when a player empties a bucket
 */
public class PlayerBucketEmptyEvent : PlayerBucketEvent {
    private static readonly HandlerList handlers = new HandlerList();

    public PlayerBucketEmptyEvent(Player who, readonly Block blockClicked, readonly BlockFace blockFace, readonly Material bucket, readonly ItemStack itemInHand) {
        base(who, blockClicked, blockFace, bucket, itemInHand);
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
