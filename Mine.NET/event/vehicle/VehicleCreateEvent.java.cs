using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle is created.
     */
    public class VehicleCreateEventArgs : VehicleEventArgs
    {
        public VehicleCreateEventArgs(Vehicle vehicle) : base(vehicle)
        {
        }
    }
}
