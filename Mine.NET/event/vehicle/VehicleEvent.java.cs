using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Represents a vehicle-related event.
     */
    public abstract class VehicleEvent : Event
    {
        protected Vehicle vehicle;

        public VehicleEvent(Vehicle vehicle)
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
