namespace Mine.NET.event.player;

import org.bukkit.inventory.EquipmentSlot;
import org.bukkit.entity.Player;
import org.bukkit.entity.ArmorStand;
import org.bukkit.event.HandlerList;
import org.bukkit.inventory.ItemStack;

/**
 * Called when a player interacts with an armor stand and will either swap, retrieve or place an item.
 */
public class PlayerArmorStandManipulateEvent : PlayerInteractEntityEvent {

    private static readonly HandlerList handlers = new HandlerList();

    private readonly ItemStack playerItem;
    private readonly ItemStack armorStandItem;
    private readonly EquipmentSlot slot;

    public PlayerArmorStandManipulateEvent(Player who, readonly ArmorStand clickedEntity, readonly ItemStack playerItem, readonly ItemStack armorStandItem, readonly EquipmentSlot slot) {
        base(who, clickedEntity);
        this.playerItem = playerItem;
        this.armorStandItem = armorStandItem;
        this.slot = slot;
    }

    /**
     * Returns the item held by the player. If this Item is null and the armor stand Item is also null,
     * there will be no transaction between the player and the armor stand.
     * If the Player's item is null, but the armor stand item is not then the player will obtain the armor stand item.
     * In the case that the Player's item is not null, but the armor stand item is null, the players item will be placed on the armor stand.
     * If both items are not null, the items will be swapped.
     * In the case that the event is cancelled the original items will remain the same.
     * @return the item held by the player.
     */
    public ItemStack getPlayerItem() {
        return this.playerItem;
    }

    /**
     * Returns the item held by the armor stand.
     * If this Item is null and the player's Item is also null, there will be no transaction between the player and the armor stand.
     * If the Player's item is null, but the armor stand item is not then the player will obtain the armor stand item.
     * In the case that the Player's item is not null, but the armor stand item is null, the players item will be placed on the armor stand.
     * If both items are not null, the items will be swapped.
     * In the case that the event is cancelled the original items will remain the same.
     * @return the item held by the armor stand.
     */
    public ItemStack getArmorStandItem() {
        return this.armorStandItem;
    }

    /**
     * Returns the raw item slot of the armor stand in this event.
     *
     * @return the index of the item obtained or placed of the armor stand.
     */
    public EquipmentSlot getSlot() {
        return this.slot;
    }

    public override ArmorStand getRightClicked() {
        return (ArmorStand) this.clickedEntity;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
