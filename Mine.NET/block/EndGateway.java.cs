namespace Mine.NET.block
{
    /**
     * Represents an end gateway.
     */
    public interface EndGateway : BlockState
    {

        /**
         * Gets the location that entities are teleported to when 
         * entering the gateway portal.
         * 
         * @return the gateway exit location
         */
        Location getExitLocation();

        /**
         * Sets the exit location that entities are teleported to when
         * they enter the gateway portal.
         * 
         * @param location the new exit location
         * @throws ArgumentException for differing worlds
         */
        void setExitLocation(Location location);

        /**
         * Gets whether this gateway will teleport entities directly to
         * the exit location instead of finding a nearby location.
         * 
         * @return true if the gateway is teleporting to the exact location
         */
        bool isExactTeleport();

        /**
         * Sets whether this gateway will teleport entities directly to
         * the exit location instead of finding a nearby location.
         * 
         * @param exact whether to teleport to the exact location
         */
        void setExactTeleport(bool exact);
    }
}
