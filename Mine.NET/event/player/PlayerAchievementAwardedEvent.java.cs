namespace Mine.NET.event.player;

import org.bukkit.Achievement;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;

/**
 * Called when a player earns an achievement.
 */
public class PlayerAchievementAwardedEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly Achievement achievement;
    private bool isCancelled = false;

    public PlayerAchievementAwardedEvent(Player player, Achievement achievement) {
        base(player);
        this.achievement = achievement;
    }

    /**
     * Gets the Achievement being awarded.
     *
     * @return the achievement being awarded
     */
    public Achievement getAchievement() {
        return achievement;
    }

    public bool isCancelled() {
        return isCancelled;
    }

    public void setCancelled(bool cancel) {
        this.isCancelled = cancel;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
