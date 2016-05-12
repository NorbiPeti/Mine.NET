using Mine.NET.block;
using Mine.NET.entity;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown when a Living Entity creates a portal in a world.
     */
    public class EntityCreatePortalEvent : EntityEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly List<BlockState> blocks;
        private bool cancelled = false;
        private PortalType type = PortalType.CUSTOM;

        public EntityCreatePortalEvent(LivingEntity what, List<BlockState> blocks, PortalType type) : base(what)
        {
            this.blocks = blocks;
            this.type = type;
        }

        public override LivingEntity getEntity()
        {
            return (LivingEntity)entity;
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
