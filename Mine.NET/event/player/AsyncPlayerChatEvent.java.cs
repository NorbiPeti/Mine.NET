using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.Event.player
{
    /**
     * This event will sometimes fire synchronously, depending on how it was
     * triggered.
     * <p>
     * The constructor provides a bool to indicate if the event was fired
     * synchronously or asynchronously. When asynchronous, this event can be
     * called from any thread, sans the main thread, and has limited access to the
     * API.
     * <p>
     * If a player is the direct cause of this event by an incoming packet, this
     * event will be asynchronous. If a plugin triggers this event by compelling a
     * player to chat, this event will be synchronous.
     * <p>
     * Care should be taken to check {@link #isAsynchronous()} and treat the event
     * appropriately.
     */
    public class AsyncPlayerChatEvent : PlayerEvent, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel = false;
        private String message;
        private String format = "<%1$s> %2$s";
        private readonly HashSet<Player> recipients;

        /**
         *
         * @param async This changes the event to a synchronous state.
         * @param who the chat sender
         * @param message the message sent
         * @param players the players to receive the message. This may be a lazy
         *     or unmodifiable collection.
         */
        public AsyncPlayerChatEvent(bool async, Player who, String message, HashSet<Player> players) : base(who, async)
        {
            this.message = message;
            recipients = players;
        }

        /**
         * Gets the message that the player is attempting to send. This message
         * will be used with {@link #getFormat()}.
         *
         * @return Message the player is attempting to send
         */
        public String getMessage()
        {
            return message;
        }

        /**
         * Sets the message that the player will send. This message will be used
         * with {@link #getFormat()}.
         *
         * @param message New message that the player will send
         */
        public void setMessage(String message)
        {
            this.message = message;
        }

        /**
         * Gets the format to use to display this chat message.
         * <p>
         * When this event finishes execution, the first format parameter is the
         * {@link Player#getDisplayName()} and the second parameter is {@link
         * #getMessage()}
         *
         * @return {@link String#format(String, Object...)} compatible format
         *     string
         */
        public String getFormat()
        {
            return format;
        }

        /**
         * Sets the format to use to display this chat message.
         * <p>
         * When this event finishes execution, the first format parameter is the
         * {@link Player#getDisplayName()} and the second parameter is {@link
         * #getMessage()}
         *
         * @param format {@link String#format(String, Object...)} compatible
         *     format string
         * @throws IllegalFormatException if the underlying API throws the
         *     exception
         * @throws NullPointerException if format is null
         * @see String#format(String, Object...)
         */
        public void setFormat(String format)
        {
            // Oh for a better way to do this!
            String.Format(format, player, message); //TODO

            this.format = format;
        }

        /**
         * Gets a set of recipients that this chat message will be displayed to.
         * <p>
         * The set returned is not guaranteed to be mutable and may auto-populate
         * on access. Any listener accessing the returned set should be aware that
         * it may reduce performance for a lazy set implementation.
         * <p>
         * Listeners should be aware that modifying the list may throw {@link
         * UnsupportedOperationException} if the event caller provides an
         * unmodifiable set.
         *
         * @return All Players who will see this chat message
         */
        public HashSet<Player> getRecipients()
        {
            return recipients;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
