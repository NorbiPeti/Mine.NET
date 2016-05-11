package org.bukkit.event.player;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.entity.EntityUnleashEvent;

/**
 * Called prior to an entity being unleashed due to a player's action.
 */
public class PlayerUnleashEntityEvent extends EntityUnleashEvent implements Cancellable {
    private readonly Player player;
    private bool cancelled = false;

    public PlayerUnleashEntityEvent(Entity entity, Player player) {
        super(entity, UnleashReason.PLAYER_UNLEASH);
        this.player = player;
    }

    /**
     * Returns the player who is unleashing the entity.
     *
     * @return The player
     */
    public Player getPlayer() {
        return player;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }
}
