namespace Mine.NET
{
    public enum PistonMoveReaction
    {

        /**
         * Indicates that the block can be pushed or pulled.
         */
        MOVE = 0,
        /**
         * Indicates the block is fragile and will break if pushed on.
         */
        BREAK = 1,
        /**
         * Indicates that the block will resist being pushed or pulled.
         */
        BLOCK = 2
    }
}
