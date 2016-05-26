using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player interacts with a Bucket
     */
    public abstract class PlayerBucketEventArgs : PlayerEventArgs, Cancellable
    {
        private ItemStack itemStack;
        private bool cancelled = false;
        private readonly Block blockClicked;
        private readonly BlockFaces BlockFaces;
        private readonly Materials bucket;

        public PlayerBucketEventArgs(Player who, Block blockClicked, BlockFaces BlockFaces, Materials bucket, ItemStack itemInHand) :
            base(who)
        {
            this.blockClicked = blockClicked;
            this.BlockFaces = BlockFaces;
            this.itemStack = itemInHand;
            this.bucket = bucket;
        }

        /**
         * Returns the bucket used in this event
         *
         * @return the used bucket
         */
        public Materials getBucket()
        {
            return bucket;
        }

        /**
         * Get the resulting item in hand after the bucket event
         *
         * @return Itemstack hold in hand after the event.
         */
        public ItemStack getItemStack()
        {
            return itemStack;
        }

        /**
         * Set the item in hand after the event
         *
         * @param itemStack the new held itemstack after the bucket event.
         */
        public void setItemStack(ItemStack itemStack)
        {
            this.itemStack = itemStack;
        }

        /**
         * Return the block clicked
         *
         * @return the blicked block
         */
        public Block getBlockClicked()
        {
            return blockClicked;
        }

        /**
         * Get the face on the clicked block
         *
         * @return the clicked face
         */
        public BlockFaces getBlockFace()
        {
            return BlockFaces;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }
    }
}
