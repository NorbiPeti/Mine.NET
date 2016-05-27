using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle collides with an entity.
     */
    public class VehicleEntityCollisionEventArgs : VehicleCollisionEventArgs, Cancellable
    {
        private readonly Entity entity;
        private bool cancelled = false;
        private bool cancelledPickup = false;
        private bool cancelledCollision = false;

        public VehicleEntityCollisionEventArgs(Vehicle vehicle, Entity entity) : base(vehicle)
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
    }
}
