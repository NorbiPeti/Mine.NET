using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity is damaged by an entity
     */
    public class EntityDamageByEntityEvent : EntityDamageEvent
    {
        private readonly Entity damager;

        [Obsolete]
        public EntityDamageByEntityEvent(Entity damager, Entity damagee, DamageCause cause, int damage) :
            this(damager, damagee, cause, (double)damage)
        {
        }

        [Obsolete]
        public EntityDamageByEntityEvent(Entity damager, Entity damagee, DamageCause cause, double damage) :
            base(damagee, cause, damage)
        {
            this.damager = damager;
        }

        public EntityDamageByEntityEvent(Entity damager, Entity damagee, DamageCause cause, Dictionary<DamageModifier, Double> modifiers, Dictionary<DamageModifier, Func<Double, Double>> modifierFunctions) :
            base(damagee, cause, modifiers, modifierFunctions)
        {
            this.damager = damager;
        }

        /**
         * Returns the entity that damaged the defender.
         *
         * @return Entity that damaged the defender.
         */
        public Entity getDamager()
        {
            return damager;
        }
    }
}
