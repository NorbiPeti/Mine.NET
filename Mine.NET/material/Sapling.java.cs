using System;

namespace Mine.NET.material
{
    /**
     * Represents the different types of Tree block that face a direction.
     *
     * @see Materials#SAPLING
     */
    public class Sapling : Wood
    {

        /**
         * Constructs a sapling.
         */
        public Sapling() : this(DEFAULT_SPECIES)
        {
        }

        /**
         * Constructs a sapling of the given tree species.
         *
         * @param species the species of the sapling
         */
        public Sapling(TreeSpecies species) : this(species, false)
        {
        }

        /**
         * Constructs a sapling of the given tree species and if is it instant
         * growable
         *
         * @param species the species of the tree block
         * @param isInstantGrowable true if the Sapling should grow when next ticked with bonemeal
         */
        public Sapling(TreeSpecies species, bool isInstantGrowable) : this(Materials.SAPLING, species, isInstantGrowable)
        {
        }

        /**
         * Constructs a sapling of the given type.
         *
         * @param type the type of tree block
         */
        public Sapling(Materials type) : this(type, DEFAULT_SPECIES, false)
        {
        }

        /**
         * Constructs a sapling of the given type and tree species.
         *
         * @param type the type of sapling
         * @param species the species of the sapling
         */
        public Sapling(Materials type, TreeSpecies species) : this(type, species, false)
        {
        }

        /**
         * Constructs a sapling of the given type and tree species and if is it
         * instant growable
         *
         * @param type the type of sapling
         * @param species the species of the sapling
         * @param isInstantGrowable true if the Sapling should grow when next ticked
         * with bonemeal
         */
        public Sapling(Materials type, TreeSpecies species, bool isInstantGrowable) : base(type, species)
        {
            setIsInstantGrowable(isInstantGrowable);
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Sapling(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Checks if the Sapling would grow when next ticked with bonemeal
         *
         * @return true if the Sapling would grow when next ticked with bonemeal
         */
        public bool isInstantGrowable()
        {
            return (getData() & 0x8) == 0x8;
        }

        /**
         * Set whether this sapling will grow when next ticked with bonemeal
         *
         * @param isInstantGrowable true if the Sapling should grow when next ticked
         * with bonemeal
         */
        public void setIsInstantGrowable(bool isInstantGrowable)
        {
            setData(isInstantGrowable ? (byte)((getData() & 0x7) | 0x8) : (byte)(getData() & 0x7));
        }

        public override string ToString()
        {
            return getSpecies() + " " + (isInstantGrowable() ? " IS_INSTANT_GROWABLE " : "") + " " + base.ToString();
        }

        public new Sapling Clone() { return (Sapling)base.Clone(); }
    }
}
