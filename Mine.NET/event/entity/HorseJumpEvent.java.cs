using System;
using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a horse jumps.
     */
    public class HorseJumpEventArgs : EntityEventArgs<Horse>
    {
        private float power;

        public HorseJumpEventArgs(Horse horse, float power) : base(horse)
        {
            this.power = power;
        }

        /**
         * Gets the power of the jump.
         * <p>
         * Power is a value that defines how much of the horse's jump strength
         * should be used for the jump. Power is effectively multiplied times
         * the horse's jump strength to determine how high the jump is; 0
         * represents no jump strength while 1 represents full jump strength.
         * Setting power to a value above 1 will use additional jump strength
         * that the horse does not usually have.
         * <p>
         * Power does not affect how high the horse is capable of jumping, only
         * how much of its jumping capability will be used in this jump. To set
         * the horse's overall jump strength, see {@link
         * Horse#setJumpStrength(double)}.
         *
         * @return jump strength
         */
        public float getPower()
        {
            return power;
        }
    }
}
