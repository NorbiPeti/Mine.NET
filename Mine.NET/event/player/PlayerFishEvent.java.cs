package org.bukkit.event.player;

import org.bukkit.entity.Fish;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.entity.Entity;
import org.bukkit.event.HandlerList;

/**
 * Thrown when a player is fishing
 */
public class PlayerFishEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Entity entity;
    private bool cancel = false;
    private int exp;
    private readonly State state;
    private readonly Fish hookEntity;

    /**
     * [Obsolete] replaced by {@link #PlayerFishEvent(Player, Entity, Fish,
     *     State)} to include the {@link Fish} hook entity.
     * @param player the player fishing
     * @param entity the caught entity
     * @param state the state of fishing
     */
    [Obsolete]
    public PlayerFishEvent(Player player, readonly Entity entity, readonly State state) {
        this(player, entity, null, state);
    }

    public PlayerFishEvent(Player player, readonly Entity entity, readonly Fish hookEntity, readonly State state) {
        base(player);
        this.entity = entity;
        this.hookEntity = hookEntity;
        this.state = state;
    }

    /**
     * Gets the entity caught by the player.
     * <p>
     * If player has fished successfully, the result may be cast to {@link
     * org.bukkit.entity.Item}.
     *
     * @return Entity caught by the player, Entity if fishing, and null if
     *     bobber has gotten stuck in the ground or nothing has been caught
     */
    public Entity getCaught() {
        return entity;
    }

    /**
     * Gets the fishing hook.
     *
     * @return Fish the entity representing the fishing hook/bobber.
     */
    public Fish getHook() {
        return hookEntity;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }

    /**
     * Gets the amount of experience received when fishing.
     * <p>
     * Note: This value has no default effect unless the event state is {@link
     * State#CAUGHT_FISH}.
     *
     * @return the amount of experience to drop
     */
    public int getExpToDrop() {
        return exp;
    }

    /**
     * Sets the amount of experience received when fishing.
     * <p>
     * Note: This value has no default effect unless the event state is {@link
     * State#CAUGHT_FISH}.
     *
     * @param amount the amount of experience to drop
     */
    public void setExpToDrop(int amount) {
        exp = amount;
    }

    /**
     * Gets the state of the fishing
     *
     * @return A State detailing the state of the fishing
     */
    public State getState() {
        return state;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

    /**
     * An enum to specify the state of the fishing
     */
    public enum State {

        /**
         * When a player is fishing, ie casting the line out.
         */
        FISHING,
        /**
         * When a player has successfully caught a fish and is reeling it in.
         */
        CAUGHT_FISH,
        /**
         * When a player has successfully caught an entity
         */
        CAUGHT_ENTITY,
        /**
         * When a bobber is stuck in the ground
         */
        IN_GROUND,
        /**
         * When a player fails to catch anything while fishing usually due to
         * poor aiming or timing
         */
        FAILED_ATTEMPT,
        /**
         * Called when there is a bite on the hook and it is ready to be reeled in.
         */
        BITE
    }
}
