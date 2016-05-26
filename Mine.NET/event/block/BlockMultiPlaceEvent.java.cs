using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.inventory;
using System.Collections.Generic;

namespace Mine.NET.Event.block
{
    /**
     * Fired when a single block placement action of a player triggers the
     * creation of multiple blocks(e.g. placing a bed block). The block returned
     * by {@link #getBlockPlaced()} and its related methods is the block where
     * the placed block would exist if the placement only affected a single
     * block.
     */
    public class BlockMultiPlaceEventArgs : BlockPlaceEventArgs
    {
        private readonly List<BlockState> states;

        public BlockMultiPlaceEventArgs(List<BlockState> states, Block clicked, ItemStack itemInHand, Player thePlayer, bool canBuild, EquipmentSlot hand) : base(states[0].getBlock(), states[0], clicked, itemInHand, thePlayer, canBuild, hand)
        {
            this.states = new List<BlockState>(states);
        }

        /**
         * Gets a list of blockstates for all blocks which were replaced by the
         * placement of the new blocks. Most of these blocks will just have a
         * Materials type of AIR.
         *
         * @return immutable list of replaced BlockStates
         */
        public List<BlockState> getReplacedBlockStates()
        {
            return states;
        }
    }
}
