package org.bukkit.event.player;

import org.bukkit.GameMode;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when the GameMode of the player is changed.
 */
public class PlayerGameModeChangeEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private bool cancelled;
    private readonly GameMode newGameMode;

    public PlayerGameModeChangeEvent(Player player, readonly GameMode newGameMode) {
        base(player);
        this.newGameMode = newGameMode;
    }

    public bool isCancelled() {
        return cancelled;
    }

    public void setCancelled(bool cancel) {
        this.cancelled = cancel;
    }

    /**
     * Gets the GameMode the player is switched to.
     *
     * @return  player's new GameMode
     */
    public GameMode getNewGameMode() {
        return newGameMode;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
