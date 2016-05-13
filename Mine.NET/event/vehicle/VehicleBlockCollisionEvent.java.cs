using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle collides with a block.
     */
    public class VehicleBlockCollisionEvent : VehicleCollisionEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Block block;

        public VehicleBlockCollisionEvent(Vehicle vehicle, Block block) : base(vehicle)
        {
            this.block = block;
        }

        /**
         * Gets the block the vehicle collided with
         *
         * @return the block the vehicle collided with
         */
        public Block getBlock()
        {
            return block;
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
