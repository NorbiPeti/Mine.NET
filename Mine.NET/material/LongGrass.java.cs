using System;

namespace Mine.NET.material
{

    /**
     * Represents the different types of long grasses.
     */
    public class LongGrass : MaterialData
    {
        public LongGrass() : base(Materials.LONG_GRASS)
        {
        }

        public LongGrass(GrassSpecies species) : this()
        {
            setSpecies(species);
        }

        public LongGrass(Materials type) : base(type)
        {
        }

        private GrassSpecies species;
        /**
         * Gets the current species of this grass
         *
         * @return GrassSpecies of this grass
         */
        public GrassSpecies getSpecies()
        {
            //return (GrassSpecies)getData();
            return species;
        }

        /**
         * Sets the species of this grass
         *
         * @param species New species of this grass
         */
        public void setSpecies(GrassSpecies species)
        {
            //setData((GrassSpecies)species));
            this.species = species;
        }

        public override string ToString()
        {
            return getSpecies() + " " + base.ToString();
        }

        public new LongGrass Clone() { return (LongGrass)base.Clone(); }
    }
}
