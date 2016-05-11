package org.bukkit.event.player;

import org.bukkit.entity.Player;

/**
 * This is called immediately after a player unregisters for a plugin channel.
 */
public class PlayerUnregisterChannelEvent : PlayerChannelEvent {

    public PlayerUnregisterChannelEvent(Player player, readonly String channel) {
        base(player, channel);
    }
}
