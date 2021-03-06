using Mine.NET.block;
using Mine.NET.entity;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown when a Living Entity creates a portal in a world.
     */
    public class EntityCreatePortalEventArgs : EntityEventArgs<LivingEntity>, Cancellable
    {
        private readonly List<BlockState> blocks;
        private bool cancelled = false;
        private PortalType type = PortalType.CUSTOM;

        public EntityCreatePortalEventArgs(LivingEntity what, List<BlockState> blocks, PortalType type) : base(what)
        {
            this.blocks = blocks;
            this.type = type;
        }

        /**
         * Gets a list of all blocks associated with the portal.
         *
         * @return List of blocks that will be changed.
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
            this.cancelled = cancel;
        }

        /**
         * Gets the type of portal that is trying to be created.
         *
         * @return Type of portal.
         */
        public PortalType getPortalType()
        {
            return type;
        }
    }
}
