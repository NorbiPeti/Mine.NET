namespace Mine.NET.event.entity;

import org.bukkit.DyeColor;
import org.bukkit.entity.Sheep;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a sheep's wool is dyed
 */
public class SheepDyeWoolEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel;
    private DyeColor color;

    public SheepDyeWoolEvent(Sheep sheep, readonly DyeColor color) {
        base(sheep);
        this.cancel = false;
        this.color = color;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public override Sheep getEntity() {
        return (Sheep) entity;
    }

    /**
     * Gets the DyeColor the sheep is being dyed
     *
     * @return the DyeColor the sheep is being dyed
     */
    public DyeColor getColor() {
        return color;
    }

    /**
     * Sets the DyeColor the sheep is being dyed
     *
     * @param color the DyeColor the sheep will be dyed
     */
    public void setColor(DyeColor color) {
        this.color = color;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

}
