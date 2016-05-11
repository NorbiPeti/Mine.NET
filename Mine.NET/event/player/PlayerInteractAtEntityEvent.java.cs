package org.bukkit.event.player;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Player;
import org.bukkit.event.HandlerList;
import org.bukkit.inventory.EquipmentSlot;
import org.bukkit.util.Vector;

/**
 * Represents an event that is called when a player right clicks an entity
 * with a location on the entity the was clicked.
 */
public class PlayerInteractAtEntityEvent : PlayerInteractEntityEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Vector position;

    public PlayerInteractAtEntityEvent(Player who, Entity clickedEntity, Vector position) {
        this(who, clickedEntity, position, EquipmentSlot.HAND);
    }

    public PlayerInteractAtEntityEvent(Player who, Entity clickedEntity, Vector position, EquipmentSlot hand) {
        super(who, clickedEntity, hand);
        this.position = position;
    }

    public Vector getClickedPosition() {
        return position.clone();
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
