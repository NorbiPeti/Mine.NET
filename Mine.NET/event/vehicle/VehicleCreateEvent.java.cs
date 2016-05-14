using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle is created.
     */
    public class VehicleCreateEvent : VehicleEvent
    {
        private static readonly HandlerList handlers = new HandlerList();

        public VehicleCreateEvent(Vehicle vehicle) : base(vehicle)
        {
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
