using System;

namespace Mine.NET.material
{
    /**
     * Represents the different types of wooden steps.
     *
     * @see Materials#WOOD_STEP
     */
    public class WoodenStep : Wood
    {
        protected static new readonly Materials DEFAULT_TYPE = Materials.WOOD_STEP;
        protected static readonly bool DEFAULT_INVERTED = false;

        /**
         * Constructs a wooden step.
         */
        public WoodenStep() : this(DEFAULT_SPECIES, DEFAULT_INVERTED)
        {
        }

        /**
         * Constructs a wooden step of the given tree species.
         *
         * @param species the species of the wooden step
         */
        public WoodenStep(TreeSpecies species) : this(species, DEFAULT_INVERTED)
        {
        }

        /**
         * Constructs a wooden step of the given type and tree species, either
         * inverted or not.
         *
         * @param species the species of the wooden step
         * @param inv true the step is at the top of the block
         */
        public WoodenStep(TreeSpecies species, bool inv) : base(DEFAULT_TYPE, species)
        {
            setInverted(inv);
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public WoodenStep(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Test if step is inverted
         *
         * @return true if inverted (top half), false if normal (bottom half)
         */

        public bool isInverted()
        {
            return ((getData() & 0x8) != 0);
        }

        /**
         * Set step inverted state
         *
         * @param inv - true if step is inverted (top half), false if step is normal
         * (bottom half)
         */

        public void setInverted(bool inv)
        {
            int dat = getData() & 0x7;
            if (inv)
            {
                dat |= 0x8;
            }
            setData((byte)dat);
        }

        public new WoodenStep Clone() { return (WoodenStep)base.Clone(); }

        public override string ToString()
        {
            return base.ToString() + " " + getSpecies() + (isInverted() ? " inverted" : "");
        }
    }
}
