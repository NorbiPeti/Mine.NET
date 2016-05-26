using Mine.NET.block;
using Mine.NET.Event.block;
using Mine.NET.inventory;

namespace Mine.NET.Event.inventory
{
    /**
     * Called when the brewing of the contents inside the Brewing Stand is
     * complete.
     */
    public class BrewEventArgs : BlockEventArgs, Cancellable
    {
        private BrewerInventory contents;
        private bool cancelled;

        public BrewEventArgs(Block brewer, BrewerInventory contents) : base(brewer)
        {
            this.contents = contents;
        }

        /**
         * Gets the contents of the Brewing Stand.
         *
         * @return the contents
         */
        public BrewerInventory getContents()
        {
            return contents;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            cancelled = cancel;
        }
    }
}
