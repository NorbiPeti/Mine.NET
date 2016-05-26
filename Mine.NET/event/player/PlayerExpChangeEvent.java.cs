using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a players experience changes naturally
     */
    public class PlayerExpChangeEventArgs : PlayerEventArgs
    {
        private int exp;

        public PlayerExpChangeEventArgs(Player player, int expAmount) : base(player)
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
    }
}
