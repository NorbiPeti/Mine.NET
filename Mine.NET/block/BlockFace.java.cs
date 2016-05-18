namespace Mine.NET.block
{
    public enum BlockFaces
    {
        NORTH,
        EAST,
        SOUTH,
        WEST,
        UP,
        DOWN,
        NORTH_EAST,
        NORTH_WEST,
        SOUTH_EAST,
        SOUTH_WEST,
        WEST_NORTH_WEST,
        NORTH_NORTH_WEST,
        NORTH_NORTH_EAST,
        EAST_NORTH_EAST,
        EAST_SOUTH_EAST,
        SOUTH_SOUTH_EAST,
        SOUTH_SOUTH_WEST,
        WEST_SOUTH_WEST,
        SELF
    }

    /**
     * Represents the face of a block
     */
    public static class BlockFace
    {
        /**
         * Get the amount of X-coordinates to modify to get the represented block
         *
         * @return Amount of X-coordinates to modify
         */
        public static int getModX(BlockFaces face)
        {
            switch (face)
            {
                case BlockFaces.EAST:
                case BlockFaces.NORTH_EAST:
                case BlockFaces.NORTH_NORTH_EAST:
                case BlockFaces.EAST_NORTH_EAST:
                case BlockFaces.EAST_SOUTH_EAST:
                case BlockFaces.SOUTH_EAST:
                case BlockFaces.SOUTH_SOUTH_EAST:
                    return 1;
                case BlockFaces.WEST:
                case BlockFaces.NORTH_NORTH_WEST:
                case BlockFaces.NORTH_WEST:
                case BlockFaces.SOUTH_SOUTH_WEST:
                case BlockFaces.SOUTH_WEST:
                case BlockFaces.WEST_NORTH_WEST:
                case BlockFaces.WEST_SOUTH_WEST:
                    return -1;
                default:
                    return 0;
            }
        }

        /**
         * Get the amount of Y-coordinates to modify to get the represented block
         *
         * @return Amount of Y-coordinates to modify
         */
        public static int getModY(BlockFaces face)
        {
            switch (face)
            {
                case BlockFaces.UP:
                    return 1;
                case BlockFaces.DOWN:
                    return -1;
                default:
                    return 0;
            }
        }

        /**
         * Get the amount of Z-coordinates to modify to get the represented block
         *
         * @return Amount of Z-coordinates to modify
         */
        public static int getModZ(BlockFaces face)
        {
            switch (face)
            {
                case BlockFaces.SOUTH:
                case BlockFaces.EAST_SOUTH_EAST:
                case BlockFaces.SOUTH_EAST:
                case BlockFaces.SOUTH_SOUTH_EAST:
                case BlockFaces.SOUTH_SOUTH_WEST:
                case BlockFaces.SOUTH_WEST:
                case BlockFaces.WEST_SOUTH_WEST:
                    return 1;
                case BlockFaces.NORTH:
                case BlockFaces.EAST_NORTH_EAST:
                case BlockFaces.NORTH_EAST:
                case BlockFaces.NORTH_NORTH_EAST:
                case BlockFaces.NORTH_NORTH_WEST:
                case BlockFaces.NORTH_WEST:
                case BlockFaces.WEST_NORTH_WEST:
                    return -1;
                default:
                    return 0;
            }
        }

        public static BlockFaces getOppositeFace(BlockFaces face)
        {
            if (face == BlockFaces.NORTH)
                return BlockFaces.SOUTH;
            else if (face == BlockFaces.SOUTH)
                return BlockFaces.NORTH;
            else if (face == BlockFaces.EAST)
                return BlockFaces.WEST;
            else if (face == BlockFaces.WEST)
                return BlockFaces.EAST;
            else if (face == BlockFaces.UP)
                return BlockFaces.DOWN;
            else if (face == BlockFaces.DOWN)
                return BlockFaces.UP;
            else if (face == BlockFaces.NORTH_EAST)
                return BlockFaces.SOUTH_WEST;
            else if (face == BlockFaces.NORTH_WEST)
                return BlockFaces.SOUTH_EAST;
            else if (face == BlockFaces.SOUTH_EAST)
                return BlockFaces.NORTH_WEST;
            else if (face == BlockFaces.SOUTH_WEST)
                return BlockFaces.NORTH_EAST;
            else if (face == BlockFaces.WEST_NORTH_WEST)
                return BlockFaces.EAST_SOUTH_EAST;
            else if (face == BlockFaces.NORTH_NORTH_WEST)
                return BlockFaces.SOUTH_SOUTH_EAST;
            else if (face == BlockFaces.NORTH_NORTH_EAST)
                return BlockFaces.SOUTH_SOUTH_WEST;
            else if (face == BlockFaces.EAST_NORTH_EAST)
                return BlockFaces.WEST_SOUTH_WEST;
            else if (face == BlockFaces.EAST_SOUTH_EAST)
                return BlockFaces.WEST_NORTH_WEST;
            else if (face == BlockFaces.SOUTH_SOUTH_EAST)
                return BlockFaces.NORTH_NORTH_WEST;
            else if (face == BlockFaces.SOUTH_SOUTH_WEST)
                return BlockFaces.NORTH_NORTH_EAST;
            else if (face == BlockFaces.WEST_SOUTH_WEST)
                return BlockFaces.EAST_NORTH_EAST;
            else if (face == BlockFaces.SELF)
                return BlockFaces.SELF;
            return BlockFaces.SELF;
        }
    }
}
