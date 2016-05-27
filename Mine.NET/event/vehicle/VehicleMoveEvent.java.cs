using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle moves.
     */
    public class VehicleMoveEventArgs : VehicleEventArgs
    {
        private readonly Location from;
        private readonly Location to;

        public VehicleMoveEventArgs(Vehicle vehicle, Location from, Location to) : base(vehicle)
        {
            this.from = from;
            this.to = to;
        }

        /**
         * Get the previous position.
         *
         * @return Old position.
         */
        public Location getFrom()
        {
            return from;
        }

        /**
         * Get the next position.
         *
         * @return New position.
         */
        public Location getTo()
        {
            return to;
        }
    }
}
