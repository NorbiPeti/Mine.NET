using Mine.NET.block;
using Mine.NET.entity;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity explodes
     */
    public class EntityExplodeEventArgs : EntityEventArgs<Explosive>, Cancellable
    {
        private bool cancel;
        private readonly Location location;
        private readonly List<Block> blocks;
        private float yield;

        public EntityExplodeEventArgs(Entity what, Location location, List<Block> blocks, float yield) :
            base(what)
        {
            this.location = location;
            this.blocks = blocks;
            this.yield = yield;
            this.cancel = false;
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
         * Returns the list of blocks that would have been removed or were removed
         * from the explosion event.
         *
         * @return All blown-up blocks
         */
        public List<Block> blockList()
        {
            return blocks;
        }

        /**
         * Returns the location where the explosion happened.
         * <p>
         * It is not possible to get this value from the Entity as the Entity no
         * longer exists in the world.
         *
         * @return The location of the explosion
         */
        public Location getLocation()
        {
            return location;
        }

        /**
         * Returns the percentage of blocks to drop from this explosion
         *
         * @return The yield.
         */
        public float getYield()
        {
            return yield;
        }

        /**
         * Sets the percentage of blocks to drop from this explosion
         *
         * @param yield The new yield percentage
         */
        public void setYield(float yield)
        {
            this.yield = yield;
        }
    }
}
