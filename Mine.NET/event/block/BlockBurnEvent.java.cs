using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is destroyed as a result of being burnt by fire.
     * <p>
     * If a Block Burn event is cancelled, the block will not be destroyed as a
     * result of being burnt by fire.
     */
    public class BlockBurnEvent : BlockEvent, Cancellable {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;

        public BlockBurnEvent(Block block) : base(block)
        {
            this.cancelled = false;
        }

        public bool isCancelled() {
            return cancelled;
        }

        public void setCancelled(bool cancel) {
            this.cancelled = cancel;
        }

        public override HandlerList getHandlers() {
            return handlers;
        }
            
        public static HandlerList getHandlerList() {
            return handlers;
        }
    }
}
