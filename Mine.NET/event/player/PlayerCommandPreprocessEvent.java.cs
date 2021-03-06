using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.Event.player
{
    /**
     * This event is called whenever a player runs a command (by placing a slash
     * at the start of their message). It is called early in the command handling
     * process, and modifications in this event (via {@link #setMessage(String)})
     * will be shown in the behavior.
     * <p>
     * Many plugins will have <b>no use for this event</b>, and you should
     * attempt to avoid using it if it is not necessary.
     * <p>
     * Some examples of valid uses for this event are:
     * <ul>
     * <li>Logging executed commands to a separate file
     * <li>Variable substitution. For example, replacing
     *     <code>${nearbyPlayer}</code> with the name of the nearest other
     *     player, or simulating the <code>@a</code> and <code>@p</code>
     *     decorators used by Command Blocks in plugins that do not handle it.
     * <li>Conditionally blocking commands belonging to other plugins. For
     *     example, blocking the use of the <code>/home</code> command in a
     *     combat arena.
     * <li>Per-sender command aliases. For example, after a player runs the
     *     command <code>/calias cr gamemode creative</code>, the next time they
     *     run <code>/cr</code>, it gets replaced into
     *     <code>/gamemode creative</code>. (Global command aliases should be
     *     done by registering the alias.)
     * </ul>
     * <p>
     * Examples of incorrect uses are:
     * <ul>
     * <li>Using this event to run command logic
     * </ul>
     * <p>
     * If the event is cancelled, processing of the command will halt.
     * <p>
     * The state of whether or not there is a slash (<code>/</code>) at the
     * beginning of the message should be preserved. If a slash is added or
     * removed, unexpected behavior may result.
     */
    public class PlayerCommandPreprocessEventArgs : PlayerEventArgs, Cancellable
    {
        private bool cancel = false;
        private String message;
        private String format = "<%1$s> %2$s";
        private readonly HashSet<Player> recipients;

        public PlayerCommandPreprocessEventArgs(Player player, String message) : base(player)
        {
            this.recipients = new HashSet<Player>(player.getServer().getOnlinePlayers());
            this.message = message;
        }

        public PlayerCommandPreprocessEventArgs(Player player, String message, HashSet<Player> recipients) :
            base(player)
        {
            this.recipients = recipients;
            this.message = message;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Gets the command that the player is attempting to send.
         * <p>
         * All commands begin with a special char; implementations do not
         * consider the first char when executing the content.
         *
         * @return Message the player is attempting to send
         */
        public String getMessage()
        {
            return message;
        }

        /**
         * Sets the command that the player will send.
         * <p>
         * All commands begin with a special char; implementations do not
         * consider the first char when executing the content.
         *
         * @param command New message that the player will send
         * @throws ArgumentException if command is null or empty
         */
        public void setMessage(String command)
        {
            if (command == null) throw new ArgumentNullException("Command cannot be null");
            if (string.IsNullOrEmpty(command)) throw new ArgumentException("Command cannot be empty");
            this.message = command;
        }

        /**
         * Sets the player that this command will be executed as.
         *
         * @param player New player which this event will execute as
         * @throws ArgumentException if the player provided is null
         */
        public void setPlayer(Player player)
        {
            if (player == null) throw new ArgumentNullException("Player cannot be null");
            this.player = player;
        }

        /**
         * Gets a set of recipients that this chat message will be displayed to.
         * <p>
         * The set returned is not guaranteed to be mutable and may auto-populate
         * on access. Any listener accessing the returned set should be aware that
         * it may reduce performance for a lazy set implementation. Listeners
         * should be aware that modifying the list may throw {@link
         * InvalidOperationException} if the event caller provides an
         * unmodifiable set.
         *
         * [Obsolete] This method is provided for backward compatibility with no
         *     guarantee to the effect of viewing or modifying the set.
         * @return All Players who will see this chat message
         */
        [Obsolete]
        public HashSet<Player> getRecipients()
        {
            return recipients;
        }
    }
}
