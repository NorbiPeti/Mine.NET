using Mine.NET.entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET.Event.entity
{
    /**
     * Stores data for damage events
     */
    public class EntityDamageEvent<T> : EntityEvent<T>, Cancellable where T : Entity
    {
        private static readonly HandlerList handlers = new HandlerList();
        private static readonly DamageModifier[] MODIFIERS = new DamageModifier[] { DamageModifier.ABSORPTION, DamageModifier.ARMOR, DamageModifier.BASE, DamageModifier.BLOCKING, DamageModifier.HARD_HAT, DamageModifier.MAGIC, DamageModifier.RESISTANCE };
        private static readonly Func<Double, Double> ZERO = new Func<double, double>(delegate { return 0; });
        private readonly Dictionary<DamageModifier, Double> modifiers;
        private readonly Dictionary<DamageModifier, Func<Double, Double>> modifierFunctions;
        private readonly Dictionary<DamageModifier, Double> originals;
        private bool cancelled;
        private readonly DamageCause cause;

        public EntityDamageEvent(Entity damagee, DamageCause cause, Dictionary<DamageModifier, Double> modifiers, Dictionary<DamageModifier, Func<Double, Double>> modifierFunctions) :
            base(damagee)
        {
            if (modifiers.ContainsKey(DamageModifier.BASE)) throw new ArgumentException("BASE DamageModifier missing");
            if (modifierFunctions.Values.Any(f => f == null)) throw new ArgumentException("Cannot have null modifier function");
            this.originals = new Dictionary<DamageModifier, double>(modifiers);
            this.cause = cause;
            this.modifiers = modifiers;
            this.modifierFunctions = modifierFunctions;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            cancelled = cancel;
        }

        /**
         * Gets the original damage for the specified modifier, as defined at this
         * event's construction.
         *
         * @param type the modifier
         * @return the original damage
         * @throws ArgumentException if type is null
         */
        public double getOriginalDamage(DamageModifier type)
        {
            return originals[type];
        }

        /**
         * Sets the damage for the specified modifier.
         *
         * @param type the damage modifier
         * @param damage the scalar value of the damage's modifier
         * @see #getFinalDamage()
         * @throws ArgumentException if type is null
         * @throws UnsupportedOperationException if the caller does not support
         *     the particular DamageModifier, or to rephrase, when {@link
         *     #isApplicable(DamageModifier)} returns false
         */
        public void setDamage(DamageModifier type, double damage)
        {
            if (!modifiers.ContainsKey(type))
            {
                throw new NotSupportedException(type + " is not applicable to " + getEntity());
            }
            modifiers.Add(type, damage);
        }

        /**
         * Gets the damage change for some modifier
         *
         * @param type the damage modifier
         * @return The raw amount of damage caused by the event
         * @throws ArgumentException if type is null
         * @see DamageModifier#BASE
         */
        public double getDamage(DamageModifier type)
        {
            return modifiers[type];
        }

        /**
         * This checks to see if a particular modifier is valid for this event's
         * caller, such that, {@link #setDamage(DamageModifier, double)} will not
         * throw an {@link UnsupportedOperationException}.
         * <p>
         * {@link DamageModifier#BASE} is always applicable.
         *
         * @param type the modifier
         * @return true if the modifier is supported by the caller, false otherwise
         * @throws ArgumentException if type is null
         */
        public bool isApplicable(DamageModifier type)
        {
            return modifiers.ContainsKey(type);
        }

        /**
         * Gets the raw amount of damage caused by the event
         *
         * @return The raw amount of damage caused by the event
         * @see DamageModifier#BASE
         */
        public double getDamage()
        {
            return getDamage(DamageModifier.BASE);
        }

        /**
         * Gets the amount of damage caused by the event after all damage
         * reduction is applied.
         *
         * @return the amount of damage caused by the event
         */
        public double getFinalDamage()
        {
            double damage = 0;
            foreach (DamageModifier modifier in MODIFIERS)
            {
                damage += getDamage(modifier);
            }
            return damage;
        }

        /**
         * Sets the raw amount of damage caused by the event.
         * <p>
         * For compatibility this also recalculates the modifiers and scales
         * them by the difference between the modifier for the previous damage
         * value and the new one.
         *
         * @param damage The raw amount of damage caused by the event
         */
        public void setDamage(double damage)
        {
            // These have to happen in the same order as the server calculates them, keep the enum sorted
            double remaining = damage;
            double oldRemaining = getDamage(DamageModifier.BASE);
            foreach (DamageModifier modifier in MODIFIERS)
            {
                if (!isApplicable(modifier))
                {
                    continue;
                }

                Func<Double, Double> modifierFunction = modifierFunctions[modifier];
                double newVanilla = modifierFunction(remaining);
                double oldVanilla = modifierFunction(oldRemaining);
                double difference = oldVanilla - newVanilla;

                // Don't allow value to cross zero, assume zero values should be negative
                double old = getDamage(modifier);
                if (old > 0)
                {
                    setDamage(modifier, Math.Max(0, old - difference));
                }
                else
                {
                    setDamage(modifier, Math.Min(0, old - difference));
                }
                remaining += newVanilla;
                oldRemaining += oldVanilla;
            }

            setDamage(DamageModifier.BASE, damage);
        }

        /**
         * This method exists for legacy reasons to provide backwards
         * compatibility. It will not exist at runtime and should not be used
         * under any circumstances.
         * 
         * @param damage the new damage value
         */
        [Obsolete]
        public void _INVALID_setDamage(int damage)
        {
            setDamage(damage);
        }

        /**
         * Gets the cause of the damage.
         *
         * @return A DamageCause value detailing the cause of the damage.
         */
        public DamageCause getCause()
        {
            return cause;
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
         * An enum to specify the types of modifier
         */
        public enum DamageModifier
        {
            /**
             * This represents the amount of damage being done, also known as the
             * raw {@link EntityDamageEvent#getDamage()}.
             */
            BASE,
            /**
             * This represents the damage reduced by a wearing a helmet when hit
             * by a falling block.
             */
            HARD_HAT,
            /**
             * This represents  the damage reduction caused by blocking, only present for
             * {@link Player Players}.
             */
            BLOCKING,
            /**
             * This represents the damage reduction caused by wearing armor.
             */
            ARMOR,
            /**
             * This represents the damage reduction caused by the Resistance potion effect.
             */
            RESISTANCE,
            /**
             * This represents the damage reduction caused by the combination of:
             * <ul>
             * <li>
             *     Armor enchantments
             * </li><li>
             *     Witch's potion resistance
             * </li>
             * </ul>
             */
            MAGIC,
            /**
             * This represents the damage reduction caused by the absorption potion
             * effect.
             */
            ABSORPTION
        }

        /**
         * An enum to specify the cause of the damage
         */
        public enum DamageCause
        {

            /**
             * Damage caused when an entity contacts a block such as a Cactus.
             * <p>
             * Damage: 1 (Cactus)
             */
            CONTACT,
            /**
             * Damage caused when an entity attacks another entity.
             * <p>
             * Damage: variable
             */
            ENTITY_ATTACK,
            /**
             * Damage caused when attacked by a projectile.
             * <p>
             * Damage: variable
             */
            PROJECTILE,
            /**
             * Damage caused by being put in a block
             * <p>
             * Damage: 1
             */
            SUFFOCATION,
            /**
             * Damage caused when an entity falls a distance greater than 3 blocks
             * <p>
             * Damage: fall height - 3.0
             */
            FALL,
            /**
             * Damage caused by direct exposure to fire
             * <p>
             * Damage: 1
             */
            FIRE,
            /**
             * Damage caused due to burns caused by fire
             * <p>
             * Damage: 1
             */
            FIRE_TICK,
            /**
             * Damage caused due to a snowman melting
             * <p>
             * Damage: 1
             */
            MELTING,
            /**
             * Damage caused by direct exposure to lava
             * <p>
             * Damage: 4
             */
            LAVA,
            /**
             * Damage caused by running out of air while in water
             * <p>
             * Damage: 2
             */
            DROWNING,
            /**
             * Damage caused by being in the area when a block explodes.
             * <p>
             * Damage: variable
             */
            BLOCK_EXPLOSION,
            /**
             * Damage caused by being in the area when an entity, such as a
             * Creeper, explodes.
             * <p>
             * Damage: variable
             */
            ENTITY_EXPLOSION,
            /**
             * Damage caused by falling into the void
             * <p>
             * Damage: 4 for players
             */
            VOID,
            /**
             * Damage caused by being struck by lightning
             * <p>
             * Damage: 5
             */
            LIGHTNING,
            /**
             * Damage caused by committing suicide using the command "/kill"
             * <p>
             * Damage: 1000
             */
            SUICIDE,
            /**
             * Damage caused by starving due to having an empty hunger bar
             * <p>
             * Damage: 1
             */
            STARVATION,
            /**
             * Damage caused due to an ongoing poison effect
             * <p>
             * Damage: 1
             */
            POISON,
            /**
             * Damage caused by being hit by a damage potion or spell
             * <p>
             * Damage: variable
             */
            MAGIC,
            /**
             * Damage caused by Wither potion effect
             */
            WITHER,
            /**
             * Damage caused by being hit by a falling block which deals damage
             * <p>
             * <b>Note:</b> Not every block deals damage
             * <p>
             * Damage: variable
             */
            FALLING_BLOCK,
            /**
             * Damage caused in retaliation to another attack by the Thorns
             * enchantment.
             * <p>
             * Damage: 1-4 (Thorns)
             */
            THORNS,
            /**
             * Damage caused by a dragon breathing fire.
             * <p>
             * Damage: variable
             */
            DRAGON_BREATH,
            /**
             * Custom damage.
             * <p>
             * Damage: variable
             */
            CUSTOM,
            /**
             * Damage caused when an entity runs into a wall.
             * <p>
             * Damage: variable
             */
            FLY_INTO_WALL
        }
    }
}
