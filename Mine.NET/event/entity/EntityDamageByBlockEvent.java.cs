using Mine.NET.block;
using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity is damaged by a block
     */
    public class EntityDamageByBlockEventArgs : EntityDamageEventArgs
    {
        private readonly Block damager;

        public EntityDamageByBlockEventArgs(Block damager, Entity damagee, DamageCause cause, Dictionary<DamageModifier, Double> modifiers, Dictionary<DamageModifier, Func<Double, Double>> modifierFunctions) :
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
