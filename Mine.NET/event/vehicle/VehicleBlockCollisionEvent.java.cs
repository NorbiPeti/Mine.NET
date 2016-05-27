using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle collides with a block.
     */
    public class VehicleBlockCollisionEventArgs : VehicleCollisionEventArgs
    {
        private readonly Block block;

        public VehicleBlockCollisionEventArgs(Vehicle vehicle, Block block) : base(vehicle)
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
    }
}
