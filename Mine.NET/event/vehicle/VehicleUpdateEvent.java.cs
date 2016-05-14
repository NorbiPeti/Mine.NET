using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Called when a vehicle updates
     */
    public class VehicleUpdateEvent : VehicleEvent
    {
        private static readonly HandlerList handlers = new HandlerList();

        public VehicleUpdateEvent(Vehicle vehicle) : base(vehicle)
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
