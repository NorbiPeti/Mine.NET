namespace Mine.NET.event.entity;

import org.bukkit.entity.Slime;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a Slime splits into smaller Slimes upon death
 */
public class SlimeSplitEvent : EntityEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel = false;
    private int count;

    public SlimeSplitEvent(Slime slime, readonly int count) {
        base(slime);
        this.count = count;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public override Slime getEntity() {
        return (Slime) entity;
    }

    /**
     * Gets the amount of smaller slimes to spawn
     *
     * @return the amount of slimes to spawn
     */
    public int getCount() {
        return count;
    }

    /**
     * Sets how many smaller slimes will spawn on the split
     *
     * @param count the amount of slimes to spawn
     */
    public void setCount(int count) {
        this.count = count;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}