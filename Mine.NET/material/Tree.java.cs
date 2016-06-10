using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents the different types of Tree block that face a direction.
     *
     * @see Materials#LOG
     * @see Materials#LOG_2
     */
    public class Tree : Wood
    {
        protected static new readonly Materials DEFAULT_TYPE = Materials.LOG;
        protected static readonly BlockFaces DEFAULT_DIRECTION = BlockFaces.UP;

        /**
         * Constructs a tree block.
         */
        public Tree() : this(DEFAULT_TYPE, DEFAULT_SPECIES, DEFAULT_DIRECTION)
        {
        }

        /**
         * Constructs a tree block of the given tree species.
         *
         * @param species the species of the tree block
         */
        public Tree(TreeSpecies species) : this(DEFAULT_TYPE, species, DEFAULT_DIRECTION)
        {
        }

        /**
         * Constructs a tree block of the given tree species, and facing the given
         * direction.
         *
         * @param species the species of the tree block
         * @param dir the direction the tree block is facing
         */
        public Tree(TreeSpecies species, BlockFaces dir) : this(DEFAULT_TYPE, species, dir)
        {
        }

        /**
         * Constructs a tree block of the given type.
         *
         * @param type the type of tree block
         */
        public Tree(Materials type) : this(type, DEFAULT_SPECIES, DEFAULT_DIRECTION)
        {
        }

        /**
         * Constructs a tree block of the given type and tree species.
         *
         * @param type the type of tree block
         * @param species the species of the tree block
         */
        public Tree(Materials type, TreeSpecies species) : this(type, species, DEFAULT_DIRECTION)
        {
        }

        /**
         * Constructs a tree block of the given type and tree species, and facing
         * the given direction.
         *
         * @param type the type of tree block
         * @param species the species of the tree block
         * @param dir the direction the tree block is facing
         */
        public Tree(Materials type, TreeSpecies species, BlockFaces dir) : base(type, species)
        {
            setDirection(dir);
        }

        private BlockFaces dir;
        /**
         * Get direction of the log
         *
         * @return one of:
         * <ul>
         * <li>BlockFaces.TOP for upright (default)
         * <li>BlockFaces.NORTH (east-west)
         * <li>BlockFaces.WEST (north-south)
         * <li>BlockFaces.SELF (directionless)
         * </ul>
         */
        public BlockFaces getDirection()
        {
            /*switch ((getData() >> 2) & 0x3)
            {
                case 0: // Up-down
                default:
                    return BlockFaces.UP;
                case 1: // North-south
                    return BlockFaces.WEST;
                case 2: // East-west
                    return BlockFaces.NORTH;
                case 3: // Directionless (bark on all sides)
                    return BlockFaces.SELF;
            }*/
            return dir;
        }

        /**
         * Set direction of the log
         *
         * @param dir - direction of end of log (BlockFaces.SELF for no direction)
         */
        public void setDirection(BlockFaces dir)
        {
            /*int dat;
            switch (dir)
            {
                case BlockFaces.UP:
                case BlockFaces.DOWN:
                default:
                    dat = 0;
                    break;
                case BlockFaces.WEST:
                case BlockFaces.EAST:
                    dat = 4; // 1<<2
                    break;
                case BlockFaces.NORTH:
                case BlockFaces.SOUTH:
                    dat = 8; // 2<<2
                    break;
                case BlockFaces.SELF:
                    dat = 12; // 3<<2
                    break;
            }
            setData((byte)((getData() & 0x3) | dat));*/
            this.dir = dir;
        }

        public override string ToString()
        {
            return getSpecies() + " " + getDirection() + " " + base.ToString();
        }

        public new Tree Clone() { return (Tree)base.Clone(); }
    }
}
