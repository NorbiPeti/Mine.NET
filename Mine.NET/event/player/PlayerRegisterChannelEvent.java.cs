package org.bukkit.event.player;

import org.bukkit.entity.Player;

/**
 * This is called immediately after a player registers for a plugin channel.
 */
public class PlayerRegisterChannelEvent : PlayerChannelEvent {

    public PlayerRegisterChannelEvent(Player player, readonly String channel) {
        base(player, channel);
    }
}
