using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player empties a bucket
     */
    public class PlayerBucketEmptyEventArgs : PlayerBucketEventArgs
    {
        public PlayerBucketEmptyEventArgs(Player who, Block blockClicked, BlockFaces BlockFaces, Materials bucket, ItemStack itemInHand) :
            base(who, blockClicked, BlockFaces, bucket, itemInHand)
        {
        }
    }
}
