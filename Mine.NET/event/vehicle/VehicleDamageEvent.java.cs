using Mine.NET.entity;

namespace Mine.NET.Event.vehicle
{
    /**
     * Raised when a vehicle receives damage.
     */
    public class VehicleDamageEventArgs : VehicleEventArgs, Cancellable
    {
        private readonly Entity attacker;
        private double damage;
        private bool cancelled;

        public VehicleDamageEventArgs(Vehicle vehicle, Entity attacker, double damage) : base(vehicle)
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
         * Sets the damage done to the vehicle
         *
         * @param damage The damage
         */
        public void setDamage(double damage)
        {
            this.damage = damage;
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
