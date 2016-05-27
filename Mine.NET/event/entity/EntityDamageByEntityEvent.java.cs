using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity is damaged by an entity
     */
    public class EntityDamageByEntityEventArgs : EntityDamageEventArgs
    {
        private readonly Entity damager;

        public EntityDamageByEntityEventArgs(Entity damager, Entity damagee, DamageCause cause, Dictionary<DamageModifier, Double> modifiers, Dictionary<DamageModifier, Func<Double, Double>> modifierFunctions) :
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
