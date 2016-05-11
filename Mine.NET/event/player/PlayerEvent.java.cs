package org.bukkit.event.player;

import org.bukkit.entity.Player;
import org.bukkit.event.Event;

/**
 * Represents a player related event
 */
public abstract class PlayerEvent : Event {
    protected Player player;

    public PlayerEvent(Player who) {
        player = who;
    }

    PlayerEvent(Player who, bool async) {
        super(async);
        player = who;

    }

    /**
     * Returns the player involved in this event
     *
     * @return Player who is involved in this event
     */
    public readonly Player getPlayer() {
        return player;
    }
}
