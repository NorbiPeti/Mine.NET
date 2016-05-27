using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when an entity enters a vehicle.
     */
    public class VehicleEnterEventArgs : VehicleEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly Entity entered;

        public VehicleEnterEventArgs(Vehicle vehicle, Entity entered) : base(vehicle)
        {
            this.entered = entered;
        }

        /**
         * Gets the Entity that entered the vehicle.
         *
         * @return the Entity that entered the vehicle
         */
        public Entity getEntered()
        {
            return entered;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }
    }
}
