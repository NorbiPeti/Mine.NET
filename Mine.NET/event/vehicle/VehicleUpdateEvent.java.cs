using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Called when a vehicle updates
     */
    public class VehicleUpdateEventArgs : VehicleEventArgs
    {
        public VehicleUpdateEventArgs(Vehicle vehicle) : base(vehicle)
        {
        }
    }
}
