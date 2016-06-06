using System;

namespace Mine.NET.material
{
    /**
     * Represents a cauldron
     */
    public class Cauldron : MaterialData
    {
        private static readonly int CAULDRON_FULL = 3;
        private static readonly int CAULDRON_EMPTY = 0;

        private byte waterlevel = 0;

        public Cauldron() :
            base(Materials.CAULDRON)
        {
        }

        /**
         *
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        public Cauldron(byte level) : base(Materials.CAULDRON)
        {
            waterlevel = level;
        }

        /**
         * Check if the cauldron is full.
         *
         * @return True if it is full.
         */
        public bool isFull()
        {
            return waterlevel >= CAULDRON_FULL;
        }

        /**
         * Check if the cauldron is empty.
         *
         * @return True if it is empty.
         */
        public bool isEmpty()
        {
            return waterlevel <= CAULDRON_EMPTY;
        }

        public override string ToString()
        {
            return (isEmpty() ? "EMPTY" : (isFull() ? "FULL" : waterlevel + "/3 FULL")) + " CAULDRON";
        }

        public new Cauldron Clone()
        {
            return (Cauldron)base.Clone();
        }
    }
}
