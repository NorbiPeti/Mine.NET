using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a sheep's wool is dyed
     */
    public class SheepDyeWoolEventArgs : EntityEventArgs<Sheep>, Cancellable
    { //TODO: Convert to .NET events
        private bool cancel;
        private DyeColor color;

        public SheepDyeWoolEventArgs(Sheep sheep, DyeColor color) : base(sheep)
        {
            this.cancel = false;
            this.color = color;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Gets the DyeColor the sheep is being dyed
         *
         * @return the DyeColor the sheep is being dyed
         */
        public DyeColor getColor()
        {
            return color;
        }

        /**
         * Sets the DyeColor the sheep is being dyed
         *
         * @param color the DyeColor the sheep will be dyed
         */
        public void setColor(DyeColor color)
        {
            this.color = color;
        }
    }
}
