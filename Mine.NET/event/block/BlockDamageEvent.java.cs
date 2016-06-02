using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is damaged by a player.
     * <p>
     * If a Block Damage event is cancelled, the block will not be damaged.
     */
    public class BlockDamageEventArgs : BlockEventArgs, Cancellable {
        private readonly Player player;
        private bool instaBreak;
        private bool cancel;
        private readonly ItemStack itemstack;

        public BlockDamageEventArgs(Player player, Block block, ItemStack itemInHand, bool instaBreak) : base(block)
        {
            this.instaBreak = instaBreak;
            this.player = player;
            this.itemstack = itemInHand;
            this.cancel = false;
        }

        /**
         * Gets the player damaging the block involved in this event.
         *
         * @return The player damaging the block involved in this event
         */
        public Player getPlayer() {
            return player;
        }

        /**
         * Gets if the block is set to instantly break when damaged by the player.
         *
         * @return true if the block should instantly break when damaged by the
         *     player
         */
        public bool getInstaBreak() {
            return instaBreak;
        }

        /**
         * Sets if the block should instantly break when damaged by the player.
         *
         * @param bool true if you want the block to instantly break when damaged
         *     by the player
         */
        public void setInstaBreak(bool bool_) {
            this.instaBreak = bool_;
        }

        /**
         * Gets the ItemStack for the item currently in the player's hand.
         *
         * @return The ItemStack for the item currently in the player's hand
         */
        public ItemStack getItemInHand() {
            return itemstack;
        }

        public bool isCancelled() {
            return cancel;
        }

        public void setCancelled(bool cancel) {
            this.cancel = cancel;
        }
    }
}
