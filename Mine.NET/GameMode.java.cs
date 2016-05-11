namespace Mine.NET
{
    /**
     * Represents the various type of game modes that {@link HumanEntity}s may
     * have
     */
    public enum GameMode
    {
        /**
         * Creative mode may fly, build instantly, become invulnerable and create
         * free items.
         */
        CREATIVE = 1,

        /**
         * Survival mode is the "normal" gameplay type, with no special features.
         */
        SURVIVAL = 0,

        /**
         * Adventure mode cannot break blocks without the correct tools.
         */
        ADVENTURE = 2,

        /**
         * Spectator mode cannot interact with the world in anyway and is 
         * invisible to normal players. This grants the player the 
         * ability to no-clip through the world.
         */
        SPECTATOR = 3
    }
}
