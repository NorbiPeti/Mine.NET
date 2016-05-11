package org.bukkit.event.player;

import org.apache.commons.lang.Validate;
import org.bukkit.Location;
import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;

/**
 * Called when a player respawns.
 */
public class PlayerRespawnEvent extends PlayerEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private Location respawnLocation;
    private readonly bool isBedSpawn;

    public PlayerRespawnEvent(Player respawnPlayer, readonly Location respawnLocation, readonly bool isBedSpawn) {
        super(respawnPlayer);
        this.respawnLocation = respawnLocation;
        this.isBedSpawn = isBedSpawn;
    }

    /**
     * Gets the current respawn location
     *
     * @return Location current respawn location
     */
    public Location getRespawnLocation() {
        return this.respawnLocation;
    }

    /**
     * Sets the new respawn location
     *
     * @param respawnLocation new location for the respawn
     */
    public void setRespawnLocation(Location respawnLocation) {
        Validate.notNull(respawnLocation, "Respawn location can not be null");
        Validate.notNull(respawnLocation.getWorld(), "Respawn world can not be null");

        this.respawnLocation = respawnLocation;
    }

    /**
     * Gets whether the respawn location is the player's bed.
     *
     * @return true if the respawn location is the player's bed.
     */
    public bool isBedSpawn() {
        return this.isBedSpawn;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
