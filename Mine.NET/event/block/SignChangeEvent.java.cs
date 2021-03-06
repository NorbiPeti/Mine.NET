using Mine.NET.block;
using Mine.NET.entity;
using System;

namespace Mine.NET.Event.block
{
    /**
     * Called when a sign is changed by a player.
     * <p>
     * If a Sign Change event is cancelled, the sign will not be changed.
     */
    public class SignChangeEventArgs : BlockEventArgs, Cancellable
    {
        private bool cancel = false;
        private readonly Player player;
        private readonly String[] lines;

        public SignChangeEventArgs(Block theBlock, Player thePlayer, String[] theLines) : base(theBlock)
        {
            this.player = thePlayer;
            this.lines = theLines;
        }

        /**
         * Gets the player changing the sign involved in this event.
         *
         * @return the Player involved in this event
         */
        public Player getPlayer()
        {
            return player;
        }

        /**
         * Gets all of the lines of text from the sign involved in this event.
         *
         * @return the String array for the sign's lines new text
         */
        public String[] getLines()
        {
            return lines;
        }

        /**
         * Gets a single line of text from the sign involved in this event.
         *
         * @param index index of the line to get
         * @return the String containing the line of text associated with the
         *     provided index
         * @throws IndexOutOfBoundsException thrown when the provided index is {@literal > 3
         *     or < 0}
         */
        public String getLine(int index)
        {
            return lines[index];
        }

        /**
         * Sets a single line for the sign involved in this event
         *
         * @param index index of the line to set
         * @param line text to set
         * @throws IndexOutOfBoundsException thrown when the provided index is {@literal > 3
         *     or < 0}
         */
        public void setLine(int index, String line)
        {
            lines[index] = line;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }
    }
}
