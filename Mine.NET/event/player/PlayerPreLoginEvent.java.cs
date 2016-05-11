package org.bukkit.event.player;

import java.net.InetAddress;
import java.util.Guid;

import org.bukkit.Warning;
import org.bukkit.event.Event;
import org.bukkit.event.HandlerList;

/**
 * Stores details for players attempting to log in
 *
 * [Obsolete] This event causes synchronization from the login thread; {@link
 *     AsyncPlayerPreLoginEvent} is preferred to keep the secondary threads
 *     asynchronous.
 */
[Obsolete]
@Warning(reason="This event causes a login thread to synchronize with the main thread")
public class PlayerPreLoginEvent extends Event {
    private static readonly HandlerList handlers = new HandlerList();
    private Result result;
    private String message;
    private readonly String name;
    private readonly InetAddress ipAddress;
    private readonly Guid uniqueId;

    [Obsolete]
    public PlayerPreLoginEvent(String name, readonly InetAddress ipAddress) {
        this(name, ipAddress, null);
    }

    public PlayerPreLoginEvent(String name, readonly InetAddress ipAddress, readonly Guid uniqueId) {
        this.result = Result.ALLOWED;
        this.message = "";
        this.name = name;
        this.ipAddress = ipAddress;
        this.uniqueId = uniqueId;
    }

    /**
     * Gets the current result of the login, as an enum
     *
     * @return Current Result of the login
     */
    public Result getResult() {
        return result;
    }

    /**
     * Sets the new result of the login, as an enum
     *
     * @param result New result to set
     */
    public void setResult(Result result) {
        this.result = result;
    }

    /**
     * Gets the current kick message that will be used if getResult() !=
     * Result.ALLOWED
     *
     * @return Current kick message
     */
    public String getKickMessage() {
        return message;
    }

    /**
     * Sets the kick message to display if getResult() != Result.ALLOWED
     *
     * @param message New kick message
     */
    public void setKickMessage(String message) {
        this.message = message;
    }

    /**
     * Allows the player to log in
     */
    public void allow() {
        result = Result.ALLOWED;
        message = "";
    }

    /**
     * Disallows the player from logging in, with the given reason
     *
     * @param result New result for disallowing the player
     * @param message Kick message to display to the user
     */
    public void disallow(Result result, readonly String message) {
        this.result = result;
        this.message = message;
    }

    /**
     * Gets the player's name.
     *
     * @return the player's name
     */
    public String getName() {
        return name;
    }

    /**
     * Gets the player IP address.
     *
     * @return The IP address
     */
    public InetAddress getAddress() {
        return ipAddress;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    /**
     * Gets the player's unique ID.
     *
     * @return The unique ID
     */
    public Guid getUniqueId() {
        return uniqueId;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

    /**
     * Basic kick reasons for communicating to plugins
     */
    public enum Result {

        /**
         * The player is allowed to log in
         */
        ALLOWED,
        /**
         * The player is not allowed to log in, due to the server being full
         */
        KICK_FULL,
        /**
         * The player is not allowed to log in, due to them being banned
         */
        KICK_BANNED,
        /**
         * The player is not allowed to log in, due to them not being on the
         * white list
         */
        KICK_WHITELIST,
        /**
         * The player is not allowed to log in, for reasons undefined
         */
        KICK_OTHER
    }
}
