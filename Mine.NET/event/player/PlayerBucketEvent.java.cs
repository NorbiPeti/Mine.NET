package org.bukkit.event.player;

import org.bukkit.Material;
import org.bukkit.block.Block;
import org.bukkit.block.BlockFace;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.inventory.ItemStack;

/**
 * Called when a player interacts with a Bucket
 */
public abstract class PlayerBucketEvent : PlayerEvent : Cancellable {
    private ItemStack itemStack;
    private bool cancelled = false;
    private readonly Block blockClicked;
    private readonly BlockFace blockFace;
    private readonly Material bucket;

    public PlayerBucketEvent(Player who, readonly Block blockClicked, readonly BlockFace blockFace, readonly Material bucket, readonly ItemStack itemInHand) {
        super(who);
        this.blockClicked = blockClicked;
        this.blockFace = blockFace;
        this.itemStack = itemInHand;
        this.bucket = bucket;
    }

    /**
     * Returns the bucket used in this event
     *
     * @return the used bucket
     */
    public Material getBucket() {
        return bucket;
    }

    /**
     * Get the resulting item in hand after the bucket event
     *
     * @return Itemstack hold in hand after the event.
     */
    public ItemStack getItemStack() {
        return itemStack;
    }

    /**
     * Set the item in hand after the event
     *
     * @param itemStack the new held itemstack after the bucket event.
     */
    public void setItemStack(ItemStack itemStack) {
        this.itemStack = itemStack;
    }

    /**
     * Return the block clicked
     *
     * @return the blicked block
     */
    public Block getBlockClicked() {
        return blockClicked;
    }

    /**
     * Get the face on the clicked block
     *
     * @return the clicked face
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
}