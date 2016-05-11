package org.bukkit.event.player;

import org.bukkit.entity.Arrow;
import org.bukkit.entity.Item;
import org.bukkit.entity.Player;

/**
 * Thrown when a player picks up an arrow from the ground.
 */
public class PlayerPickupArrowEvent extends PlayerPickupItemEvent {

    private readonly Arrow arrow;

    public PlayerPickupArrowEvent(Player player, readonly Item item, readonly Arrow arrow) {
        super(player, item, 0);
        this.arrow = arrow;
    }

    /**
     * Get the arrow being picked up by the player
     *
     * @return The arrow being picked up
     */
    public Arrow getArrow() {
        return arrow;
    }
}
