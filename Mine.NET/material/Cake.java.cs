using System;

namespace Mine.NET.material
{
    public class Cake : MaterialData
    {
        private byte sliceseaten = 0;

        public Cake(): base(Materials.CAKE_BLOCK)
        {
        }

        public Cake(Materials type): base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        public Cake(Materials type, byte sliceseaten):            base(type)
        {
            this.sliceseaten = sliceseaten;
        }

        /**
         * Gets the number of slices eaten from this cake
         *
         * @return The number of slices eaten
         */
        public int getSlicesEaten()
        {
            return sliceseaten;
        }

        /**
         * Gets the number of slices remaining on this cake
         *
         * @return The number of slices remaining
         */
        public int getSlicesRemaining()
        {
            return 6 - sliceseaten;
        }

        /**
         * Sets the number of slices eaten from this cake
         *
         * @param n The number of slices eaten
         */
        public void setSlicesEaten(int n)
        {
            if (n < 6)
            {
                sliceseaten = (byte)n;
            } // TODO: else destroy the block? Probably not possible though
        }

        /**
         * Sets the number of slices remaining on this cake
         *
         * @param n The number of slices remaining
         */
        public void setSlicesRemaining(int n)
        {
            if (n > 6)
            {
                n = 6;
            }
            sliceseaten = (byte)(6 - n);
        }

        public override string ToString()
        {
            return base.ToString() + " " + getSlicesEaten() + "/" + getSlicesRemaining() + " slices eaten/remaining";
        }

        public new Cake Clone()
        {
            return (Cake)base.Clone();
        }
    }
}
