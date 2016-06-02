using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is destroyed as a result of being burnt by fire.
     * <p>
     * If a Block Burn event is cancelled, the block will not be destroyed as a
     * result of being burnt by fire.
     */
    public class BlockBurnEventArgs : BlockEventArgs, Cancellable {
        private bool cancelled;

        public BlockBurnEventArgs(Block block) : base(block)
        {
            this.cancelled = false;
        }

        public bool isCancelled() {
            return cancelled;
        }

        public void setCancelled(bool cancel) {
            this.cancelled = cancel;
        }
    }
}
