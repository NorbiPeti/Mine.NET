using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when leaves are decaying naturally.
     * <p>
     * If a Leaves Decay event is cancelled, the leaves will not decay.
     */
    public class LeavesDecayEventArgs : BlockEventArgs, Cancellable
    {
        private bool cancel = false;

        public LeavesDecayEventArgs(Block block) : base(block)
        {
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }
    }
}
