using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player empties a bucket
     */
    public class PlayerBucketEmptyEvent : PlayerBucketEvent
    {
        private static readonly HandlerList handlers = new HandlerList();

        public PlayerBucketEmptyEvent(Player who, Block blockClicked, BlockFaces BlockFaces, Material bucket, ItemStack itemInHand) :
            base(who, blockClicked, BlockFaces, bucket, itemInHand)
        {
        }

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
