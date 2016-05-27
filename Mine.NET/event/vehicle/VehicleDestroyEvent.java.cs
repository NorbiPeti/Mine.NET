using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle is destroyed, which could be caused by either a
     * player or the environment. This is not raised if the boat is simply
     * 'removed' due to other means.
     */
    public class VehicleDestroyEventArgs : VehicleEventArgs, Cancellable
    {
        private readonly Entity attacker;
        private bool cancelled;

        public VehicleDestroyEventArgs(Vehicle vehicle, Entity attacker) : base(vehicle)
        {
            this.attacker = attacker;
        }

        /**
         * Gets the Entity that has destroyed the vehicle, potentially null
         *
         * @return the Entity that has destroyed the vehicle, potentially null
         */
        public Entity getAttacker()
        {
            return attacker;
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
