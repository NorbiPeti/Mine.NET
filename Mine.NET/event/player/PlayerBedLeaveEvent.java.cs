using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * This event is fired when the player is leaving a bed.
     */
    public class PlayerBedLeaveEventArgs : PlayerEventArgs
    {
        private readonly Block bed;

        public PlayerBedLeaveEventArgs(Player who, Block bed) : base(who)
        {
            this.bed = bed;
        }

        /**
         * Returns the bed block involved in this event.
         *
         * @return the bed block involved in this event
         */
        public Block getBed()
        {
            return bed;
        }
    }
}
