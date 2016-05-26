using Mine.NET.block;

namespace Mine.NET.Event.block
{

    /**
     * Thrown when a block physics check is called
     */
    public class BlockPhysicsEventArgs : BlockEventArgs, Cancellable
    {
        private readonly Materials changed;
        private bool cancel = false;

        public BlockPhysicsEventArgs(Block block, Materials changed) : base(block)
        {
            this.changed = changed;
        }

        /**
         * Gets the type of block that changed, causing this event
         *
         * @return Changed block's type
         */
        public Materials getChangedType()
        {
            return changed;
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
