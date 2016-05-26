using Mine.NET.block;

namespace Mine.NET.Event.block
{
    /**
     * Called when a redstone current changes
     */
    public class BlockRedstoneEventArgs : BlockEventArgs
    {
        private readonly int oldCurrent;
        private int newCurrent;

        public BlockRedstoneEventArgs(Block block, int oldCurrent, int newCurrent) : base(block)
        {
            this.oldCurrent = oldCurrent;
            this.newCurrent = newCurrent;
        }

        /**
         * Gets the old current of this block
         *
         * @return The previous current
         */
        public int getOldCurrent()
        {
            return oldCurrent;
        }

        /**
         * Gets the new current of this block
         *
         * @return The new current
         */
        public int getNewCurrent()
        {
            return newCurrent;
        }

        /**
         * Sets the new current of this block
         *
         * @param newCurrent The new current to set
         */
        public void setNewCurrent(int newCurrent)
        {
            this.newCurrent = newCurrent;
        }
    }
}
