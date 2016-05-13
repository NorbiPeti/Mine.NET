using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a players experience changes naturally
     */
    public class PlayerExpChangeEvent : PlayerEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private int exp;

        public PlayerExpChangeEvent(Player player, int expAmount) : base(player)
        {
            exp = expAmount;
        }

        /**
         * Get the amount of experience the player will receive
         *
         * @return The amount of experience
         */
        public int getAmount()
        {
            return exp;
        }

        /**
         * Set the amount of experience the player will receive
         *
         * @param amount The amount of experience to set
         */
        public void setAmount(int amount)
        {
            exp = amount;
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
