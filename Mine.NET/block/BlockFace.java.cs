namespace Mine.NET.block
{
    /**
     * Represents the face of a block
     */
    public class BlockFace
    {
        public static BlockFace NORTH = new BlockFace(0, 0, -1);
        public static BlockFace EAST = new BlockFace(1, 0, 0);
        public static BlockFace SOUTH = new BlockFace(0, 0, 1);
        public static BlockFace WEST = new BlockFace(-1, 0, 0);
        public static BlockFace UP = new BlockFace(0, 1, 0);
        public static BlockFace DOWN = new BlockFace(0, -1, 0);
        public static BlockFace NORTH_EAST = new BlockFace(NORTH, EAST);
        public static BlockFace NORTH_WEST = new BlockFace(NORTH, WEST);
        public static BlockFace SOUTH_EAST = new BlockFace(SOUTH, EAST);
        public static BlockFace SOUTH_WEST = new BlockFace(SOUTH, WEST);
        public static BlockFace WEST_NORTH_WEST = new BlockFace(WEST, NORTH_WEST);
        public static BlockFace NORTH_NORTH_WEST = new BlockFace(NORTH, NORTH_WEST);
        public static BlockFace NORTH_NORTH_EAST = new BlockFace(NORTH, NORTH_EAST);
        public static BlockFace EAST_NORTH_EAST = new BlockFace(EAST, NORTH_EAST);
        public static BlockFace EAST_SOUTH_EAST = new BlockFace(EAST, SOUTH_EAST);
        public static BlockFace SOUTH_SOUTH_EAST = new BlockFace(SOUTH, SOUTH_EAST);
        public static BlockFace SOUTH_SOUTH_WEST = new BlockFace(SOUTH, SOUTH_WEST);
        public static BlockFace WEST_SOUTH_WEST = new BlockFace(WEST, SOUTH_WEST);
        public static BlockFace SELF = new BlockFace(0, 0, 0);

        private readonly int modX;
        private readonly int modY;
        private readonly int modZ;

        private BlockFace(int modX, int modY, int modZ)
        {
            this.modX = modX;
            this.modY = modY;
            this.modZ = modZ;
        }

        private BlockFace(BlockFace face1, BlockFace face2)
        {
            this.modX = face1.getModX() + face2.getModX();
            this.modY = face1.getModY() + face2.getModY();
            this.modZ = face1.getModZ() + face2.getModZ();
        }

        /**
         * Get the amount of X-coordinates to modify to get the represented block
         *
         * @return Amount of X-coordinates to modify
         */
        public int getModX()
        {
            return modX;
        }

        /**
         * Get the amount of Y-coordinates to modify to get the represented block
         *
         * @return Amount of Y-coordinates to modify
         */
        public int getModY()
        {
            return modY;
        }

        /**
         * Get the amount of Z-coordinates to modify to get the represented block
         *
         * @return Amount of Z-coordinates to modify
         */
        public int getModZ()
        {
            return modZ;
        }

        public BlockFace getOppositeFace()
        {
            if (this == NORTH)
                return BlockFace.SOUTH;
            else if (this == SOUTH)
                return BlockFace.NORTH;
            else if (this == EAST)
                return BlockFace.WEST;
            else if (this == WEST)
                return BlockFace.EAST;
            else if (this == UP)
                return BlockFace.DOWN;
            else if (this == DOWN)
                return BlockFace.UP;
            else if (this == NORTH_EAST)
                return BlockFace.SOUTH_WEST;
            else if (this == NORTH_WEST)
                return BlockFace.SOUTH_EAST;
            else if (this == SOUTH_EAST)
                return BlockFace.NORTH_WEST;
            else if (this == SOUTH_WEST)
                return BlockFace.NORTH_EAST;
            else if (this == WEST_NORTH_WEST)
                return BlockFace.EAST_SOUTH_EAST;
            else if (this == NORTH_NORTH_WEST)
                return BlockFace.SOUTH_SOUTH_EAST;
            else if (this == NORTH_NORTH_EAST)
                return BlockFace.SOUTH_SOUTH_WEST;
            else if (this == EAST_NORTH_EAST)
                return BlockFace.WEST_SOUTH_WEST;
            else if (this == EAST_SOUTH_EAST)
                return BlockFace.WEST_NORTH_WEST;
            else if (this == SOUTH_SOUTH_EAST)
                return BlockFace.NORTH_NORTH_WEST;
            else if (this == SOUTH_SOUTH_WEST)
                return BlockFace.NORTH_NORTH_EAST;
            else if (this == WEST_SOUTH_WEST)
                return BlockFace.EAST_NORTH_EAST;
            else if (this == SELF)
                return BlockFace.SELF;
            return BlockFace.SELF;
        }

        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType())
                return base.Equals(obj);
            BlockFace other = (BlockFace)obj;
            return modX == other.modX && modY == other.modY && modZ == other.modZ;
        }
    }
}
