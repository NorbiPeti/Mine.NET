using Mine.NET.entity;
using Mine.NET.Event;
using Mine.NET.Event.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Stores data for health-regain events
     */
    public class EntityRegainHealthEvent : EntityEvent<Entity>, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private double amount;
        private readonly RegainReason regainReason;

        public EntityRegainHealthEvent(Entity entity, double amount, RegainReason regainReason) :
            base(entity)
        {
            this.amount = amount;
            this.regainReason = regainReason;
        }

        /**
         * Gets the amount of regained health
         *
         * @return The amount of health regained
         */
        public double getAmount()
        {
            return amount;
        }

        /**
         * Sets the amount of regained health
         *
         * @param amount the amount of health the entity will regain
         */
        public void setAmount(double amount)
        {
            this.amount = amount;
        }

        public override bool isCancelled()
        {
            return cancelled;
        }

        public override void setCancelled(bool cancel)
        {
            cancelled = cancel;
        }

        /**
         * Gets the reason for why the entity is regaining health
         *
         * @return A RegainReason detailing the reason for the entity regaining
         *     health
         */
        public RegainReason getRegainReason()
        {
            return regainReason;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }

        /**
         * An enum to specify the type of health regaining that is occurring
         */
        public enum RegainReason
        {

            /**
             * When a player regains health from regenerating due to Peaceful mode
             * (difficulty=0)
             */
            REGEN,
            /**
             * When a player regains health from regenerating due to their hunger
             * being satisfied
             */
            SATIATED,
            /**
             * When a player regains health from eating consumables
             */
            EATING,
            /**
             * When an ender dragon regains health from an ender crystal
             */
            ENDER_CRYSTAL,
            /**
             * When a player is healed by a potion or spell
             */
            MAGIC,
            /**
             * When a player is healed over time by a potion or spell
             */
            MAGIC_REGEN,
            /**
             * When a wither is filling its health during spawning
             */
            WITHER_SPAWN,
            /**
             * When an entity is damaged by the Wither potion effect
             */
            WITHER,
            /**
             * Any other reason not covered by the reasons above
             */
            CUSTOM
        }
    }
}
