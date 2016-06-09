using System;

namespace Mine.NET.material
{
    /**
     * Represents wood blocks of different species.
     *
     * @see Materials#WOOD
     * @see Materials#SAPLING
     * @see Materials#WOOD_DOUBLE_STEP
     */
    public class Wood : MaterialData
    {
        protected static readonly Materials DEFAULT_TYPE = Materials.WOOD;
        protected static readonly TreeSpecies DEFAULT_SPECIES = TreeSpecies.GENERIC;

        /**
         * Constructs a wood block.
         */
        public Wood() : this(DEFAULT_TYPE, DEFAULT_SPECIES)
        {
        }

        /**
         * Constructs a wood block of the given tree species.
         * 
         * @param species the species of the wood block
         */
        public Wood(TreeSpecies species) : this(DEFAULT_TYPE, species)
        {
        }

        /**
         * Constructs a wood block of the given type.
         *
         * @param type the type of wood block
         */
        public Wood(Materials type) : this(type, DEFAULT_SPECIES)
        {
        }

        /**
         * Constructs a wood block of the given type and tree species.
         *
         * @param type the type of wood block
         * @param species the species of the wood block
         */
        public Wood(Materials type, TreeSpecies species) : base(getSpeciesType(type, species))
        {
            // Ensure only valid species-type combinations
            setSpecies(species);
        }

        private TreeSpecies species;
        /**
         * Gets the current species of this wood block
         *
         * @return TreeSpecies of this wood block
         */
        public TreeSpecies getSpecies()
        {
            switch (getItemType())
            {
                case Materials.WOOD:
                case Materials.WOOD_DOUBLE_STEP:
                //return Materials.getByData((byte) getData());
                case Materials.LOG:
                case Materials.LEAVES:
                //return Materials.getByData((byte) (getData() & 0x3));
                case Materials.LOG_2:
                case Materials.LEAVES_2:
                //return Materials.getByData((byte) ((getData() & 0x3) | 0x4));
                case Materials.SAPLING:
                case Materials.WOOD_STEP:
                    //return TreeSpecies.getByData((byte) (getData() & 0x7));
                    return species;
                default:
                    throw new ArgumentException("Invalid block type for tree species");
            }
        }

        /**
         * Correct the block type for certain species-type combinations.
         *
         * @param type The desired type
         * @param species The required species
         * @return The actual type for this species given the desired type
         */
        private static Materials getSpeciesType(Materials type, TreeSpecies species)
        {
            switch (species)
            {
                case TreeSpecies.GENERIC:
                case TreeSpecies.REDWOOD:
                case TreeSpecies.BIRCH:
                case TreeSpecies.JUNGLE:
                    switch (type)
                    {
                        case Materials.LOG_2:
                            return Materials.LOG;
                        case Materials.LEAVES_2:
                            return Materials.LEAVES;
                        default:
                            break;
                    }
                    break;
                case TreeSpecies.ACACIA:
                case TreeSpecies.DARK_OAK:
                    switch (type)
                    {
                        case Materials.LOG:
                            return Materials.LOG_2;
                        case Materials.LEAVES:
                            return Materials.LEAVES_2;
                        default:
                            break;
                    }
                    break;
            }
            return type;
        }

        /**
         * Sets the species of this wood block
         *
         * @param species New species of this wood block
         */
        public void setSpecies(TreeSpecies species)
        {
            bool firstType = false;
            switch (getItemType())
            {
                case Materials.WOOD:
                case Materials.WOOD_DOUBLE_STEP:
                    //setData(species.getData());
                    this.species = species;
                    break;
                case Materials.LOG:
                case Materials.LEAVES:
                    firstType = true;
                    // fall through to next switch statement below - Control cannot fall through from one case label('case Materials.LEAVES:') to another
                    goto case Materials.LOG_2; //http://stackoverflow.com/questions/174155/switch-statement-fallthrough-in-c
                case Materials.LOG_2:
                case Materials.LEAVES_2:
                    switch (species)
                    {
                        case TreeSpecies.GENERIC:
                        case TreeSpecies.REDWOOD:
                        case TreeSpecies.BIRCH:
                        case TreeSpecies.JUNGLE:
                            if (!firstType)
                            {
                                throw new ArgumentException("Invalid tree species for block type, use block type 2 instead");
                            }
                            break;
                        case TreeSpecies.ACACIA:
                        case TreeSpecies.DARK_OAK:
                            if (firstType)
                            {
                                throw new ArgumentException("Invalid tree species for block type 2, use block type instead");
                            }
                            break;
                    }
                    //setData((byte) ((getData() & 0xC) | (species.getData() & 0x3)));
                    this.species = species;
                    break;
                case Materials.SAPLING:
                case Materials.WOOD_STEP:
                    //setData((byte) ((getData() & 0x8) | species.getData()));
                    this.species = species;
                    break;
                default:
                    throw new ArgumentException("Invalid block type for tree species");
            }
        }

        public override string ToString()
        {
            return getSpecies() + " " + base.ToString();
        }

        public new Wood Clone() { return (Wood)base.Clone(); }
    }
}
