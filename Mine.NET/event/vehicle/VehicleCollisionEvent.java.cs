using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle collides.
     */
    public abstract class VehicleCollisionEvent : VehicleEvent
    {
        public VehicleCollisionEvent(Vehicle vehicle) : base(vehicle)
        {
        }
    }
}
