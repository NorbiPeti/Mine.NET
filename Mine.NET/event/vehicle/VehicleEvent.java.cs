using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Represents a vehicle-related event.
     */
    public abstract class VehicleEventArgs : GameEventArgs
    {
        protected Vehicle vehicle;

        public VehicleEventArgs(Vehicle vehicle)
        {
            this.vehicle = vehicle;
        }

        /**
         * Get the vehicle.
         *
         * @return the vehicle
         */
        public Vehicle getVehicle()
        {
            return vehicle;
        }
    }
}
