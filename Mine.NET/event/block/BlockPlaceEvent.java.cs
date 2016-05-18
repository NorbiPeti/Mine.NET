using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is placed by a player.
     * <p>
     * If a Block Place event is cancelled, the block will not be placed.
     */
    public class BlockPlaceEvent : BlockEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        protected bool cancel;
        protected Block placedAgainst;
        protected BlockState replacedBlockState;
        protected ItemStack itemInHand;
        protected Player player;
        protected EquipmentSlot hand;

        public BlockPlaceEvent(Block placedBlock, BlockState replacedBlockState, Block placedAgainst, ItemStack itemInHand, Player thePlayer, bool canBuild, EquipmentSlot hand) : base(placedBlock)
        {
            this.placedAgainst = placedAgainst;
            this.itemInHand = itemInHand;
            this.player = thePlayer;
            this.replacedBlockState = replacedBlockState;
            this.canBuild = canBuild;
            this.hand = hand;
            cancel = false;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Gets the player who placed the block involved in this event.
         *
         * @return The Player who placed the block involved in this event
         */
        public Player getPlayer()
        {
            return player;
        }

        /**
         * Clarity method for getting the placed block. Not really needed except
         * for reasons of clarity.
         *
         * @return The Block that was placed
         */
        public Block getBlockPlaced()
        {
            return getBlock();
        }

        /**
         * Gets the BlockState for the block which was replaced. Materials type air
         * mostly.
         *
         * @return The BlockState for the block which was replaced.
         */
        public BlockState getBlockReplacedState()
        {
            return this.replacedBlockState;
        }

        /**
         * Gets the block that this block was placed against
         *
         * @return Block the block that the new block was placed against
         */
        public Block getBlockAgainst()
        {
            return placedAgainst;
        }

        /**
         * Gets the item in the player's hand when they placed the block.
         *
         * @return The ItemStack for the item in the player's hand when they
         *     placed the block
         */
        public ItemStack getItemInHand()
        {
            return itemInHand;
        }

        /**
         * Gets the hand which placed the block
         * @return Main or off-hand, depending on which hand was used to place the block
         */
        public EquipmentSlot getHand()
        {
            return this.hand;
        }

        /**
         * Gets the value whether the player would be allowed to build here.
         * Defaults to spawn if the server was going to stop them (such as, the
         * player is in Spawn). Note that this is an entirely different check
         * than BLOCK_CANBUILD, as this refers to a player, not universe-physics
         * rule like cactus on dirt.
         *
         * @return bool whether the server would allow a player to build here
         */
        public bool canBuild { get; set; }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
