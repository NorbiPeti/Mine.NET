using Mine.NET.block;
using Mine.NET.entity;
using System.Collections.Generic;

namespace Mine.NET.Event.world
{
    /**
     * Event that is called when an organic structure attempts to grow (Sapling {@literal ->}
     * Tree), (Mushroom {@literal ->} Huge Mushroom), naturally or using bonemeal.
     */
    public class StructureGrowEventArgs : WorldEventArgs, Cancellable
    {
        private bool cancelled = false;
        private readonly Location location;
        private readonly TreeType species;
        private readonly bool bonemeal;
        private readonly Player player;
        private readonly List<BlockState> blocks;

        public StructureGrowEventArgs(Location location, TreeType species, bool bonemeal, Player player, List<BlockState> blocks) :
            base(location.getWorld())
        {
            this.location = location;
            this.species = species;
            this.bonemeal = bonemeal;
            this.player = player;
            this.blocks = blocks;
        }

        /**
         * Gets the location of the structure.
         *
         * @return Location of the structure
         */
        public Location getLocation()
        {
            return location;
        }

        /**
         * Gets the species type (birch, normal, pine, red mushroom, brown
         * mushroom)
         *
         * @return Structure species
         */
        public TreeType getSpecies()
        {
            return species;
        }

        /**
         * Checks if structure was grown using bonemeal.
         *
         * @return True if the structure was grown using bonemeal.
         */
        public bool isFromBonemeal()
        {
            return bonemeal;
        }

        /**
         * Gets the player that created the structure.
         *
         * @return Player that created the structure, null if was not created
         *     manually
         */
        public Player getPlayer()
        {
            return player;
        }

        /**
         * Gets an List of all blocks associated with the structure.
         *
         * @return List of all blocks associated with the structure.
         */
        public List<BlockState> getBlocks()
        {
            return blocks;
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
