using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player fills a bucket
     */
    public class PlayerBucketFillEventArgs : PlayerBucketEventArgs
    {
        public PlayerBucketFillEventArgs(Player who, Block blockClicked, BlockFaces BlockFaces, Materials bucket, ItemStack itemInHand) :
            base(who, blockClicked, BlockFaces, bucket, itemInHand)
        {
        }
    }
}
