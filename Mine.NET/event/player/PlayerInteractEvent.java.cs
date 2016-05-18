using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.Event.block;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
/**
 * Called when a player interacts with an object or air.
 * <p>
 * This event will fire as cancelled if the vanilla behavior
 * is to do nothing (e.g interacting with air)
 */
public class PlayerInteractEvent : PlayerEvent, Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    protected ItemStack item;
    protected Action action;
    protected Block blockClicked;
    protected BlockFaces BlockFaces;
    private Result useClickedBlock;
    private EquipmentSlot hand;

    public PlayerInteractEvent(Player who, Action action, ItemStack item, Block clickedBlock, BlockFaces clickedFace) :
        this(who, action, item, clickedBlock, clickedFace, EquipmentSlot.HAND)
        {
    }

    public PlayerInteractEvent(Player who, Action action, ItemStack item, Block clickedBlock, BlockFaces clickedFace, EquipmentSlot hand) :
        base(who)
        {
        this.action = action;
        this.item = item;
        this.blockClicked = clickedBlock;
        this.BlockFaces = clickedFace;
        this.hand = hand;

        UseItemInHand = Result.DEFAULT;
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
            UseItemInHand = cancel ? Result.DENY : UseItemInHand == Result.DENY ? Result.DEFAULT : UseItemInHand;
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
    public Materials getMaterial() {
        if (!hasItem()) {
            return Materials.AIR;
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
     * @return BlockFaces returns the face of the block that was clicked
     */
    public BlockFaces getBlockFace() {
        return BlockFaces;
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
    public Result UseItemInHand { get; set; }

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
}
