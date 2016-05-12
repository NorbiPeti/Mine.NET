using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
/**
 * Called when a human entity's food level changes
 */
public class FoodLevelChangeEvent : EntityEvent<HumanEntity>, Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel = false;
    private int level;

    public FoodLevelChangeEvent(HumanEntity what, int level) : base(what)
        {
        this.level = level;
    }

    /**
     * Gets the resultant food level that the entity involved in this event
     * should be set to.
     * <p>
     * Where 20 is a full food bar and 0 is an empty one.
     *
     * @return The resultant food level
     */
    public int getFoodLevel() {
        return level;
    }

    /**
     * Sets the resultant food level that the entity involved in this event
     * should be set to
     *
     * @param level the resultant food level that the entity involved in this
     *     event should be set to
     */
    public void setFoodLevel(int level) {
        if (level > 20) level = 20;
        else if (level < 0) level = 0;

        this.level = level;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
}
