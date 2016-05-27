using Mine.NET.entity;
using System;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player respawns.
     */
    public class PlayerRespawnEventArgs : PlayerEventArgs
    {
        private Location respawnLocation;

        public PlayerRespawnEventArgs(Player respawnPlayer, Location respawnLocation, bool isBedSpawn) : base(respawnPlayer)
        {
            this.respawnLocation = respawnLocation;
            this.isBedSpawn = isBedSpawn;
        }

        /**
         * Gets the current respawn location
         *
         * @return Location current respawn location
         */
        public Location getRespawnLocation()
        {
            return this.respawnLocation;
        }

        /**
         * Sets the new respawn location
         *
         * @param respawnLocation new location for the respawn
         */
        public void setRespawnLocation(Location respawnLocation)
        {
            if (respawnLocation == null) throw new ArgumentNullException("Respawn location can not be null");
            if (respawnLocation.getWorld() == null) throw new ArgumentNullException("Respawn world can not be null");

            this.respawnLocation = respawnLocation;
        }

        /**
         * Gets whether the respawn location is the player's bed.
         *
         * @return true if the respawn location is the player's bed.
         */
        public bool isBedSpawn { get; private set; }
    }
}
