package org.bukkit.event.block;

import com.google.common.base.Preconditions;
import org.bukkit.block.Block;
import org.bukkit.entity.Entity;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

public class CauldronLevelChangeEvent : BlockEvent : Cancellable {

    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    //
    private readonly Entity entity;
    private readonly ChangeReason reason;
    private readonly int oldLevel;
    private int newLevel;

    public CauldronLevelChangeEvent(Block block, Entity entity, ChangeReason reason, int oldLevel, int newLevel) {
        base(block);
        this.entity = entity;
        this.reason = reason;
        this.oldLevel = oldLevel;
        this.newLevel = newLevel;
    }

    /**
     * Get entity which did this. May be null.
     *
     * @return acting entity
     */
    public Entity getEntity() {
        return entity;
    }

    public ChangeReason getReason() {
        return reason;
    }

    public int getOldLevel() {
        return oldLevel;
    }

    public int getNewLevel() {
        return newLevel;
    }

    public void setNewLevel(int newLevel) {
        Preconditions.checkArgument(0 <= newLevel && newLevel <= 3, "Cauldron level out of bounds 0 <= %s <= 3", newLevel);
        this.newLevel = newLevel;
    }

    public override bool isCancelled() {
        return cancelled;
    }

    public override void setCancelled(bool cancelled) {
        this.cancelled = cancelled;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

    public enum ChangeReason {
        /**
         * Player emptying the cauldron by filling their bucket.
         */
        BUCKET_FILL,
        /**
         * Player filling the cauldron by emptying their bucket.
         */
        BUCKET_EMPTY,
        /**
         * Player emptying the cauldron by filling their bottle.
         */
        BOTTLE_FILL,
        /**
         * Player filling the cauldron by emptying their bottle.
         */
        BOTTLE_EMPTY,
        /**
         * Player cleaning their banner.
         */
        BANNER_WASH,
        /**
         * Player cleaning their armor.
         */
        ARMOR_WASH,
        /**
         * Entity being extinguished.
         */
        EXTINGUISH,
        /**
         * Evaporating due to biome dryness.
         */
        EVAPORATE,
        /**
         * Unknown.
         */
        UNKNOWN
    }
}
