package org.bukkit.event.player;

import org.bukkit.block.Block;
import org.bukkit.block.BlockFace;
import org.bukkit.event.HandlerList;
import org.bukkit.inventory.ItemStack;
import org.bukkit.Material;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.block.Action;
import org.bukkit.inventory.EquipmentSlot;

/**
 * Called when a player interacts with an object or air.
 * <p>
 * This event will fire as cancelled if the vanilla behavior
 * is to do nothing (e.g interacting with air)
 */
public class PlayerInteractEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    protected ItemStack item;
    protected Action action;
    protected Block blockClicked;
    protected BlockFace blockFace;
    private Result useClickedBlock;
    private Result useItemInHand;
    private EquipmentSlot hand;

    public PlayerInteractEvent(Player who, readonly Action action, readonly ItemStack item, readonly Block clickedBlock, readonly BlockFace clickedFace) {
        this(who, action, item, clickedBlock, clickedFace, EquipmentSlot.HAND);
    }

    public PlayerInteractEvent(Player who, readonly Action action, readonly ItemStack item, readonly Block clickedBlock, readonly BlockFace clickedFace, readonly EquipmentSlot hand) {
        base(who);
        this.action = action;
        this.item = item;
        this.blockClicked = clickedBlock;
        this.blockFace = clickedFace;
        this.hand = hand;

        useItemInHand = Result.DEFAULT;
        useClickedBlock = clickedBlock == null ? Result.DENY : Result.ALLOW;
    }

    /**
     * Returns the action type
     *
     * @return Action returns the type of interaction
     */
    public Action getAction() {
        return action;
    }

    /**
     * Gets the cancellation state of this event. Set to true if you want to
     * prevent buckets from placing water and so forth
     *
     * @return bool cancellation state
     */
    public bool isCancelled() {
        return useInteractedBlock() == Result.DENY;
    }

    /**
     * Sets the cancellation state of this event. A canceled event will not be
     * executed in the server, but will still pass to other plugins
     * <p>
     * Canceling this event will prevent use of food (player won't lose the
     * food item), prevent bows/snowballs/eggs from firing, etc. (player won't
     * lose the ammo)
     *
     * @param cancel true if you wish to cancel this event
     */
    public void setCancelled(bool cancel) {
        setUseInteractedBlock(cancel ? Result.DENY : useInteractedBlock() == Result.DENY ? Result.DEFAULT : useInteractedBlock());
        setUseItemInHand(cancel ? Result.DENY : useItemInHand() == Result.DENY ? Result.DEFAULT : useItemInHand());
    }

    /**
     * Returns the item in hand represented by this event
     *
     * @return ItemStack the item used
     */
    public ItemStack getItem() {
        return this.item;
    }

    /**
     * Convenience method. Returns the material of the item represented by
     * this event
     *
     * @return Material the material of the item used
     */
    public Material getMaterial() {
        if (!hasItem()) {
            return Material.AIR;
        }

        return item.getType();
    }

    /**
     * Check if this event involved a block
     *
     * @return bool true if it did
     */
    public bool hasBlock() {
        return this.blockClicked != null;
    }

    /**
     * Check if this event involved an item
     *
     * @return bool true if it did
     */
    public bool hasItem() {
        return this.item != null;
    }

    /**
     * Convenience method to inform the user whether this was a block
     * placement event.
     *
     * @return bool true if the item in hand was a block
     */
    public bool isBlockInHand() {
        if (!hasItem()) {
            return false;
        }

        return item.getType().isBlock();
    }

    /**
     * Returns the clicked block
     *
     * @return Block returns the block clicked with this item.
     */
    public Block getClickedBlock() {
        return blockClicked;
    }

    /**
     * Returns the face of the block that was clicked
     *
     * @return BlockFace returns the face of the block that was clicked
     */
    public BlockFace getBlockFace() {
        return blockFace;
    }

    /**
     * This controls the action to take with the block (if any) that was
     * clicked on. This event gets processed for all blocks, but most don't
     * have a default action
     *
     * @return the action to take with the interacted block
     */
    public Result useInteractedBlock() {
        return useClickedBlock;
    }

    /**
     * @param useInteractedBlock the action to take with the interacted block
     */
    public void setUseInteractedBlock(Result useInteractedBlock) {
        this.useClickedBlock = useInteractedBlock;
    }

    /**
     * This controls the action to take with the item the player is holding.
     * This includes both blocks and items (such as flint and steel or
     * records). When this is set to default, it will be allowed if no action
     * is taken on the interacted block.
     *
     * @return the action to take with the item in hand
     */
    public Result useItemInHand() {
        return useItemInHand;
    }

    /**
     * @param useItemInHand the action to take with the item in hand
     */
    public void setUseItemInHand(Result useItemInHand) {
        this.useItemInHand = useItemInHand;
    }

    /**
     * The hand used to perform this interaction. May be null in the case of
     * {@link Action#PHYSICAL}.
     *
     * @return the hand used to interact. May be null.
     */
    public EquipmentSlot getHand() {
        return hand;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
