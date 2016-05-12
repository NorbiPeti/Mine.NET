using Mine.NET.block;
using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity is damaged by a block
     */
    public class EntityDamageByBlockEvent : EntityDamageEvent
    {
        private readonly Block damager;

        [Obsolete]
        public EntityDamageByBlockEvent(Block damager, Entity damagee, DamageCause cause, int damage) :
            this(damager, damagee, cause, (double)damage)
        {

        }

        [Obsolete]
        public EntityDamageByBlockEvent(Block damager, Entity damagee, DamageCause cause, double damage) :
            base(damagee, cause, damage)
        {
            this.damager = damager;
        }

        public EntityDamageByBlockEvent(Block damager, Entity damagee, DamageCause cause, Dictionary<DamageModifier, Double> modifiers, Dictionary<DamageModifier, Func<Double, Double>> modifierFunctions) :
            base(damagee, cause, modifiers, modifierFunctions)
        {
            this.damager = damager;
        }

        /**
         * Returns the block that damaged the player.
         *
         * @return Block that damaged the player
         */
        public Block getDamager()
        {
            return damager;
        }
    }
}
