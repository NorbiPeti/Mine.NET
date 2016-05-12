using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is broken by a player.
     * <p>
     * If you wish to have the block drop experience, you must set the experience
     * value above 0. By default, experience will be set in the event if:
     * <ol>
     * <li>The player is not in creative or adventure mode
     * <li>The player can loot the block (ie: does not destroy it completely, by
     *     using the correct tool)
     * <li>The player does not have silk touch
     * <li>The block drops experience in vanilla Minecraft
     * </ol>
     * <p>
     * Note:
     * Plugins wanting to simulate a traditional block drop should set the block
     * to air and utilize their own methods for determining what the default drop
     * for the block being broken is and what to do about it, if anything.
     * <p>
     * If a Block Break event is cancelled, the block will not break and
     * experience will not drop.
     */
    public class BlockBreakEvent : BlockExpEvent, Cancellable {
        private readonly Player player;
        private bool cancel;

        public BlockBreakEvent(Block theBlock, Player player) : base(theBlock, 0)
        {
            this.player = player;
        }

        /**
         * Gets the Player that is breaking the block involved in this event.
         *
         * @return The Player that is breaking the block involved in this event
         */
        public Player getPlayer() {
            return player;
        }

        public bool isCancelled() {
            return cancel;
        }

        public void setCancelled(bool cancel) {
            this.cancel = cancel;
        }
    }
}
