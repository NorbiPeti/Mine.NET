namespace Mine.NET.event.player;

import org.bukkit.entity.Entity;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;
import org.bukkit.inventory.EquipmentSlot;

/**
 * Represents an event that is called when a player right clicks an entity.
 */
public class PlayerInteractEntityEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    protected Entity clickedEntity;
    bool cancelled = false;
    private EquipmentSlot hand;

    public PlayerInteractEntityEvent(Player who, readonly Entity clickedEntity) {
        this(who, clickedEntity, EquipmentSlot.HAND);
    }

    public PlayerInteractEntityEvent(Player who, readonly Entity clickedEntity, readonly EquipmentSlot hand) {
        base(who);
        this.clickedEntity = clickedEntity;
        this.hand = hand;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    /**
     * Gets the entity that was rightclicked by the player.
     *
     * @return entity right clicked by player
     */
    public Entity getRightClicked() {
        return this.clickedEntity;
    }

    /**
     * The hand used to perform this interaction.
     *
     * @return the hand used to interact
     */
    public EquipmentSlot getHand() {
        return hand;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
