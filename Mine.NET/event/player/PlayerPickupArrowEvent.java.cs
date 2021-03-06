using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Thrown when a player picks up an arrow from the ground.
     */
    public class PlayerPickupArrowEventArgs : PlayerPickupItemEventArgs
    {

        private readonly Arrow arrow;

        public PlayerPickupArrowEventArgs(Player player, Item item, Arrow arrow) : base(player, item, 0)
        {
            this.arrow = arrow;
        }

        /**
         * Get the arrow being picked up by the player
         *
         * @return The arrow being picked up
         */
        public Arrow getArrow()
        {
            return arrow;
        }
    }
}
