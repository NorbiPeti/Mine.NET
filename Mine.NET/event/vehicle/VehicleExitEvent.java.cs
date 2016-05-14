using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a living entity exits a vehicle.
     */
    public class VehicleExitEvent : VehicleEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private readonly LivingEntity exited;

        public VehicleExitEvent(Vehicle vehicle, LivingEntity exited) : base(vehicle)
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
