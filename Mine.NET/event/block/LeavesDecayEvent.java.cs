namespace Mine.NET.event.block;

import org.bukkit.block.Block;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when leaves are decaying naturally.
 * <p>
 * If a Leaves Decay event is cancelled, the leaves will not decay.
 */
public class LeavesDecayEvent : BlockEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancel = false;

    public LeavesDecayEvent(Block block) {
        base(block);
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
