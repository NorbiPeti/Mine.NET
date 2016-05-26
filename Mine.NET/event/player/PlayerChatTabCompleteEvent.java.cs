using Mine.NET.entity;
using System;
using System.Collections.ObjectModel;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player attempts to tab-complete a chat message.
     */
    public class PlayerChatTabCompleteEventArgs : PlayerEventArgs
    {
        private readonly String message;
        private readonly String lastToken;
        private readonly Collection<String> completions;

        public PlayerChatTabCompleteEventArgs(Player who, String message, Collection<String> completions) : base(who)
        {
            if (message == null) throw new ArgumentNullException("Message cannot be null");
            if (completions == null) throw new ArgumentNullException("Completions cannot be null");
            this.message = message;
            int i = message.LastIndexOf(' ');
            if (i < 0)
            {
                this.lastToken = message;
            }
            else
            {
                this.lastToken = message.Substring(i + 1);
            }
            this.completions = completions;
        }

        /**
         * Gets the chat message being tab-completed.
         *
         * @return the chat message
         */
        public String getChatMessage()
        {
            return message;
        }

        /**
         * Gets the last 'token' of the message being tab-completed.
         * <p>
         * The token is the substring starting with the char after the last
         * space in the message.
         *
         * @return The last token for the chat message
         */
        public String getLastToken()
        {
            return lastToken;
        }

        /**
         * This is the collection of completions for this event.
         *
         * @return the current completions
         */
        public Collection<String> getTabCompletions()
        {
            return completions;
        }
    }
}
