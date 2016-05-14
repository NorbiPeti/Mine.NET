using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle receives damage.
     */
    public class VehicleDamageEvent : VehicleEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Entity attacker;
        private double damage;
        private bool cancelled;

        public VehicleDamageEvent(Vehicle vehicle, Entity attacker, double damage) : base(vehicle)
        {
            this.attacker = attacker;
            this.damage = damage;
        }

        /**
         * Gets the Entity that is attacking the vehicle
         *
         * @return the Entity that is attacking the vehicle
         */
        public Entity getAttacker()
        {
            return attacker;
        }

        /**
         * Gets the damage done to the vehicle
         *
         * @return the damage done to the vehicle
         */
        public double getDamage()
        {
            return damage;
        }

        /**
         * This method exists for legacy reasons to provide backwards
         * compatibility. It will not exist at runtime and should not be used
         * under any circumstances.
         * 
         * @return the damage
         */
        [Obsolete]
        public int _INVALID_getDamage()
        {
            return NumberConversions.ceil(getDamage());
        }

        /**
         * Sets the damage done to the vehicle
         *
         * @param damage The damage
         */
        public void setDamage(double damage)
        {
            this.damage = damage;
        }

        /**
         * This method exists for legacy reasons to provide backwards
         * compatibility. It will not exist at runtime and should not be used
         * under any circumstances.
         * 
         * @param damage the damage
         */
        [Obsolete]
        public void _INVALID_setDamage(int damage)
        {
            setDamage(damage);
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
