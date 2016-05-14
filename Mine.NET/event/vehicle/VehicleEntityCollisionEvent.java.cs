using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle collides with an entity.
     */
    public class VehicleEntityCollisionEvent : VehicleCollisionEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Entity entity;
        private bool cancelled = false;
        private bool cancelledPickup = false;
        private bool cancelledCollision = false;

        public VehicleEntityCollisionEvent(Vehicle vehicle, Entity entity) : base(vehicle)
        {
            this.entity = entity;
        }

        public Entity getEntity()
        {
            return entity;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }

        public bool isPickupCancelled()
        {
            return cancelledPickup;
        }

        public void setPickupCancelled(bool cancel)
        {
            cancelledPickup = cancel;
        }

        public bool isCollisionCancelled()
        {
            return cancelledCollision;
        }

        public void setCollisionCancelled(bool cancel)
        {
            cancelledCollision = cancel;
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
