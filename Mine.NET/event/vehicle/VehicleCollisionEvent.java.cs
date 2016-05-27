using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle collides.
     */
    public abstract class VehicleCollisionEventArgs : VehicleEventArgs
    {
        public VehicleCollisionEventArgs(Vehicle vehicle) : base(vehicle)
        {
        }
    }
}
