using System;

namespace Mine.NET.material
{

    /**
     * Represents the different types of long grasses.
     */
    public class LongGrass : MaterialData<byte> {
        public LongGrass() : base(Materials.LONG_GRASS) {
        }

        public LongGrass(GrassSpecies species) :            this()
        {
            setSpecies(species);
        }

        public LongGrass(Materials type) : base(type) {
        }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
        public LongGrass(Materials type, byte data) : base(type, data) {
    }

    /**
     * Gets the current species of this grass
     *
     * @return GrassSpecies of this grass
     */
    public GrassSpecies getSpecies() {
            return (GrassSpecies)getData();
        }

        /**
         * Sets the species of this grass
         *
         * @param species New species of this grass
         */
        public void setSpecies(GrassSpecies species) {
            setData((GrassSpecies)species));
        }

        public override string ToString() {
            return getSpecies() + " " + base.ToString();
        }

        public new LongGrass Clone() { return (LongGrass)base.Clone(); }
    }
}
