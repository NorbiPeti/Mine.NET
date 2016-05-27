using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a living entity exits a vehicle.
     */
    public class VehicleExitEventArgs : VehicleEventArgs, Cancellable
    {
        private bool cancelled;
        private readonly LivingEntity exited;

        public VehicleExitEventArgs(Vehicle vehicle, LivingEntity exited) : base(vehicle)
        {
            this.exited = exited;
        }

        /**
         * Get the living entity that exited the vehicle.
         *
         * @return The entity.
         */
        public LivingEntity getExited()
        {
            return exited;
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
