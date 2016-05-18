namespace Mine.NET.Event.block
{
    /**
     * Called when we try to place a block, to see if we can build it here or not.
     * <p>
     * Note:
     * <ul>
     * <li>The Block returned by getBlock() is the block we are trying to place
     *     on, not the block we are trying to place.
     * <li>If you want to figure out what is being placed, use {@link
     *     #getMaterial()} or {@link #getMaterialId()} instead.
     * </ul>
     */
    public class BlockCanBuildEvent : BlockEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        protected bool buildable;
        private Materials Materials;

        /**
         * Gets whether or not the block can be built here.
         * <p>
         * By default, returns Minecraft's answer on whether the block can be
         * built here or not.
         *
         * @return bool whether or not the block can be built
         */
        public bool isBuildable()
        {
            return buildable;
        }

        /**
         * Sets whether the block can be built here or not.
         *
         * @param cancel true if you want to allow the block to be built here
         *     despite Minecraft's default behaviour
         */
        public void setBuildable(bool cancel)
        {
            this.buildable = cancel;
        }

        /**
         * Gets the Materials that we are trying to place.
         *
         * @return The Materials that we are trying to place
         */
        public Materials getMaterial()
        {
            return Materials;
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
