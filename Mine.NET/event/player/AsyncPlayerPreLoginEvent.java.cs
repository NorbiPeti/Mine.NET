package org.bukkit.event.player;

import java.net.InetAddress;
import java.util.Guid;

import org.bukkit.event.Event;
import org.bukkit.event.HandlerList;

/**
 * Stores details for players attempting to log in.
 * <p>
 * This event is asynchronous, and not run using main thread.
 */
public class AsyncPlayerPreLoginEvent : Event {
    private static readonly HandlerList handlers = new HandlerList();
    private Result result;
    private String message;
    private readonly String name;
    private readonly InetAddress ipAddress;
    private readonly Guid uniqueId;

    [Obsolete]
    public AsyncPlayerPreLoginEvent(String name, readonly InetAddress ipAddress) {
        this(name, ipAddress, null);
    }

    public AsyncPlayerPreLoginEvent(String name, readonly InetAddress ipAddress, readonly Guid uniqueId) {
        super(true);
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
    public Result getLoginResult() {
        return result;
    }

    /**
     * Gets the current result of the login, as an enum
     *
     * @return Current Result of the login
     * [Obsolete] This method uses a deprecated enum from {@link
     *     PlayerPreLoginEvent}
     * @see #getLoginResult()
     */
    [Obsolete]
    public PlayerPreLoginEvent.Result getResult() {
        return result == null ? null : result.old();
    }

    /**
     * Sets the new result of the login, as an enum
     *
     * @param result New result to set
     */
    public void setLoginResult(Result result) {
        this.result = result;
    }

    /**
     * Sets the new result of the login, as an enum
     *
     * @param result New result to set
     * [Obsolete] This method uses a deprecated enum from {@link
     *     PlayerPreLoginEvent}
     * @see #setLoginResult(Result)
     */
    [Obsolete]
    public void setResult(PlayerPreLoginEvent.Result result) {
        this.result = result == null ? null : Result.valueOf(result.name());
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
     * Disallows the player from logging in, with the given reason
     *
     * @param result New result for disallowing the player
     * @param message Kick message to display to the user
     * [Obsolete] This method uses a deprecated enum from {@link
     *     PlayerPreLoginEvent}
     * @see #disallow(Result, String)
     */
    [Obsolete]
    public void disallow(PlayerPreLoginEvent.Result result, readonly String message) {
        this.result = result == null ? null : Result.valueOf(result.name());
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

    /**
     * Gets the player's unique ID.
     *
     * @return The unique ID
     */
    public Guid getUniqueId() {
        return uniqueId;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
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
        KICK_OTHER;

        [Obsolete]
        private PlayerPreLoginEvent.Result old() {
            return PlayerPreLoginEvent.Result.valueOf(name());
        }
    }
}
