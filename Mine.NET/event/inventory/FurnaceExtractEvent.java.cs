using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.Event.block;

namespace Mine.NET.Event.inventory
{
    /**
     * This event is called when a player takes items out of the furnace
     */
    public class FurnaceExtractEventArgs : BlockExpEventArgs
    {
        private readonly Player player;
        private readonly Materials itemType;
        private readonly int itemAmount;

        public FurnaceExtractEventArgs(Player player, Block block, Materials itemType, int itemAmount, int exp) :
            base(block, exp)
        {
            this.player = player;
            this.itemType = itemType;
            this.itemAmount = itemAmount;
        }

        /**
         * Get the player that triggered the event
         *
         * @return the relevant player
         */
        public Player getPlayer()
        {
            return player;
        }

        /**
         * Get the Materials of the item being retrieved
         *
         * @return the Materials of the item
         */
        public Materials getItemType()
        {
            return itemType;
        }

        /**
         * Get the item count being retrieved
         *
         * @return the amount of the item
         */
        public int getItemAmount()
        {
            return itemAmount;
        }
    }
}
