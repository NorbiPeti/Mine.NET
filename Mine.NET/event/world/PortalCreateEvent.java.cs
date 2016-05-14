using Mine.NET.block;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mine.NET.Event.world
{
    /**
     * Called when a portal is created
     */
    public class PortalCreateEvent : WorldEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel = false;
        private readonly List<Block> blocks = new List<Block>();
        private CreateReason reason = CreateReason.FIRE;

        public PortalCreateEvent(Collection<Block> blocks, World world, CreateReason reason) : base(world)
        {
            this.blocks.AddRange(blocks);
            this.reason = reason;
        }

        /**
         * Gets an array list of all the blocks associated with the created portal
         *
         * @return array list of all the blocks associated with the created portal
         */
        public List<Block> getBlocks()
        {
            return this.blocks;
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
         * Gets the reason for the portal's creation
         *
         * @return CreateReason for the portal's creation
         */
        public CreateReason getReason()
        {
            return reason;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }

        /**
         * An enum to specify the various reasons for a portal's creation
         */
        public enum CreateReason
        {
            /**
             * When a portal is created 'traditionally' due to a portal frame
             * being set on fire.
             */
            FIRE,
            /**
             * When a portal is created as a destination for an existing portal
             * when using the custom PortalTravelAgent
             */
            OBC_DESTINATION
        }
    }
}
